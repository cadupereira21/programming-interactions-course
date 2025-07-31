using UnityEngine.InputSystem;

namespace GameInput.Command {
    public class FireCommand : Command<InputAction.CallbackContext, Cannon> {
        public override void Execute(InputAction.CallbackContext input, Cannon cannon) {
            cannon.Fire();
        }
    }
}