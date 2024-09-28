using Coffee.Tools;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace VampireDynasty
{
    public partial class PlayerAnimationSystem : SystemBase
    {
        private Entity Player;
        private SpriteRenderer CurrentSpriteRenderer;
        
        private NativeArray<PlayerAnimationMaterials> Materials;
        
        protected override void OnCreate()
        {
            RequireForUpdate<PlayerTag>();
            RequireForUpdate<PlayerWeapon>();
        }

        protected override void OnStartRunning()
        {
            Player = SystemAPI.GetSingletonEntity<PlayerTag>();
            CurrentSpriteRenderer = SystemAPI.ManagedAPI.GetComponent<SpriteRenderer>(Player);
            
            Materials = SystemAPI.GetBuffer<PlayerAnimationMaterials>(Player).ToNativeArray(Allocator.Persistent);
            UnityObjectRef<Material> temp = Materials[0].Material;
        }

        protected override void OnUpdate()
        {
            var transform = SystemAPI.GetComponentRW<LocalTransform>(Player);

            var playerMovement = PlayerInputSystem.Instance.Movement;
            
            if (math.abs(playerMovement.x) > .1f || math.abs(playerMovement.y) > .1f)
            {
                CurrentSpriteRenderer.material = Materials[1].Material;
                
                // 改变朝向
                if (playerMovement.x > .1f)
                    transform.ValueRW.Rotation = quaternion.RotateY(0f);
                else if (playerMovement.x < -.1f)
                    transform.ValueRW.Rotation = quaternion.RotateY(math.PI);
            }
            else
                CurrentSpriteRenderer.material = Materials[0].Material;
        }
    }
}