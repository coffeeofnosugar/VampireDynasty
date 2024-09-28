using Cysharp.Threading.Tasks;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace VampireDynasty
{
    public class PlayerPositionMap : MonoBehaviour
    {
        private EntityManager entityManager;
        private EntityQuery playerEntityQuery;
        private Entity playerEntity;
        
        private async void Start()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            playerEntityQuery = entityManager.CreateEntityQuery(typeof(PlayerTag));
            while (playerEntity == Entity.Null)
            {
                await UniTask.NextFrame();
                if (playerEntityQuery.IsEmpty) continue;
                playerEntity = playerEntityQuery.GetSingletonEntity();
            }
        }

        private void Update()
        {
            if (!entityManager.Exists(playerEntity)) return;
            transform.position = entityManager.GetComponentData<LocalTransform>(playerEntity).Position;
        }
    }
}