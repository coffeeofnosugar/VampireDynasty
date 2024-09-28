using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace VampireDynasty
{
    [BurstCompile]
    public partial struct SpawnMonsterSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            state.Dependency = new SpawnMonsterJob()
            {
                deltaTime = SystemAPI.Time.DeltaTime,
                playerTransform = SystemAPI.GetComponent<LocalTransform>(playerEntity).Position,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel(state.Dependency);
        }
    }

    [BurstCompile]
    public partial struct SpawnMonsterJob : IJobEntity
    {
        public float deltaTime;
        public float3 playerTransform;

        public EntityCommandBuffer.ParallelWriter ECB;
        
        [BurstCompile]
        private void Execute(ref Random random, ref SpawnMonsterTimer spawnMonsterTimer,
            in GamePlayProperties gamePlayProperties, in DynamicBuffer<MonsterPrefabs> monsterPrefabs,
            in DegreeOfDifficulty degreeOfDifficulty, [ChunkIndexInQuery] int sortKey)
        {
            spawnMonsterTimer.Value -= deltaTime;
            if (spawnMonsterTimer.Value >= 0) return;
            spawnMonsterTimer.Value += gamePlayProperties.SpawnInterval;

            // 获取屏幕外的随机一个位置
            var spawnPosition = new float3(
                random.Value.NextFloat(-gamePlayProperties.SpawnWide, gamePlayProperties.SpawnWide),
                random.Value.NextFloat(-gamePlayProperties.SpawnHigh, gamePlayProperties.SpawnHigh),
                0f);
            spawnPosition += playerTransform;
            
            // 怪物朝向，如果在左边，则朝向为0，在右边，则朝向为PI
            var spawnRotation = quaternion.RotateY(spawnPosition.x < 0 ? 0f : math.PI);

            var monsterEntity = ECB.Instantiate(sortKey, monsterPrefabs[degreeOfDifficulty.Value].Value);
            ECB.SetComponent(sortKey, monsterEntity, LocalTransform.FromPositionRotation(spawnPosition, spawnRotation));
        }
    }
}