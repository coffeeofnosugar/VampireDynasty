using Cysharp.Threading.Tasks;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace VampireDynasty
{
    public class PlayerPositionMap : MonoBehaviour
    {
        private EntityManager _entityManager;
        private EntityQuery playerEntityQuery;
        private Entity playerEntity;
        
        private async void Start()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            playerEntityQuery = _entityManager.CreateEntityQuery(typeof(PlayerTag));
            while (playerEntity == Entity.Null)
            {
                await UniTask.NextFrame();
                if (playerEntityQuery.IsEmpty) continue;
                playerEntity = playerEntityQuery.GetSingletonEntity();
            }
        }

        private void Update()
        {
            if (playerEntity == Entity.Null) return;
            transform.position = _entityManager.GetComponentData<LocalTransform>(playerEntity).Position;
        }
    }
}