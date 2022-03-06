using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    SpriteRenderer renderer;
    float hue = 0;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    int change = 30;
    float inc = 1;
    void Update()
    {
        renderer.color = Color.HSVToRGB(Map(0, 360, 0, 1, hue), 1, 1);
        hue += inc;
        if (hue >= 360)
            inc = -1;
        else if (hue <= 0)
            inc = 1;
    }

    float Map(int ogStart, int ogEnd, int newStart, int newEnd, float value)
    {
        double scale = (double)(newEnd - newStart) / (ogEnd - ogStart);
        return (float)(newStart + ((value - ogStart) * scale));
    }
}
