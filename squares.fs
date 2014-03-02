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

    if (passed < 1.0) discard;

    bool regular = distance(gl_FragCoord.x, sqpos[whichsq]) <
        peakFreq * 100.0 * floor(0.5+sin( (gl_FragCoord.y + TIME * 50.0) / (peakFreq*50.0)))
        || distance(gl_FragCoord.x, sqpos[whichsq])
        < RENDERSIZE.x/45.0;

    if (!regular) discard;

    gl_FragColor = vec4(1.0, 1.0, 1.0, 1.0);

}
