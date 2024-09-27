using Unity.Entities;
using Unity.Mathematics;

namespace VampireDynasty
{
    public struct MonsterTag : IComponentData { }

    public struct MoveTarget : IComponentData
    {
        public float3 Position;
    }
}