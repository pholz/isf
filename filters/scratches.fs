/*
{
  "CREDIT": "Peter Holzkorn",
  "CATEGORIES": [
     "PH_filters"
  ],

  "INPUTS": [
        {
                "NAME": "filters",
                "TYPE": "color",
                "DEFAULT": [0.5, 0.5, 0.5, 0.5]
        },
        {
                "NAME": "inputImage",
                "TYPE": "image"
        },
        {
                "NAME": "noiseImage",
                "TYPE": "image"
        }


  ]
}
*/

void main(void)
{
    vec4 srcPixel = IMG_THIS_PIXEL(inputImage);
    vec4 noisePixel = IMG_THIS_PIXEL(noiseImage);
    gl_FragColor = mix(srcPixel, noisePixel, 0.5);

}
