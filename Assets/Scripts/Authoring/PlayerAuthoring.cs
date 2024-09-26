using Unity.Entities;
using UnityEngine;

namespace VampireDynasty
{
    public class PlayerAuthoring : MonoBehaviour
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private float moveSpeed;
        private class Baker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent<PlayerTag>(entity);
                AddComponent(entity, new MoveSpeed { Value = authoring.moveSpeed });
                AddComponent(entity, new MaxHealth { Value = authoring.maxHealth });
                AddComponent(entity, new CurrentHealth { Value = authoring.maxHealth });
            }
        }
    }
}