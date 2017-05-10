using UnityEngine;
using System.Collections;

public class LowPassFilter
{
    float AccelerometerUpdateInterval = 1.0f / 60.0f;
    float LowPassKernelWidthInSeconds = 1.0f;

    private Vector3 lowPassValue;
    private float LowPassFilterFactor;

    public LowPassFilter(Vector3 startValue)
    {
        LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds;
        lowPassValue = startValue;
    }

    public Vector3 Apply(Vector3 v)
    {
        return Vector3.Lerp(lowPassValue, v, LowPassFilterFactor);
    }
}
