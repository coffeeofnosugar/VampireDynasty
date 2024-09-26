using Unity.Entities;
using UnityEngine;

namespace VampireDynasty
{
    public class PlayerSpriteAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject _idleSprite;
        [SerializeField] private GameObject _runSprite;
        
        private class Baker : Baker<PlayerSpriteAuthoring>
        {
            public override void Bake(PlayerSpriteAuthoring authoring)
            {
                var prefabContainerEntity = GetEntity(TransformUsageFlags.None);
                AddComponent(prefabContainerEntity, new PlayerSprites()
                {
                    IdleSprite = GetEntity(authoring._idleSprite, TransformUsageFlags.None),
                    RunSprite = GetEntity(authoring._runSprite, TransformUsageFlags.None)
                });
            }
        }
    }
}