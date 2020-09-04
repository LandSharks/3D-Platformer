using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

namespace Player {
    public class PlayerCameraController : MonoBehaviour {
        [SerializeField]
        private float distanceAway, distanceUp, smooth, widescreen = 0.2f, targetingTime = 0.5f;
        [SerializeField]
        private Transform target;

        private Vector3 targetPosition, lookDirection, velocityCamSmooth = Vector3.zero;
        private CameraStates currentState = CameraStates.Default;
        //private BarsEffect barsEffect; TODO: Add effect to indicate camera focus
        public enum CameraStates {
            Default,
            FirstPerson,
            Target,
            Free
        }
        private void LateUpdate() {
            Vector3 characterOffset = target.position + new Vector3(0f, distanceUp, 0f);

            if(Input.GetAxis("Target") > 0.01f) { // TODO: Is there a way to fetch deadzone...?
                currentState = CameraStates.Target;
            } else {
                currentState = CameraStates.Default;
            }

            if(currentState == CameraStates.Target) {
                lookDirection = target.forward;
            } else {
                lookDirection = characterOffset - this.transform.position;
            }

            lookDirection.y = 0f;
            lookDirection.Normalize();

            targetPosition = characterOffset + target.up * distanceUp - lookDirection * distanceAway;
            HandleCollisionWithWalls(characterOffset, ref targetPosition);
            SmoothPosition(this.transform.position, targetPosition);

            // Ensuring camera faces target
            transform.LookAt(target);
        }
        private void SmoothPosition(Vector3 fromPos, Vector3 toPos) {
            this.transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, 0.1f);
        }

        private void HandleCollisionWithWalls(Vector3 fromObject, ref Vector3 toTarget) {

            RaycastHit wallHit = new RaycastHit();
            if(Physics.Linecast(fromObject, toTarget , out wallHit)) {
                Debug.DrawRay(wallHit.point, Vector3.left, Color.red);
                toTarget = new Vector3(wallHit.point.x, toTarget.y, wallHit.point.z);
            }
        }
    }
}
