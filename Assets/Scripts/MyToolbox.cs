using UnityEngine;

public static class MyToolbox
{
    private static float _defaultXMin = 0f;
    private static float _defaultXMax = 1f;
    private static float _defaultYMin = 0f;
    private static float _defaultYMax = 1f;

    public static Vector2 GetRandomVector2(float _xMin, float _xMax, float _yMin, float _yMax)
    {
        Vector2 value = new Vector2();
        value.x = UnityEngine.Random.Range(_xMin, _xMax);
        value.y = UnityEngine.Random.Range(_yMin, _yMax);
        return value;
    }

    public static Vector2 GetRandomVector2() {
        return GetRandomVector2(_defaultXMin, _defaultXMax, _defaultYMin, _defaultYMax);
    }
}