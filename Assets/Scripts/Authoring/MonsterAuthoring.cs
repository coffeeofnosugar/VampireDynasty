using Unity.Entities;
using UnityEngine;

namespace VampireDynasty
{
    public class MonsterAuthoring : MonoBehaviour
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private float moveSpeed;
        
        private class Baker : Baker<MonsterAuthoring>
        {
            public override void Bake(MonsterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<MonsterTag>(entity);
                AddComponent(entity, new MoveSpeed { Value = authoring.moveSpeed });
                AddComponent(entity, new MaxHealth { Value = authoring.maxHealth });
                AddComponent(entity, new CurrentHealth { Value = authoring.maxHealth });
            }
        }
    }
}