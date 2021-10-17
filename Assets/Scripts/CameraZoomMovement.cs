using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera mainCamera;
    [SerializeField] private float zoomOutMin;
    [SerializeField] private float zoomOutMax;
    void Start() {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld) {
            if (Input.touchCount == 2) {
                Touch zero = Input.GetTouch(0);
                Touch one = Input.GetTouch(1);

                var touchZeroPrevPos = zero.position - zero.deltaPosition;
                var touchOnePrevPos = one.position - one.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (zero.position - one.position).magnitude;
                float difference = currentMagnitude - prevMagnitude;
                Zoom(difference * 0.01f);
            }
        } else {
            Zoom(Input.GetAxis("Mouse ScrollWheel"));
        }
    }

    void Zoom(float incremment) {
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize - incremment, zoomOutMin, zoomOutMax);
    }
}
