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
        }

  ]
}
*/

float sqpos[4];
//float passed[4];

void main(void)
{
    sqpos[0] = RENDERSIZE.x/5.0;
    sqpos[1] = RENDERSIZE.x*2.0/5.0;
    sqpos[2] = RENDERSIZE.x*3.0/5.0;
    sqpos[3] = RENDERSIZE.x*4.0/5.0;

    int whichsq = -1;
    for (int i = 0; i < 4; i++)
    {
        if (distance(gl_FragCoord.x, sqpos[i]) < RENDERSIZE.x/20.0)
        {
            whichsq = i;
        }
    }

    if (whichsq < 0) return;

    float passed = distance(gl_FragCoord.y, RENDERSIZE.y/2.0) / RENDERSIZE.y < filters[whichsq] ? 1.0 : 0.0;

    gl_FragColor = vec4(passed);

}
