using UnityEngine;

public static class Utility
{
    private const float G = 0.66743f; // Gravitational constant
    private const float density = 238.7324149f; // Density of an attractor

    // Calculates the gravitational force between 2 given rigidbodies
    public static Vector3 CalcForce(Rigidbody a, Rigidbody b)
    {
        Vector3 displacement = b.position - a.position;

        float r2 = displacement.x * displacement.x + displacement.y * displacement.y + displacement.z * displacement.z;
        float distance = Mathf.Sqrt(r2);
        float force = G * (a.mass * b.mass / r2);

        return displacement / distance * force;
    }

    // Calculates the diameter of a sphere given mass
    public static float CalcDiameter(float mass)
    {
        float V = mass / density;
        return Mathf.Pow(6 * V / Mathf.PI, 1f / 3f);
    }
}
