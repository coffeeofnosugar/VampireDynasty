using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace VampireDynasty
{
    [BurstCompile]
    public partial struct MonsterTargetingSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            state.Dependency = new MonsterTargetingJob()
            {
                playerTransform = SystemAPI.GetComponent<LocalTransform>(playerEntity),
            }.ScheduleParallel(state.Dependency);
        }
    }

    [BurstCompile]
    public partial struct MonsterTargetingJob : IJobEntity
    {
        [ReadOnly] public LocalTransform playerTransform;
        
        [BurstCompile]
        private void Execute(ref MoveTarget moveTarget)
        {
            moveTarget.Position = playerTransform.Position;
        }
    }
}