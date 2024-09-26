using Coffee.Tools;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using VHierarchy.Libs;

namespace VampireDynasty
{
    public partial class PlayerAnimationSystem : SystemBase
    {
        private Entity Player;
        private SpriteRenderer CurrentSpriteRenderer;
        
        private Material IdleMaterial;
        private Material RunMaterial;
        
        protected override void OnCreate()
        {
            RequireForUpdate<PlayerTag>();
            RequireForUpdate<PlayerSprites>();
        }

        protected override void OnStartRunning()
        {
            Player = SystemAPI.GetSingletonEntity<PlayerTag>();
            CurrentSpriteRenderer = SystemAPI.ManagedAPI.GetComponent<SpriteRenderer>(Player);
            
            var playerSpritesEntity = SystemAPI.GetSingletonRW<PlayerSprites>();
            IdleMaterial = SystemAPI.ManagedAPI.GetComponent<SpriteRenderer>(playerSpritesEntity.ValueRO.IdleSprite).material;
            RunMaterial = SystemAPI.ManagedAPI.GetComponent<SpriteRenderer>(playerSpritesEntity.ValueRO.RunSprite).material;
        }

        protected override void OnUpdate()
        {
            var transform = SystemAPI.GetComponentRW<LocalTransform>(Player);

            var playerMovement = PlayerInputSystem.Instance.Movement;
            
            if (math.abs(playerMovement.x) > .1f || math.abs(playerMovement.y) > .1f)
            {
                CurrentSpriteRenderer.material = RunMaterial;
                
                // 改变朝向
                if (playerMovement.x > .1f)
                    transform.ValueRW.Rotation = quaternion.RotateY(0f);
                else if (playerMovement.x < -.1f)
                    transform.ValueRW.Rotation = quaternion.RotateY(math.PI);
            }
            else
            {
                CurrentSpriteRenderer.material = IdleMaterial;
            }
        }
    }
}