/*{
	"CREDIT": "by Carter Rosenberg",
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
                }

	]
}*/


//varying vec4 vColor;

vec3 spheres[5];

struct Ray
{
	vec3 org;
	vec3 dir;
};

vec3 raymarch(Ray ray)
{
	const int maxSteps = 10;
	float hitThreshold = 2.0 * sphereSize;


	bool hit = false;

	vec3 pos = ray.org;

	vec3 colorbase = baseColor.rgb;
	vec3 colortop = vec3(1.0, 0.0, 0.0);
	vec3 color = vec3(0.0, 0.0, 0.0);


	for(int i = 0; i < maxSteps; i++)
	{
		int numHits = 0;
		for (int j = 0; j < 4; j++)
		{
                    float d = distance(pos,	spheres[j] - vec3(0.0,0.0,3.0));
			if (d < hitThreshold)
			{
				numHits++;
				color += colorbase * pow((1.0-d/2.0),1.6) * 0.4;
			}
		}

		if(numHits > 2)
		{
			color += colortop * 0.1;
		}

		pos += 0.48 / sphereSize * ray.dir;
	}

	return color;
}

void main(void)
{
//	spheres[0] = vec3(0.0, 0.0, 0.0);
    spheres[1] = vec3(cos(TIME)*4.0, sin(TIME)*1.0, sin(TIME+0.3)-0.3);
    spheres[2] = vec3(cos(TIME*3.0)*1.0, sin(TIME*3.0)*3.1, 0.0);
    spheres[3] = vec3(sin(TIME*1.6)*1.5, cos(TIME*1.6)*1.5, cos(TIME));
    spheres[0] = vec3(0.0, 0.0, sin(TIME*2.0)*2.0);

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
