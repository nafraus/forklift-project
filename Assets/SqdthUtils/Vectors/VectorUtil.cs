using UnityEngine;

namespace SqdthUtils.Vectors
{
    public static class VectorUtil
    {
        #region Component Modifiers

        /// <summary>
        /// Removes a direction component from a Vector3. Does not modify the target vector
        /// </summary>
        /// <param name="target"> The target Vector3. </param>
        /// <param name="direction"> The direction to be removed. </param>
        /// <returns> The new Vector3 with direction negated. </returns>
        public static Vector3 NegatedDirection(this Vector3 target, Vector3 direction)
        {
            return target - Vector3.Dot(target, direction.normalized) * direction.normalized;
        }

        /// <summary>
        /// Isolates a direction component of a Vector3. Does not modify the target vector
        /// </summary>
        /// <param name="target"> The target Vector3. </param>
        /// <param name="direction"> The direction to be isolated. </param>
        /// <returns> The new Vector3 with isolated direction. </returns>
        public static Vector3 IsolatedDirection(this Vector3 target, Vector3 direction)
        {
            return Vector3.Dot(target, direction.normalized) * direction.normalized;
        }
        
        /// <summary>
        /// Removes a direction component from a Vector3. Modifies the target vector
        /// </summary>
        /// <param name="target"> The target Vector3. </param>
        /// <param name="direction"> The direction to be removed. </param>
        public static void NegateDirection(this ref Vector3 target, Vector3 direction)
        {
            target -= Vector3.Dot(target, direction.normalized) * direction.normalized;
        }

        /// <summary>
        /// Isolates a direction component of a Vector3. Modifies the target vector
        /// </summary>
        /// <param name="target"> The target Vector3. </param>
        /// <param name="direction"> The direction to be isolated. </param>
        public static void IsolateDirection(this ref Vector3 target, Vector3 direction)
        {
            target = Vector3.Dot(target, direction.normalized) * direction.normalized;
        }

        #endregion

        #region Swizzles

        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> X, X, X </returns>
        public static Vector3 XXX(this Vector3 target)
        {
            return new Vector3(target.x, target.x, target.x);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> X, X, Y </returns>
        public static Vector3 XXY(this Vector3 target)
        {
            return new Vector3(target.x, target.x, target.y);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> X, X, Z </returns>
        public static Vector3 XXZ(this Vector3 target)
        {
            return new Vector3(target.x, target.x, target.z);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> X, Y, X </returns>
        public static Vector3 XYX(this Vector3 target)
        {
            return new Vector3(target.x, target.y, target.x);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> X, Y, Y </returns>
        public static Vector3 XYY(this Vector3 target)
        {
            return new Vector3(target.x, target.y, target.y);
        }
        
        // SKIP XYZ, this would just return the same vector
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> X, Z, X </returns>
        public static Vector3 XZX(this Vector3 target)
        {
            return new Vector3(target.x, target.z, target.x);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> X, Z, Y </returns>
        public static Vector3 XZY(this Vector3 target)
        {
            return new Vector3(target.x, target.z, target.y);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> X, Z, Z </returns>
        public static Vector3 XZZ(this Vector3 target)
        {
            return new Vector3(target.x, target.z, target.z);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> Y, X, X </returns>
        public static Vector3 YXX(this Vector3 target)
        {
            return new Vector3(target.y, target.x, target.x);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> Y, X, Y </returns>
        public static Vector3 YXY(this Vector3 target)
        {
            return new Vector3(target.y, target.x, target.y);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> Y, X, Z </returns>
        public static Vector3 YXZ(this Vector3 target)
        {
            return new Vector3(target.y, target.x, target.z);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> Y, Y, X </returns>
        public static Vector3 YYX(this Vector3 target)
        {
            return new Vector3(target.y, target.y, target.x);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> Y, Y, Y </returns>
        public static Vector3 YYY(this Vector3 target)
        {
            return new Vector3(target.y, target.y, target.y);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> Y, Y, Z </returns>
        public static Vector3 YYZ(this Vector3 target)
        {
            return new Vector3(target.y, target.y, target.z);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> Y, Z, X </returns>
        public static Vector3 YZX(this Vector3 target)
        {
            return new Vector3(target.y, target.z, target.x);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> Y, Z, Y </returns>
        public static Vector3 YZY(this Vector3 target)
        {
            return new Vector3(target.y, target.z, target.y);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> Y, Z, Z </returns>
        public static Vector3 YZZ(this Vector3 target)
        {
            return new Vector3(target.y, target.z, target.z);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> Z, X, X </returns>
        public static Vector3 ZXX(this Vector3 target)
        {
            return new Vector3(target.z, target.x, target.x);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> Z, X, Y </returns>
        public static Vector3 ZXY(this Vector3 target)
        {
            return new Vector3(target.z, target.x, target.y);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> Z, X, Z </returns>
        public static Vector3 ZXZ(this Vector3 target)
        {
            return new Vector3(target.z, target.x, target.z);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> Z, Y, X </returns>
        public static Vector3 ZYX(this Vector3 target)
        {
            return new Vector3(target.z, target.x, target.x);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> Z, Y, Y </returns>
        public static Vector3 ZYY(this Vector3 target)
        {
            return new Vector3(target.z, target.y, target.y);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> Z, Y, Z </returns>
        public static Vector3 ZYZ(this Vector3 target)
        {
            return new Vector3(target.z, target.y, target.z);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> Z, Z, X </returns>
        public static Vector3 ZZX(this Vector3 target)
        {
            return new Vector3(target.z, target.z, target.x);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> Z, Z, Y </returns>
        public static Vector3 ZZY(this Vector3 target)
        {
            return new Vector3(target.z, target.z, target.y);
        }
        
        /// <summary>
        /// Gets a new vector with rearranged components.
        /// </summary>
        /// <param name="target"> The target of this swizzle </param>
        /// <returns> Z, Z, Z </returns>
        public static Vector3 ZZZ(this Vector3 target)
        {
            return new Vector3(target.z, target.z, target.z);
        }

        #endregion
        
        
    }
}
