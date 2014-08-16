/*
{
  "CREDIT": "Peter Holzkorn",
  "CATEGORIES": [
     "PH_gen"
  ],

  "INPUTS": [
        {
                "NAME": "filters",
                "TYPE": "color",
                "DEFAULT": [0.5, 0.5, 0.5, 0.5]
        },
        {
                "NAME": "peakFreq",
                "TYPE": "float",
                "DEFAULT": 0.5
        },
        {
                "NAME": "size",
                "TYPE": "float",
                "DEFAULT": 0.01
        }


  ]
}
*/

float sqpos[4];
//float passed[4];

void main(void)
{
    const float rad = 1000.0;
    float sz = size * size * size;
    vec2 centerAt = vec2(sin(filters.x * 3.14 * 2.0) * RENDERSIZE.x/20.0 + RENDERSIZE.x/2.0, cos(filters.x * 3.14 * 2.0) * RENDERSIZE.y/20.0 + RENDERSIZE.y/2.0);

    gl_FragColor = vec4(1.0);

    float dist = rad * sz;

    if (distance(gl_FragCoord.xy, centerAt) < dist)
    {
        gl_FragColor.xyz = vec3(0.0);
    }
    else if (distance(gl_FragCoord.xy, centerAt) < dist + dist * filters.y)
    {
        gl_FragColor.xyz = vec3( (distance(gl_FragCoord.xy, centerAt) - dist) / (dist * filters.y + 0.01));
    }

}
