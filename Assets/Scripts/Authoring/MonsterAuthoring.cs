using Sirenix.OdinInspector;
using Unity.Entities;
using UnityEngine;

namespace VampireDynasty
{
    public class MonsterAuthoring : MonoBehaviour
    {
        [Title("AnimationSprites")]
        [SerializeField] private Material[] materials;

        [Title("Properties")]
        [SerializeField] private float moveSpeed;
        
        private class Baker : Baker<MonsterAuthoring>
        {
            public override void Bake(MonsterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<MonsterTag>(entity);
                AddComponent(entity, new MoveSpeed { Value = authoring.moveSpeed });
                var animationMaterials = AddBuffer<MonsterAnimationMaterials>(entity);
                foreach (var material in authoring.materials)
                    animationMaterials.Add(new MonsterAnimationMaterials(){ Material = material });
                AddComponent<MoveTarget>(entity);
            }
        }
    }
}