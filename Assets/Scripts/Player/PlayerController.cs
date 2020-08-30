using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(PlayerMotor))]
    public class PlayerController : MonoBehaviour {

        #region Variables
        private PlayerMotor motor;

        //Controller Input
        [SerializeField]
        private string horizontalInput = "Horizontal", verticalInput = "vertical";
        #endregion

        private void Start() {
            motor = GetComponent<PlayerMotor>();
        }

        private void FixedUpdate() { //TODO: Change this to Fixed update?
            HandleInput();
        }

        private void HandleInput() {
            MoveInput();
        }

        private void MoveInput() {
            float horizontal = Input.GetAxis(horizontalInput);
            float vertical = Input.GetAxis(verticalInput);

            motor.MoveCharacter(horizontal, vertical);
        }
    }
}
