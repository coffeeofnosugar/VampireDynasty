using System;
using UnityEngine.InputSystem;

namespace Coffee.Tools
{
    partial class PlayerInputSystem : InputSystemMap.IConsoleActions
    {

        public void EnableConsoleInput() => _playerInput.Console.Enable();
        
        public void DisableConsoleInput() => _playerInput.Console.Disable();
        
        
        #region ================================ ConsoleSwitch ================================

        public event Action ConsoleSwitch = delegate { };

        public void OnConsoleSwitch(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                ConsoleSwitch.Invoke();
            }
        }
        
        #endregion ================================ ConsoleSwitch ================================

    }
}