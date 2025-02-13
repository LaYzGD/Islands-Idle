using UnityEngine;

public static class IsometricMatrix
{
    private static Matrix4x4 _matrix = Matrix4x4.Rotate(Quaternion.Euler(0f, 45f, 0f));
    public static Vector3 ToIsometric(this Vector3 input) => _matrix.MultiplyPoint3x4(input);
}
