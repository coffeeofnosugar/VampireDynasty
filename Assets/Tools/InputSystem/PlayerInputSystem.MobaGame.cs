using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Coffee.Tools
{
    partial class PlayerInputSystem : InputSystemMap.IMobaGameActions
    {
        public void EnableMobaGameInput()
        {
            _playerInput.MobaGame.Enable();
            _playerInput.UI.Disable();
        }

        #region ================================ SelectMovePosition ================================
        
        
        public event Action SelectMovePositionEvent = delegate { };
        public bool MobaRightWasPressedThisFrame => _playerInput.MobaGame.MobaRight.WasPressedThisFrame();
        
        public void OnMobaRight(InputAction.CallbackContext context)
        {
            SelectMovePositionEvent.Invoke();
        }
        
        #endregion ================================ SelectMovePosition ================================

        #region ================================ ConfirmSkillShotAbility ================================
        
        
        public event Action ConfirmSkillShotAbilityEvent = delegate { };
        public bool MobaLeftWasPressedThisFrame => _playerInput.MobaGame.MobaLeft.WasPressedThisFrame();
        
        public void OnMobaLeft(InputAction.CallbackContext context)
        {
            if (context.phase is InputActionPhase.Started)
            {
                ConfirmSkillShotAbilityEvent.Invoke();
            }
        }

        #endregion ================================ ConfirmSkillShotAbility ================================

        #region ================================ QKey ================================

        
        public event Action QKeyEvent = delegate { };
        public bool QKeyWasPressedThisFrame => _playerInput.MobaGame.QKey.WasPressedThisFrame();

        public void OnQKey(InputAction.CallbackContext context)
        {
            QKeyEvent.Invoke();
        }

        #endregion ================================ QKey ================================

        #region ================================ WKey ================================
        
        
        public event Action WKeyEvent = delegate { };
        public bool WKeyWasPressedThisFrame => _playerInput.MobaGame.WKey.WasPressedThisFrame();
        
        public void OnWKey(InputAction.CallbackContext context)
        {
            if (context.phase is InputActionPhase.Performed)
            {
                WKeyEvent.Invoke();
            }
        }
        #endregion ================================ WKey ================================
    }
}