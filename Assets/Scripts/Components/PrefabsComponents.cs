using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace VampireDynasty
{
    /// <summary>
    /// 存储玩家动画材质，用来切换动画
    /// </summary>
    [InternalBufferCapacity(4)]
    public struct PlayerAnimationMaterials : IBufferElementData
    {
        public UnityObjectRef<Material> Material;
    }
    
    /// <summary>
    /// 存储玩家武器，用来生成武器
    /// </summary>
    public struct PlayerWeapon : IComponentData
    {
        public Entity Weapon;
    }
    
    /// <summary>
    /// 存储怪物的预设，用来生成怪物
    /// </summary>
    [InternalBufferCapacity(10)]
    public struct MonsterPrefabs : IBufferElementData
    {
        public Entity Value;
    }
    
    /// <summary>
    /// 存储怪物动画材质，用来切换动画
    /// </summary>
    [InternalBufferCapacity(4)]
    public struct MonsterAnimationMaterials : IBufferElementData
    {
        public UnityObjectRef<Material> Material;
    }
}