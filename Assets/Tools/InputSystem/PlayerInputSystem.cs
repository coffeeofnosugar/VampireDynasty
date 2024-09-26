using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Coffee.Tools
{
    [DefaultExecutionOrder(-80)]
    public partial class PlayerInputSystem : Singleton<PlayerInputSystem>, IInitialize, IDisposable,
        InputSystemMap.IUIActions
    {
        private InputSystemMap _playerInput;

        /// <summary>
        /// 注销掉以前订阅的事件，将本对象的事件订阅到输入，避免重复创建对象
        /// </summary>
        public void Init()
        {
            _playerInput = new InputSystemMap();
            _playerInput.ActionGame.SetCallbacks(this);
            _playerInput.UI.SetCallbacks(this);
            _playerInput.Console.SetCallbacks(this);
            _playerInput.MobaGame.SetCallbacks(this);
        }
        
        public void EnableUIInput()
        {
            _playerInput.ActionGame.Disable();
            _playerInput.MobaGame.Disable();
            _playerInput.UI.Enable();
        }

        public void DisableAllInput()
        {
            _playerInput.UI.Disable();
            _playerInput.ActionGame.Disable();
            _playerInput.MobaGame.Disable();
        }

        #region ******************************** 通用 ********************************

        /// <summary>
        /// 左键是否点击下去了
        /// </summary>
        public bool LeftMouseDown => Mouse.current.leftButton.isPressed;

        public bool WKeyDown => Keyboard.current.wKey.isPressed;


        #endregion ******************************** 通用 ********************************


        #region ================================ Cancel ================================

        /// <summary>
        /// UI控制: 键盘ESC、手柄B键和菜单键
        /// </summary>
        public event Action UICancelEvent = delegate { };
        
        public void OnCancel(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                UICancelEvent.Invoke();
            }
        }
        
        #endregion ================================ Cancel ================================

        #region ================================ Point ================================
        
        /// <summary>
        /// UI控制: 鼠标移动时的位置坐标
        /// </summary>
        public event Action<Vector2> UIMousePointEvent = delegate { };
        
        public void OnPoint(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                UIMousePointEvent.Invoke(context.ReadValue<Vector2>());
        }

        #endregion ================================ Point ================================

        #region ================================ Click ================================

        public void OnClick(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }
        
        #endregion ================================ Click ================================

        #region ================================ RightClick ================================
        
        public void OnRightClick(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        #endregion ================================ RightClick ================================
        
        #region ================================ Navigate ================================

        
        /// <summary>
        /// UI控制: 键盘的 W/A/S/D/上/下/左/右、手柄的左摇杆/十字键
        /// </summary>
        public event Action<Vector2> UINavigateEvent = delegate { };

        public void OnNavigate(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                UINavigateEvent.Invoke(context.ReadValue<Vector2>());
            }
        }
        
        #endregion ================================ Navigate ================================
        
        #region ================================ ScrollWheel ================================
        
        public void OnScrollWheel(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        #endregion ================================ ScrollWheel ================================

        #region ================================ Submit ================================
        
        public void OnSubmit(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        #endregion ================================ Submit ================================
        
        
        public void Dispose()
        {
            DisableAllInput();
            _playerInput.Disable();
            _playerInput = null;
        }
    }
}