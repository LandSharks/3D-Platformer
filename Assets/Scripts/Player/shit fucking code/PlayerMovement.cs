using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace shit {
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class PlayerMovement : MonoBehaviour {

        [SerializeField]
        private float playerSpeed = 5f, jumpHeight = 5f, direction = 0f, gravityValue = -9.81f;

        private CharacterController characterController;
        private Vector3 playerVelocity;
        private CapsuleCollider collider;
        private void Start() {
            collider = GetComponent<CapsuleCollider>();
            characterController = GetComponent<CharacterController>();
        }

        public void Move() {
            bool groundedPlayer = characterController.isGrounded;
            Debug.Log("called");
            if(groundedPlayer && playerVelocity.y < 0) {
                playerVelocity.y = 0f;
            }

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            characterController.Move(move * Time.deltaTime * playerSpeed);

            if(move != Vector3.zero) {
                gameObject.transform.forward = move;
            }

            // Changes the height position of the player..
            if(Input.GetButtonDown("Jump") && groundedPlayer) {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            characterController.Move(playerVelocity * Time.deltaTime);
        }

        public void StickToWorldspace(Transform root, Transform camera, ref float directionOut, ref float speedOut, float horizontal, float vertical) {
            Vector3 rootDirection = root.forward;
            Vector3 stickDirection = new Vector3(horizontal, 0, vertical);

            // Get Camera Rotation
            Vector3 CameraDirection = camera.forward;
            CameraDirection.y = 0.0f;
            Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, CameraDirection);

            // Convert input in Worldspace coordinates
            Vector3 moveDirection = referentialShift * stickDirection;
            Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);

            //Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), moveDirection, Color.green);
            //Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), rootDirection, Color.magenta);
            Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), stickDirection, Color.blue);


            float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);

            angleRootToMove /= 180f;

            directionOut = angleRootToMove * playerSpeed; //TODO: Change out to turn speed
        }
    }
}
