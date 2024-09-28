using Unity.Entities;
using UnityEngine.Serialization;

namespace VampireDynasty
{
    public struct PlayerSprites : IComponentData
    {
        public Entity IdleSprite;
        public Entity RunSprite;
        public Entity WeaponSprite;
    }
    
    public struct MonsterPrefabs : IBufferElementData
    {
        public Entity Value;
    }
    
    public struct MonsterSprites : IComponentData
    {
        public Entity DieSprite;
    }
}