using Unity.Entities;
using UnityEngine;

namespace VampireDynasty
{
    public partial class PlayerMoveSystem : SystemBase
    {
        private Entity Player;
        
        protected override void OnCreate()
        {
            RequireForUpdate<PlayerTag>();
        }

        protected override void OnStartRunning()
        {
            Player = SystemAPI.GetSingletonEntity<PlayerTag>();
        }

        protected override void OnUpdate()
        {
            
        }
    }
}