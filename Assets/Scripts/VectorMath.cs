using System;
using UnityEngine;

public static class VectorMath
{
    /// <summary>
    /// Removes a direction component from a Vector3.
    /// </summary>
    /// <param name="value"> The target Vector3. </param>
    /// <param name="direction"> The direction to be removed. </param>
    /// <returns> The new Vector3 with direction negated. </returns>
    public static Vector3 NegateDirection(this Vector3 value, Vector3 direction)
    {
        return value - Vector3.Dot(value, direction.normalized) * direction.normalized;
    }

    /// <summary>
    /// Isolates a direction component of a Vector3.
    /// </summary>
    /// <param name="value"> The target Vector3. </param>
    /// <param name="direction"> The direction to be isolated. </param>
    /// <returns> The new Vector3 with isolated direction. </returns>
    public static Vector3 IsolateDirection(this Vector3 value, Vector3 direction)
    {
        return Vector3.Dot(value, direction.normalized) * direction.normalized;
    }

    /// <summary>
    /// DON'T USE YET, NOT SURE IF THIS DOES WHAT I THINK IT DOES!!!
    /// Rounds a Vector3 to the nearest multiple provided.
    /// Components that end in .5f will be rounded to their
    /// neighboring even number (Bakers Rounding).
    /// </summary>
    /// <param name="value"> The target Vector3. </param>
    /// <param name="multiple"> The multiple to be rounded to. </param>
    /// <returns> The new rounded Vector3. </returns>
    public static Vector3 Round(this ref Vector3 value, float multiple = 1)
    {
        if (multiple == 0) throw new ArgumentException(
            "Can not round to multiple of zero (0).");
        if (multiple < 0) throw new ArgumentException(
            "This method does not support rounding to negative multiples.");
        
        return new Vector3(
            Mathf.Round(value.x / multiple) * multiple,
            Mathf.Round(value.y / multiple) * multiple,
            Mathf.Round(value.z / multiple) * multiple);
    }
    
    
    /// <summary>
    /// Rounds a Vector3 to the nearest multiple provided.
    /// Components that end in .5f will be rounded to their
    /// neighboring even number (Bakers Rounding).
    /// </summary>
    /// <param name="value"> The target Vector3. </param>
    /// <param name="multiple"> The multiple to be rounded to. </param>
    /// <returns> The new rounded Vector3. </returns>
    public static Vector3 Rounded(this Vector3 value, float multiple = 1)
    {
        if (multiple == 0) throw new ArgumentException(
            "Can not round to multiple of zero (0).");
        if (multiple < 0) throw new ArgumentException(
            "This method does not support rounding to negative multiples.");
        
        return new Vector3(
            Mathf.Round(value.x / multiple) * multiple,
            Mathf.Round(value.y / multiple) * multiple,
            Mathf.Round(value.z / multiple) * multiple);
    }
}