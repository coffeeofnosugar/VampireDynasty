using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;


namespace Coffee.Tools
{
    public static class DotsHelpers
    {
        /// <summary>
        /// <para>获取Y方向朝向目标的弧度</para>
        /// <para>
        /// 用法：
        /// <code>
        /// objectPosition.rotation = quaternion.RotateY(GetRotateY(p1, p2));
        /// </code>
        /// </para>
        /// </summary>
        public static float GetRotateY(float3 objectPosition, float3 targetPosition)
        {
            var x = objectPosition.x - targetPosition.x;
            var y = objectPosition.z - targetPosition.z;
            return math.atan2(x, y) + math.PI;      // 返回的是弧度，范围为[0, 2PI]
        }


        public static bool IsCloseTo(float3 a, float3 b, float thresholdSquared = 0.01f)
                => math.distancesq(a, b) < thresholdSquared;

        #region Random

        public static float GetRandomFloat(ref Random random, float min = 0f, float max = 1f)
                => random.NextFloat(min, max);
        
        public static float GetRandomAngle(ref Random random)
                => GetRandomFloat(ref random, 0, math.PI2);

        public static float3 GetRandomPosition(ref Random random, float minRadius, float3 center)
        {
            
            return float3.zero;
        }

        #endregion
    }
}