﻿using Sirenix.OdinInspector;
using Unity.Entities;
using UnityEngine;

namespace VampireDynasty
{
    public class PlayerAuthoring : MonoBehaviour
    {
        [Title("AnimationSprite")]
        [SerializeField] private GameObject _idleSprite;
        [SerializeField] private GameObject _runSprite;
        
        [Title("WeaponSprite")]
        [SerializeField] private GameObject _swordSprite;

        [Title("Properties")]
        [SerializeField] private int maxHealth;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float attackFrequency;
        
        private class Baker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<PlayerTag>(entity);
                AddComponent(entity, new MoveSpeed { Value = authoring.moveSpeed });
                AddComponent(entity, new MaxHealth { Value = authoring.maxHealth });
                AddComponent(entity, new CurrentHealth { Value = authoring.maxHealth });
                AddComponent(entity, new PlayerSprites()
                {
                    IdleSprite = GetEntity(authoring._idleSprite, TransformUsageFlags.None),
                    RunSprite = GetEntity(authoring._runSprite, TransformUsageFlags.None),
                    SwordSprite = GetEntity(authoring._swordSprite, TransformUsageFlags.None),
                });
                AddComponent(entity, new PlayerProperties
                {
                    AttackFrequency = authoring.attackFrequency
                });
                AddComponent(entity, new AttackFrequency { Value = authoring.attackFrequency });
            }
        }
    }
}