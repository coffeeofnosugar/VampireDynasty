using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace VampireDynasty
{
    [BurstCompile]
    public partial struct MonsterMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state) { }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            foreach (var (transform, moveSpeed, moveTarget) in SystemAPI.Query<RefRW<LocalTransform>, MoveSpeed, MoveTarget>()
                         .WithAll<Simulate>())
            {
                var direction = moveTarget.Position - transform.ValueRO.Position;
                direction.z = 0;
                direction = math.normalizesafe(direction);
                
                transform.ValueRW.Position += direction * moveSpeed.Value * deltaTime;
            }
        }
    }
}