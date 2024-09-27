using Unity.Entities;
using UnityEngine;

namespace VampireDynasty
{
    public class DamageOnTriggerAuthoring : MonoBehaviour
    {
        public int damageValue;
        private class Baker : Baker<DamageOnTriggerAuthoring>
        {
            public override void Bake(DamageOnTriggerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new DamageOnTrigger { Value = authoring.damageValue });
                AddComponent<AlreadyDamagedEntity>(entity);
            }
        }
    }
}