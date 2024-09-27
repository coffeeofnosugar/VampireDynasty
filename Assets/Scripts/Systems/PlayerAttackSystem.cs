using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace VampireDynasty
{
    public partial class PlayerAttackSystem : SystemBase
    {
        private Entity Player;
        private float _attackFrequency;
        private Entity _swordSprite;
        
        protected override void OnCreate()
        {
            RequireForUpdate<PlayerTag>();
        }

        protected override void OnStartRunning()
        {
            Player = SystemAPI.GetSingletonEntity<PlayerTag>();
            _attackFrequency = SystemAPI.GetComponentRO<PlayerProperties>(Player).ValueRO.AttackFrequency;
            _swordSprite = SystemAPI.GetComponentRO<PlayerSprites>(Player).ValueRO.SwordSprite;
        }
        
        protected override void OnUpdate()
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            var transform = SystemAPI.GetComponentRO<LocalTransform>(Player);
            var deltaTime = SystemAPI.Time.DeltaTime;

            var attackFrequency = SystemAPI.GetComponentRW<AttackFrequency>(Player);
            attackFrequency.ValueRW.Value -= deltaTime;
            if (attackFrequency.ValueRO.Value <= 0f)
            {
                attackFrequency.ValueRW.Value = _attackFrequency;
                var swordEntity = ecb.Instantiate(_swordSprite);
                
                // 面朝右
                if (math.degrees(math.EulerYXZ(transform.ValueRO.Rotation)).y == 0f)
                    ecb.SetComponent(swordEntity, LocalTransform.FromPosition(transform.ValueRO.Position));
                // 面朝左
                else
                    ecb.SetComponent(swordEntity, LocalTransform.FromPositionRotation(transform.ValueRO.Position, quaternion.RotateY(math.PI)));
            }
            ecb.Playback(EntityManager);
            ecb.Dispose();
        }
    }
}