using Unity.Entities;

namespace VampireDynasty
{
    public struct PlayerSprites : IComponentData
    {
        public Entity IdleSprite;
        public Entity RunSprite;
        public Entity SwordSprite;
    }

    public struct MonsterPrefabs : IComponentData
    {
        public Entity Ghost;
    }
}