using System;
using GameInput.Command;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameInput {
    public class InputHandler : MonoBehaviour {

        [Header("References")]
        [SerializeField] private Cannon cannon;
        private MainMenuUI _mainMenuUI;

        private SimpleControls _controls;
        
        private readonly Command.Command<InputAction.CallbackContext, Cannon> _fireCommand = new FireCommand();
        private readonly Command.Command<Vector2, Cannon> _moveCommand = new MoveCommand(); 
        private readonly Command.Command<InputAction.CallbackContext, MainMenuUI> _openMenuCommand = new OpenMenuCommand();

        public bool inProgress = false;
        public bool isPressed = false;

        private void Awake() {
            _controls = new SimpleControls();
        }

        private void Start() {
            _mainMenuUI = MainMenuUI.Instance;
        }

        private void OnEnable() {
            _controls.Enable();

            _controls.gameplay.fire.performed += ctx => _fireCommand.Execute(ctx, cannon);
            _controls.gameplay.openMenu.performed += ctx => _openMenuCommand.Execute(ctx, _mainMenuUI);
        }

        private void OnDisable() {
            _controls.Disable();
            
            _controls.gameplay.fire.performed -= ctx => _fireCommand.Execute(ctx, cannon);
            _controls.gameplay.openMenu.performed -= ctx => _openMenuCommand.Execute(ctx, _mainMenuUI);
        }

        private void Update() {
            CheckMoveCommand();
        }

        private void CheckMoveCommand() {
            if (_controls.gameplay.move.inProgress) {
                _moveCommand.Execute(_controls.gameplay.move.ReadValue<Vector2>(), cannon);
            }
        }
    }
}