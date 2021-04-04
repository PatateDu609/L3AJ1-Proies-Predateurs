using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{
    public static double GetRandomDouble(System.Random random, double minimum, double maximum)
    {
        return random.NextDouble() * (maximum - minimum) + minimum;
    }

    public static Vector3 GetRandomVector3(System.Random random, Vector3 minimum, Vector3 maximum)
    {
        float x = (float)GetRandomDouble(random, minimum.x, maximum.x);
        float y = (float)GetRandomDouble(random, minimum.y, maximum.y);
        float z = (float)GetRandomDouble(random, minimum.z, maximum.z);

        return new Vector3(x, y, z);
    }

    public static Vector3 GetRandomVector3(System.Random random, int minimum, int maximum)
    {
        float value = random.Next(minimum, maximum);

        return new Vector3(value, value, value);
    }

    public static Quaternion GetRandomQuaternion(System.Random random, bool onX, bool onY, bool onZ) {
        int x = onX ? random.Next(0, 360) : 0;
        int y = onY ? random.Next(0, 360) : 0;
        int z = onZ ? random.Next(0, 360) : 0;

        return Quaternion.Euler(x, y, z);
    }
}
