using Unity.Entities;
using UnityEditor.SceneManagement;
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
                AddComponent<MoveSpeed>(entity);
                AddComponent<PlayerMovement>(entity);
                AddComponent(entity, new MaxHealth { Value = authoring.maxHealth });
                AddComponent(entity, new CurrentHealth { Value = authoring.maxHealth });
            }
        }
    }
}