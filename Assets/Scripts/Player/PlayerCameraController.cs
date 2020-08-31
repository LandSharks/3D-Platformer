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

            Debug.DrawRay(this.transform.position, lookDirection, Color.cyan);
            // Setting target position to be the correct offset
            targetPosition = characterOffset + target.up * distanceUp - lookDirection * distanceAway;

            //TODO: Remove debug code...? Maybe that sick end if would be better here
            Debug.DrawRay(target.position, Vector3.up * distanceUp, Color.red);
            Debug.DrawRay(target.position, -1f * target.forward * distanceAway, Color.blue);
            Debug.DrawLine(target.position, targetPosition, Color.magenta);

            // making smooth transition between current position and new position
            //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
            SmoothPosition(this.transform.position, targetPosition);

            // Ensuring camera faces target
            transform.LookAt(target);
        }
        private void SmoothPosition(Vector3 fromPos, Vector3 toPos) {
            this.transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, 0.1f);
        }
    }
}
