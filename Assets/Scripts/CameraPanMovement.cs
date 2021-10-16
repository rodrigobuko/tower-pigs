using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraPanMovement : MonoBehaviour
    {
        [SerializeField] private Vector3 MinCamPosition;
        [SerializeField] private Vector3 MaxCamPosition;
        private Vector3 touchStart;
        [SerializeField] private Camera mainCamera;
        [SerializeField] public bool perspective;
        private bool enable = false;
        private float halfCameraWidth;
        private float halfCameraHeight;

        [SerializeField] private float groundZ;
        [SerializeField] private UnityEvent activateEvent;
        [SerializeField] private UnityEvent deactivateEvent;
        private void Start() {
            enable = true;
            float halfFieldOfView = mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad;
            halfCameraHeight = 10f * Mathf.Tan(halfFieldOfView);
            halfCameraWidth = mainCamera.aspect * halfCameraHeight;
        }

        public void Toggle() {
            if (enable) {
                Deactivate();
            }else
            {
                Activate();
            }
        }
        
        private void Activate() {
            enable = true;
            activateEvent.Invoke();
        }

        private void Deactivate() {
            enable = false;
            deactivateEvent.Invoke();
        }
        
        void Update()
        {
            if (enable) {
                if (perspective) {
                    if (Input.GetMouseButtonDown(0)) {
                        touchStart = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    }

                    if (Input.GetMouseButton(0)) {
                        var direction = touchStart - mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        var newCameraPos = mainCamera.transform.position;
                        newCameraPos += direction;
                        mainCamera.transform.position = Vector3.Lerp( mainCamera.transform.position,new Vector3(
                            Mathf.Clamp(newCameraPos.x, MinCamPosition.x, MaxCamPosition.x),
                            Mathf.Clamp(newCameraPos.y,  transform.position.y, transform.position.y), 
                            Mathf.Clamp(newCameraPos.z, MinCamPosition.z, MaxCamPosition.z)), 0.5f);
                    }
                } else {
                    if (Input.GetMouseButtonDown(0)) {
                        touchStart = GetWorldPositionInPerspective(groundZ);
                    }

                    if (Input.GetMouseButton(0)) {

                        var direction = touchStart - GetWorldPositionInPerspective(groundZ);
                        var newCameraPos = mainCamera.transform.position;
                        newCameraPos = new Vector3(newCameraPos.x + direction.x, newCameraPos.y, newCameraPos.z + direction.y);
                        mainCamera.transform.position = Vector3.Lerp( mainCamera.transform.position,new Vector3(
                            Mathf.Clamp(newCameraPos.x, MinCamPosition.x, MaxCamPosition.x),
                            Mathf.Clamp(newCameraPos.y,  transform.position.y, transform.position.y), 
                            Mathf.Clamp(newCameraPos.z, MinCamPosition.z, MaxCamPosition.z)), 0.5f);
                    }
                }

            }
        }

        private Vector3 GetWorldPositionInPerspective(float z) {
            Ray mousePos = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane ground = new Plane(Vector3.forward, new Vector3(0, 0, z));
            float distance;
            ground.Raycast(mousePos, out distance);
            return mousePos.GetPoint(distance);
        }
        
        
    }