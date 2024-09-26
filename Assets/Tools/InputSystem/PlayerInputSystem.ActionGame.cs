using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Coffee.Tools
{
    public partial class PlayerInputSystem : InputSystemMap.IActionGameActions
    {
        public void EnableActionGameInput()
        {
            _playerInput.ActionGame.Enable();
            _playerInput.UI.Disable();
        }

        #region ================================ Move ================================

        // 将delegate分配给事件，可以跳过null检查
        public Vector2 Movement { get; private set; }
        public event Action<Vector2> MoveEvent = delegate { };
        
        public void OnMove(InputAction.CallbackContext context)
        {
            Movement = context.ReadValue<Vector2>();
            MoveEvent.Invoke(Movement);
        }

        #endregion ================================ Move ================================

        #region ================================ Look ================================

        public event Action<Vector2> LookEvent = delegate { };
        
        public void OnLook(InputAction.CallbackContext context)
        {
            // 这里需要判断一下设备是不是鼠标
            // 因为对于鼠标而言，我们不需要考虑帧的持续时间，手柄要考虑的就很多了
            LookEvent.Invoke(context.ReadValue<Vector2>());
        }

        #endregion ================================ Look ================================

        #region ================================ Run ================================

        public bool IsRunning { get; private set; }
        public event Action<bool> RunEvent = delegate { };
        
        public void OnRun(InputAction.CallbackContext context)
        {
            if (context.phase is InputActionPhase.Performed or InputActionPhase.Canceled)
            {
                IsRunning = context.ReadValueAsButton();
                RunEvent.Invoke(IsRunning);
            }
        }
        
        #endregion ================================ Run ================================

        #region ================================ Jump ================================

        
        public event Action<bool> JumpEvent = delegate { };
        
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase is InputActionPhase.Performed or InputActionPhase.Canceled)
            {
                var isJumping = context.ReadValueAsButton();
                JumpEvent.Invoke(isJumping);
            }
        }

        #endregion ================================ Jump ================================

        #region ================================ Crouch ================================

        
        public event Action<bool> CrouchEvent = delegate { };
        
        public void OnCrouch(InputAction.CallbackContext context)
        {
            if (context.phase is InputActionPhase.Performed or InputActionPhase.Canceled)
            {
                var isCrouching = context.ReadValueAsButton();
                CrouchEvent.Invoke(isCrouching);
            }
        }

        #endregion ================================ Crouch ================================

        #region ================================ SwitchWeapon ================================

        public event Action<int> SwitchWeaponEvent = delegate { };
        
        public void OnSwitchWeapon(InputAction.CallbackContext context)
        {
            if (context.phase is InputActionPhase.Performed)
            {
                int index = int.TryParse(context.control.name, out index) ? index : 0;
                SwitchWeaponEvent.Invoke(index);
            }
        }

        #endregion ================================ SwitchWeapon ================================

        #region ================================ Attack ================================

        public event Action AttackEvent = delegate { };
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.phase is InputActionPhase.Performed or InputActionPhase.Canceled)
            {
                AttackEvent.Invoke();
            }
        }
        
        #endregion ================================ Attack ================================

        #region ================================ Pause ================================
        
        public event Action PauseEvent = delegate { };
        
        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                PauseEvent.Invoke();
            }
        }
        
        #endregion ================================ Pause ================================
        
    }
}