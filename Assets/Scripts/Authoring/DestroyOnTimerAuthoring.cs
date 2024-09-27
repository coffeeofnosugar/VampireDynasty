using Unity.Entities;
using UnityEngine;

namespace VampireDynasty
{
    public class DestroyOnTimerAuthoring : MonoBehaviour
    {
        public float DestroyOnTimer;
        
        private class Baker : Baker<DestroyOnTimerAuthoring>
        {
            public override void Bake(DestroyOnTimerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new DestroyTimer { Value = authoring.DestroyOnTimer });
            }
        }
    }
}