/*{
	"CREDIT": "Peter Holzkorn",
	"CATEGORIES": [
		"PH_gen"
	],
	"INPUTS": [
		{
			"NAME": "iVar",
			"TYPE": "point2D",
			"DEFAULT": [0.0, 0.0]
		},

                {
                        "NAME": "baseColor",
                        "TYPE": "color",
                        "DEFAULT": [1.0, 0.0, 0.0, 1.0]
                },
                {
                        "NAME": "sphereSize",
                        "TYPE": "float",
                        "DEFAULT": 1.0,
                        "MAX": 5.0
                },
                {
                        "NAME": "filters",
                        "TYPE": "color",
                        "DEFAULT": [0.5, 0.5, 0.5, 0.5]
                }

	]
}*/


//varying vec4 vColor;

struct Ray
{
	vec3 org;
	vec3 dir;
};

float udBox(vec3 p, vec3 size)
{
	return length(max(abs(p) - size, vec3(size)));
}

// Returns the signed distance estimate to a box at the origin of the given size
float sdBox(vec3 p, vec3 size)
{
	vec3 d = abs(p) - size;
	return min(max(d.x, max(d.y, d.z)), 0.0) + udBox(p, size);
}

float distScene(vec3 p)
{
    p.xz = mod(p.xz, 2.0);
	return sdBox(p, vec3(0.15));

	// p = rotateY(p, 0.5f * p.y);
	// float d1 = sdBox(p - vec3(0, 0.5, 0), vec3(0.5, 1.0, 0.5));
	// float d2 = sdBox(p, vec3(2.0, 0.3, 0.25));
	// return opSubtract(d1, d2);
}

float getVisibility(vec3 p0)
{
    vec3 p1 = vec3(0.0);

	vec3 rd = normalize(p1 - p0);
	float t = 10.0 * 0.05;
	float maxt = length(p1 - p0);
	float f = 1.0;
	while(t < maxt)
	{
		float d = distScene(p0 + rd * t);

		// A surface was hit before we reached p1
		if(d < 0.01)
			return 0.0;

		// Penumbra factor
		f = min(f, d / t);

		t += d;
	}

	return f;
}

vec3 raymarch(Ray ray)
{
	const int maxSteps = 10;
	const float hitThreshold = 0.02;

        float t = 0.0;

	for(int i = 0; i < maxSteps; i++)
	{
            float dist = distScene(ray.org + ray.dir * t);

            if(dist < hitThreshold * t * 2.0)
                break;

            t += dist;
	}

	return vec3(t/100.0);
}



void main(void)
{
//	spheres[0] = vec3(0.0, 0.0, 0.0);
	vec2 ndcXY = -1.0 + 2.0 * gl_FragCoord.xy / RENDERSIZE.xy;

	float aspectRatio = RENDERSIZE.x / RENDERSIZE.y;

	vec2 scaledXY = ndcXY * vec2(aspectRatio, 1.0);

	vec3 camWsXYZ = vec3(0.0, 0.0, 5.0);
	camWsXYZ.z *= 0.0/RENDERSIZE.x;

	Ray ray;
	ray.org = camWsXYZ;
	ray.dir = vec3(scaledXY, -1.0);

	gl_FragColor = vec4(raymarch(ray), 1.0);

}
