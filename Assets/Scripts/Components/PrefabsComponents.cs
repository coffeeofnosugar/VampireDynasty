using Unity.Entities;
using UnityEngine;

namespace VampireDynasty
{
    public struct PlayerSprites : IComponentData
    {
        public Entity IdleSprite;
        public Entity RunSprite;
    }

    public struct MonsterPrefabs : IComponentData
    {
        public Entity Ghost;
    }
}