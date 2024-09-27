using Sirenix.OdinInspector;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

namespace VampireDynasty
{
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    [BurstCompile]
    public partial struct DamageOnTriggerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<SimulationSingleton>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            state.Dependency = new DamageOnTriggerJob
            {
                DamageOnTriggerLookUp = SystemAPI.GetComponentLookup<DamageOnTrigger>(),
                AlreadyDamagedEntityLookUp = SystemAPI.GetBufferLookup<AlreadyDamagedEntity>(),
                DamageBufferElementLookUp = SystemAPI.GetBufferLookup<DamageBufferElement>(),
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged),
            }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
        }
    }

    [BurstCompile]
    public struct DamageOnTriggerJob : ITriggerEventsJob
    {
        [ReadOnly] public ComponentLookup<DamageOnTrigger> DamageOnTriggerLookUp; // 伤害来源
        [ReadOnly] public BufferLookup<AlreadyDamagedEntity> AlreadyDamagedEntityLookUp; // 伤害来源

        [ReadOnly] public BufferLookup<DamageBufferElement> DamageBufferElementLookUp; // 受击者

        public EntityCommandBuffer ECB;

        public void Execute(TriggerEvent triggerEvent)
        {
            Entity damageDealingEntity; // 伤害来源，可能是飞行物，也有可能是玩家
            Entity damageReceivingEntity; // 受击者

            // 判断触发碰撞的两个物体哪个为伤害来源，哪个为受击者
            if (DamageOnTriggerLookUp.HasComponent(triggerEvent.EntityA)
                && DamageBufferElementLookUp.HasBuffer(triggerEvent.EntityB))
            {
                damageDealingEntity = triggerEvent.EntityA;
                damageReceivingEntity = triggerEvent.EntityB;
            }
            else if (DamageOnTriggerLookUp.HasComponent(triggerEvent.EntityB)
                     && DamageBufferElementLookUp.HasBuffer(triggerEvent.EntityA))
            {
                damageDealingEntity = triggerEvent.EntityB;
                damageReceivingEntity = triggerEvent.EntityA;
            }
            else return; // 触发碰撞的两个物体没有伤害组件直接退出

            // 避免重复伤害
            var alreadyDamagedEntityBuffer = AlreadyDamagedEntityLookUp[damageDealingEntity];
            foreach (var alreadyDamagedEntity in alreadyDamagedEntityBuffer)
            {
                if (alreadyDamagedEntity.Value == damageReceivingEntity) return;
            }

            // 缓存伤害值
            var damage = DamageOnTriggerLookUp[damageDealingEntity].Value;
            ECB.AppendToBuffer(damageReceivingEntity, new DamageBufferElement() { Value = damage });

            // 存储已经伤害过的实体
            ECB.AppendToBuffer(damageDealingEntity, new AlreadyDamagedEntity() { Value = damageReceivingEntity });
        }
    }
}