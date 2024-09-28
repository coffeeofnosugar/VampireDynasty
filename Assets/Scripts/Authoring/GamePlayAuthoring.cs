using Sirenix.OdinInspector;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace VampireDynasty
{
    public class GamePlayAuthoring : MonoBehaviour
    {
        [SerializeField] private uint randomSeed;

        [Title("Spawn Rule")] 
        [SerializeField] private float spawnInterval;
        [SerializeField] private float spawnWide;
        [SerializeField] private float spawnHigh;

        [Title("MonsterPrefabs")] [SerializeField]
        private GameObject[] monsterPrefabs;

        private class Baker : Baker<GamePlayAuthoring>
        {
            public override void Bake(GamePlayAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new Random() { Value = Unity.Mathematics.Random.CreateFromIndex(authoring.randomSeed) });
                AddComponent<DegreeOfDifficulty>(entity);
                AddComponent(entity, new GamePlayProperties
                {
                    SpawnInterval = authoring.spawnInterval,
                    SpawnWide = authoring.spawnWide,
                    SpawnHigh = authoring.spawnHigh,
                });
                var monsterPrefabsBuffer = AddBuffer<MonsterPrefabs>(entity);
                foreach (var prefab in authoring.monsterPrefabs)
                    monsterPrefabsBuffer.Add(new MonsterPrefabs { Value = GetEntity(prefab, TransformUsageFlags.Dynamic) });

                AddComponent<SpawnMonsterTimer>(entity);
            }
        }
    }
}