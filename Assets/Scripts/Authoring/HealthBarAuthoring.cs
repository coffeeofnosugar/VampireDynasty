using Sirenix.OdinInspector;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace VampireDynasty
{
    public class HealthBarAuthoring : MonoBehaviour
    {
        private float3 healthBarOffset;

        private class Baker : Baker<HealthBarAuthoring>
        {
            public override void Bake(HealthBarAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new HealthBarOffset()
                {
                    Value = authoring.healthBarOffset
                });
            }
        }
    }
}