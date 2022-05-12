using UnityEngine;
using UnityEngine.Profiling;

public static class Utility
{
    private const float G = 6.6743f; // Gravitational constant

    // Calculates the gravitational force between 2 given object positions (objects are all mass 1)
    public static Vector3 CalcForce(Vector3 a, Vector3 b)
    {
        Vector3 displacement = b - a;

        float r2 = displacement.x * displacement.x + displacement.y * displacement.y + displacement.z * displacement.z;
        float distance = Mathf.Sqrt(r2);
        float force = G / r2;

        return displacement / (distance / force);
    }
}
