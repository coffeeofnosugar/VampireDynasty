using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.Serialization;

namespace VampireDynasty
{
    public struct PlayerTag : IComponentData { }

    public struct PlayerProperties : IComponentData
    {
        public float AttackFrequency;
    }
    
    public struct  AttackFrequency : IComponentData
    {
        public float Value;
    }
}