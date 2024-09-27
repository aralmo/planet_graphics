public static class Vector3Extensions
{
    public static Vector3 Normalize(this Vector3 vector)
    {
        float length = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
        return new Vector3(vector.X / length, vector.Y / length, vector.Z / length);
    }
}
