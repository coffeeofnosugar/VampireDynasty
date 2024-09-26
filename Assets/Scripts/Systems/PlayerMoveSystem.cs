using Coffee.Tools;
using Unity.Entities;
using Unity.Transforms;

namespace VampireDynasty
{
    // [DisableAutoCreation]
    public partial class PlayerMoveSystem : SystemBase
    {
        private Entity Player;
        
        protected override void OnCreate()
        {
            RequireForUpdate<PlayerTag>();
        }

        protected override void OnStartRunning()
        {
            PlayerInputSystem.Instance.EnableActionGameInput();

            Player = SystemAPI.GetSingletonEntity<PlayerTag>();
        }

        protected override void OnUpdate()
        {
            var transform = SystemAPI.GetComponentRW<LocalTransform>(Player);
            var speed = SystemAPI.GetComponentRO<MoveSpeed>(Player).ValueRO.Value;
            var deltaTime = SystemAPI.Time.DeltaTime;

            transform.ValueRW.Position += PlayerInputSystem.Instance.Movement.V2ToF3() * speed * deltaTime;


            // if (PlayerInputSystem.Instance.IsJumping)
            // {
            //     var idleEntity = SystemAPI.GetSingletonEntity<PlayerIdleSpriteTag>();
            //     var idleSprite = SystemAPI.ManagedAPI.GetComponent<SpriteRenderer>(idleEntity);
            //     Debug.Log(idleSprite.material.name);
            //     curSprite.material = idleSprite.material;
            // }
        }
    }
}