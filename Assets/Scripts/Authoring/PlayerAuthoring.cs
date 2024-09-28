using Sirenix.OdinInspector;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace VampireDynasty
{
    public class PlayerAuthoring : MonoBehaviour
    {
        [Title("AnimationSprite")]
        [SerializeField] private GameObject idleSprite;
        [SerializeField] private GameObject runSprite;
        
        [Title("WeaponSprite")]
        [SerializeField] private GameObject swordSprite;

        [Title("Properties")]
        [SerializeField] private float moveSpeed;
        
        [Title("Attack")]
        [SerializeField] private float attackFrequency;
        [SerializeField] private float3 attackOffset;
        
        private class Baker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<PlayerTag>(entity);
                AddComponent(entity, new MoveSpeed { Value = authoring.moveSpeed });
                AddComponent(entity, new PlayerSprites()
                {
                    IdleSprite = GetEntity(authoring.idleSprite, TransformUsageFlags.None),
                    RunSprite = GetEntity(authoring.runSprite, TransformUsageFlags.None),
                    WeaponSprite = GetEntity(authoring.swordSprite, TransformUsageFlags.None),
                });
                AddComponent(entity, new PlayerProperties
                {
                    AttackFrequency = authoring.attackFrequency,
                    AttackOffset = authoring.attackOffset
                });
                AddComponent(entity, new AttackTimer { Value = authoring.attackFrequency });
            }
        }
    }
}