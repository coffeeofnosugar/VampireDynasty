using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace VampireDynasty
{
    public class HealthBarUIReference : ICleanupComponentData
    {
        public GameObject Value;
    }
    
    public struct HealthBarOffset : IComponentData
    {
        public bool IsSet;
        public float3 Value;
    }
}