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
        private float characterSpeed = 5f, rotationSpeed = 3f;
       
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
        }
    }
}
