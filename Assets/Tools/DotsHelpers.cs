using Unity.Mathematics;
using Unity.Transforms;

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
        {
            return math.distancesq(a, b) < thresholdSquared;
        }
    }
}