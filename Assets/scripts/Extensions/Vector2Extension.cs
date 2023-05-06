using System;
using UnityEngine;

public static class Vector2Extension
{
    private static readonly Func<float, float>[,] rotateMatrix =
    {
        { Mathf.Cos, x => -Mathf.Sin(x) },
        { Mathf.Sin, Mathf.Cos }
    };


    public static Vector2 Rotate(this Vector2 vector2, float angleInDegrees)
    {
        var angle = angleInDegrees * Mathf.Deg2Rad;
        return new Vector2(vector2.x * rotateMatrix[0, 0](angle) + vector2.y * rotateMatrix[0, 1](angle),
            vector2.x * rotateMatrix[1, 0](angle) + vector2.y * rotateMatrix[1, 1](angle));
    }

    public static Vector2 Normalize(this Vector2 vector)
    {
        return vector / vector.magnitude;
    }
}