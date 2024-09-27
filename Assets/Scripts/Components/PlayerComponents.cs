using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.Serialization;

namespace VampireDynasty
{
    public struct PlayerTag : IComponentData { }

    public struct PlayerProperties : IComponentData
    {
        public float AttackFrequency;
        public float3 AttackOffset;
    }
    
    public struct  AttackTimer : IComponentData
    {
        public float Value;
    }
}