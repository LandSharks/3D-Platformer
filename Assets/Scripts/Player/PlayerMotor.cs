using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(Rigidbody))]
    //[RequireComponent(typeof(CapsuleCollider))]
    public class PlayerMotor : MonoBehaviour {

        #region Variables
        private Rigidbody rigidBody;
        //private CapsuleCollider collider;

        [SerializeField]
        private float characterSpeed = 7f, rotationSpeed = 25f, directionOut = 0f;
       
        #endregion

        private void Start() {
            rigidBody = GetComponent<Rigidbody>();

        }
        // TODO: Change move character such that it uses local values based off face instead of camera
        public void MoveCharacter(float horizontal, float vertical) {
            Vector3 stickDirection = new Vector3(horizontal, 0, vertical);
            Vector3 cameraDirection = Camera.main.transform.forward;
            cameraDirection.y = 0.0f;
            Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection);
            Vector3 moveDirection = referentialShift * stickDirection;

            

            rigidBody.velocity = moveDirection * characterSpeed;
            if((moveDirection.x != 0 || moveDirection.z != 0) && Input.GetAxis("Target") < 0.1) {
                transform.rotation = Quaternion.LookRotation(moveDirection);
            }
        }

    }
}
