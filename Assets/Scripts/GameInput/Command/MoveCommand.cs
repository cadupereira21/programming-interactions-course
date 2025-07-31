using UnityEngine;
using UnityEngine.InputSystem;

namespace GameInput.Command {
    public class MoveCommand : Command<Vector2, Cannon> {
        public override void Execute(Vector2 input, Cannon cannon) {
            if (input != Vector2.zero) {
                cannon.Rotate(input.x);
                cannon.TiltBarrel(input.y);
            }
        }
    }
}