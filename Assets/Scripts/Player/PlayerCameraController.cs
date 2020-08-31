using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class PlayerCameraController : MonoBehaviour {
        [SerializeField]
        private float distanceAway, distanceUp, smooth;
        [SerializeField]
        private Transform target;
        [SerializeField]
        private Vector3 offset = new Vector3(0f, 1.5f, 0f);

        private Vector3 targetPosition, lookDirection, velocityCamSmooth = Vector3.zero;

        private void Start() {
            // Tutorial used GameObject.FindWithTag("Player").transform
        }

        private void LateUpdate() {
            Vector3 characterOffset = target.position + offset;

            lookDirection = characterOffset - this.transform.position;
            lookDirection.y = 0f;
            lookDirection.Normalize();

            // Setting target position to be the correct offset
            targetPosition = characterOffset + target.up * distanceUp - lookDirection * distanceAway;

            // making smooth transition between current position and new position
            SmoothPosition(this.transform.position, targetPosition);

            // Ensuring camera faces target
            transform.LookAt(target);
        }
        private void SmoothPosition(Vector3 fromPos, Vector3 toPos) {
            this.transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, 0.1f);
        }
    }
}
