using Unity.Entities;
using Unity.Mathematics;

namespace VampireDynasty
{
    public struct PlayerTag : IComponentData { }
    
    public struct MaxHealth : IComponentData
    {
        public int Value;
    }
    
    public struct CurrentHealth : IComponentData
    {
        public int Value;
    }
}