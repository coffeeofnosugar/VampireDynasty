using Unity.Entities;
using UnityEngine;

namespace VampireDynasty
{
    public struct PlayerSprites : IComponentData
    {
        public Entity IdleSprite;
        public Entity RunSprite;
    }
}