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

vec3 raymarch(Ray ray)
{
	const int maxSteps = 10;
	float hitThreshold = 0.15;


	bool hit = false;

	vec3 pos = ray.org;

	vec3 colorbase = baseColor.rgb;
	vec3 colortop = vec3(1.0, 0.0, 0.0);
	vec3 color = vec3(0.0, 0.0, 0.0);


	for(int i = 0; i < maxSteps; i++)
	{
            int numHits = 0;

            vec3 modpos = pos;
            modpos.xz = mod(modpos.xz, float(i)*5.0 * filters[1]) - vec2(0.1);
            modpos.y *= tan(modpos.z + filters[0]/0.5) * 10.0 * filters[0];
            float d = udBox(modpos - vec3(0.0, 0.15, 0.0), vec3(0.5));
            if (d < hitThreshold * length(pos))
            {
                color = vec3(1.0-d*1.5, 0.5*1.0-filters[2], 0.5*filters[2]);
                break;
            }

            pos += d * ray.dir;
	}

	return color;
}

void main(void)
{
//	spheres[0] = vec3(0.0, 0.0, 0.0);
	vec2 ndcXY = -1.0 + 2.0 * gl_FragCoord.xy / RENDERSIZE.xy;

	float aspectRatio = RENDERSIZE.x / RENDERSIZE.y;

	vec2 scaledXY = ndcXY * vec2(aspectRatio, 1.0);

	vec3 camWsXYZ = vec3(0.0, 0.0, 5.0);
	camWsXYZ.z *= iVar.x/RENDERSIZE.x;

	Ray ray;
	ray.org = camWsXYZ;
	ray.dir = vec3(scaledXY, -1.0);

	gl_FragColor = vec4(raymarch(ray), 1.0);

}
