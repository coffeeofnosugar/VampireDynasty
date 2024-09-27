using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace VampireDynasty
{
    public partial class PlayerAttackSystem : SystemBase
    {
        private Entity Player;
        private PlayerProperties _playerProperties;
        private Entity _swordSprite;
        
        protected override void OnCreate()
        {
            RequireForUpdate<PlayerTag>();
        }

        protected override void OnStartRunning()
        {
            Player = SystemAPI.GetSingletonEntity<PlayerTag>();
            _playerProperties = SystemAPI.GetComponentRO<PlayerProperties>(Player).ValueRO;
            _swordSprite = SystemAPI.GetComponentRO<PlayerSprites>(Player).ValueRO.SwordSprite;
        }
        
        protected override void OnUpdate()
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            var transform = SystemAPI.GetComponentRO<LocalTransform>(Player);
            var deltaTime = SystemAPI.Time.DeltaTime;

            var attackTimer = SystemAPI.GetComponentRW<AttackTimer>(Player);
            attackTimer.ValueRW.Value -= deltaTime;
            if (attackTimer.ValueRO.Value <= 0f)
            {
                attackTimer.ValueRW.Value = _playerProperties.AttackFrequency;
                var swordEntity = ecb.Instantiate(_swordSprite);
                
                // 面朝右
                if (math.degrees(math.EulerYXZ(transform.ValueRO.Rotation)).y == 0f)
                {
                    var spawnPosition = transform.ValueRO.Position + _playerProperties.AttackOffset;
                    ecb.SetComponent(swordEntity, LocalTransform.FromPosition(spawnPosition));
                }
                // 面朝左
                else
                {
                    var spawnPosition = transform.ValueRO.Position - _playerProperties.AttackOffset;
                    ecb.SetComponent(swordEntity,
                        LocalTransform.FromPositionRotation(spawnPosition, quaternion.RotateY(math.PI)));
                }
            }
            ecb.Playback(EntityManager);
            ecb.Dispose();
        }
    }
}