using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace VampireDynasty
{
    [BurstCompile]
    public partial struct DestroyOnTimerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state) { }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            foreach (var (destroyTimer, entity) in SystemAPI.Query<RefRW<DestroyTimer>>().WithNone<DestroyEntityTag>().WithAll<Simulate>().WithEntityAccess())
            {
                destroyTimer.ValueRW.Value -= deltaTime;
                if (destroyTimer.ValueRO.Value <= 0f)
                {
                    ecb.AddComponent<DestroyEntityTag>(entity);
                }
            }
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}