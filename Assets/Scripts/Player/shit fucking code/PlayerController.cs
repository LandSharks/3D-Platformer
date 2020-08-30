using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace shit {
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerController : MonoBehaviour {

        private PlayerMovement movement;
        private Camera camera;
        private bool jump;
        private Animator animator;

        private float direction = 0f, speed = 0f;
        private void Start() {
            movement = GetComponent<PlayerMovement>();
            camera = Camera.main;
        }

        private void Update() {
            if(!jump) {
                jump = Input.GetButton("Jump");
                Debug.Log($"Jump: ${jump}");
            }
        }

        private void FixedUpdate() {
            
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");

            movement.StickToWorldspace(this.transform, camera.transform, ref direction, ref speed, hor, ver);
            movement.Move();
            jump = false;
        }
    }
}
