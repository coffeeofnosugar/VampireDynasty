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
        [SerializeField] private Material[] materials;
        
        [Title("WeaponSprite")]
        [SerializeField] private GameObject weapon;

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
                var animationMaterials = AddBuffer<PlayerAnimationMaterials>(entity);
                foreach (var material in authoring.materials)
                    animationMaterials.Add(new PlayerAnimationMaterials() { Material = material });
                
                AddComponent(entity, new PlayerWeapon()
                {
                    Weapon = GetEntity(authoring.weapon, TransformUsageFlags.None),
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