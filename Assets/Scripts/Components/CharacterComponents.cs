using Unity.Entities;

namespace VampireDynasty
{
    public struct MoveSpeed : IComponentData
    {
        public float Value;
    }
    
    public struct MaxHealth : IComponentData
    {
        public int Value;
    }
    
    public struct CurrentHealth : IComponentData
    {
        public int Value;
    }
}