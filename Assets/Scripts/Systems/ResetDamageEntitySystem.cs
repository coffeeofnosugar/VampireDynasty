using Unity.Burst;
using Unity.Entities;

namespace VampireDynasty
{
    [BurstCompile]
    public partial struct ResetDamageEntitySystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state) { }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var elapsedTime = SystemAPI.Time.ElapsedTime;
            
            foreach (var alreadyDamagedEntity in SystemAPI.Query<DynamicBuffer<AlreadyDamagedEntity>>().WithAll<Simulate>())
            {
                if (alreadyDamagedEntity.IsEmpty) continue;

                // 当数组只有一个且ResetTime小于当前时间，则直接删除该元素。并且后续判断也没必要做了
                if (alreadyDamagedEntity.Length == 1 && alreadyDamagedEntity[0].ResetTime < elapsedTime)
                {
                    alreadyDamagedEntity.RemoveAt(0);
                    continue;
                }
                
                // ResetTime是递增的，且从0开始查找是最短的
                // 所以我们找到到第一个大于当前时间的索引，删除之前的所有元素
                // 但是当数组中只有一个元素时，永远都达不到我们的需求，所以在前面添加了一个判断
                int index = 0;
                for (int i = 0; i < alreadyDamagedEntity.Length; i++)
                {
                    if (alreadyDamagedEntity[i].ResetTime > elapsedTime)
                    {
                        index = i;
                        break;
                    }
                }
                alreadyDamagedEntity.RemoveRange(0, index);
            }
        }
    }
}