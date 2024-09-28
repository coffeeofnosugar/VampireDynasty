using Sirenix.OdinInspector;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace VampireDynasty
{
    public class DamageReceiveAuthoring : MonoBehaviour
    {
        [SerializeField] private int maxHealth;
        private class Baker : Baker<DamageReceiveAuthoring>
        {
            public override void Bake(DamageReceiveAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new MaxHealth { Value = authoring.maxHealth });
                AddComponent(entity, new CurrentHealth { Value = authoring.maxHealth });
                AddComponent<DamageBufferElement>(entity);
            }
        }
    }
}