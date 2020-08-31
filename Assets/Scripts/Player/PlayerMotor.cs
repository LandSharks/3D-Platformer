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
        private float characterSpeed = 5f, rotationSpeed = 3f, directionOut = 0f;
       
        #endregion

        private void Start() {
            rigidBody = GetComponent<Rigidbody>();

        }
        public void MoveCharacter(float horizontal, float vertical) {
            Vector3 direction = new Vector3(horizontal * characterSpeed , 0f, vertical * characterSpeed);
            direction = Camera.main.transform.TransformDirection(direction);
            direction.y = 0f;
            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, direction.normalized, rotationSpeed * Time.deltaTime, .1f);
            Quaternion newRotation = Quaternion.LookRotation(desiredForward);

            rigidBody.velocity = direction;
            transform.rotation = newRotation;
            StickToWorldspace(this.transform, Camera.main.transform, ref directionOut, ref characterSpeed, horizontal, vertical);
        }

        //TODO: Maybe move this somewhere else? is this even needed?
        public void StickToWorldspace(Transform root, Transform camera, ref float directionOut, ref float speedOut, float horizontal, float vertical) {
            Vector3 rootDirection = root.forward;
            Vector3 stickDirection = new Vector3(horizontal, 0, vertical);

            // Get Camera Rotation
            Vector3 CameraDirection = camera.forward;
            CameraDirection.y = 0.0f;
            Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, CameraDirection);

            // Convert input in Worldspace coordinates
            Vector3 moveDirection = referentialShift * stickDirection;
            Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection); //direction of turning the stick

            Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), moveDirection, Color.green); // Offset from camera (where to turn towards)
            Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), rootDirection, Color.magenta); //Value we care about?
            Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), stickDirection, Color.blue); //Stick direction


            float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f); //turn angle between move direction (green) and root (purple)

            angleRootToMove /= 180f;

            directionOut = angleRootToMove * rotationSpeed; //TODO: Change out to turn speed
        }
    }
}
