using Unity.Mathematics;
using UnityEngine;

namespace Coffee.Tools
{
    public static class DotsExtensions
    {
        public static float3 V2ToF3(this Vector2 v2) => new float3(v2.x, v2.y, 0);
    }
}