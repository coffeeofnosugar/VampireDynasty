using Sirenix.OdinInspector;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace VampireDynasty
{
    public class PrefabContainerAuthoring : MonoBehaviour
    {
        private class Baker : Baker<PrefabContainerAuthoring>
        {
            public override void Bake(PrefabContainerAuthoring authoring)
            {
                var prefabContainerEntity = GetEntity(TransformUsageFlags.None);
            }
        }
    }
}