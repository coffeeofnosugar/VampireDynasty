using Sirenix.OdinInspector;
using Unity.Entities;
using UnityEngine;

namespace VampireDynasty
{
    public class PlayerSpriteAuthoring : MonoBehaviour
    {
        [Title("PlayerSprite")]
        [SerializeField] private GameObject _idleSprite;
        [SerializeField] private GameObject _runSprite;
        
        [Title("MonsterPrefabs")]
        [SerializeField] private GameObject _ghost;
        
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
                AddComponent(prefabContainerEntity, new MonsterPrefabs
                {
                    Ghost = GetEntity(authoring._ghost, TransformUsageFlags.Dynamic),
                });
            }
        }
    }
}