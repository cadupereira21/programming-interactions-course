using UnityEngine.InputSystem;

namespace GameInput.Command {
    public class OpenMenuCommand : Command<InputAction.CallbackContext, MainMenuUI> {
        public override void Execute(InputAction.CallbackContext context, MainMenuUI mainMenuUI) {
            OpenMenu(mainMenuUI);
        }
        
        private static void OpenMenu(MainMenuUI menu) {
            UnityEngine.Debug.Log("[OpenMenuCommand] Opening menu!");
        }
    }
}