using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace VampireDynasty
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    [BurstCompile]
    public partial struct DamageApplySystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state) { }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            foreach (var (damageBuffer, currentHealth, entity) in SystemAPI.Query<DynamicBuffer<DamageBufferElement>
                         , RefRW<CurrentHealth>>().WithAll<Simulate>().WithEntityAccess())
            {
                if (damageBuffer.IsEmpty) continue;     // 注意：在Query中不能使用return，不然会跳过剩余的Entity

                // 应用伤害
                var totalDamage = 0;
                foreach (var damage in damageBuffer) 
                    totalDamage += damage.Value;
                currentHealth.ValueRW.Value -= totalDamage;
                damageBuffer.Clear();       // 清除已经应用的伤害

                if (currentHealth.ValueRO.Value <= 0)
                    ecb.AddComponent<DestroyEntityTag>(entity);
            }
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }

    [BurstCompile]
    public partial struct CalculateFrameDamageSystemJob : IJobEntity
    {
        [BurstCompile]
        private void Execute() { }
    }
}