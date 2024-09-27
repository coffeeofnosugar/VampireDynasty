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
            state.RequireForUpdate<SimulationSingleton>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Dependency = new DamageOnTriggerJob
            {
                
            }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
        }
    }

    [BurstCompile]
    public struct DamageOnTriggerJob : ITriggerEventsJob
    {
        // [ReadOnly] public ComponentLookup<PlayerTag> PlayerTagLookUp;
        // [ReadOnly] public ComponentLookup<MonsterTag> MonsterTagLookUp;
        
        public void Execute(TriggerEvent triggerEvent)
        {
            Debug.Log($"ITriggerEventsJob");
        }
    }
}