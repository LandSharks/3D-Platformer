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
            targetPosition = target.position + target.up * distanceUp - target.forward * distanceAway;

            Debug.DrawRay(target.position, Vector3.up * distanceUp, Color.red);
            Debug.DrawRay(target.position, -1f * target.forward * distanceAway, Color.blue);
            Debug.DrawLine(target.position, targetPosition, Color.magenta);

            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
            transform.LookAt(target);
        }
    }
}
