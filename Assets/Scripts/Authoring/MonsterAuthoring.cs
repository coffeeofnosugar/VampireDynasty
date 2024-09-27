using Sirenix.OdinInspector;
using Unity.Entities;
using UnityEngine;

namespace VampireDynasty
{
    public class MonsterAuthoring : MonoBehaviour
    {
        [Title("AnimationSprites")]
        [SerializeField] private GameObject dieSprite;

        [Title("Properties")]
        [SerializeField] private float moveSpeed;
        
        private class Baker : Baker<MonsterAuthoring>
        {
            public override void Bake(MonsterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<MonsterTag>(entity);
                AddComponent(entity, new MoveSpeed { Value = authoring.moveSpeed });
                AddComponent(entity, new MonsterPrefabs { DieSprite = GetEntity(authoring.dieSprite, TransformUsageFlags.None), });
                AddComponent<MoveTarget>(entity);
            }
        }
    }
}