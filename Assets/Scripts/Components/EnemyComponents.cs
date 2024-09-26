using Unity.Entities;
using Unity.Mathematics;

namespace VampireDynasty
{
    public struct EnemyTag : IComponentData { }

    public struct MoveTarget : IComponentData
    {
        public float2 Position;
    }
}