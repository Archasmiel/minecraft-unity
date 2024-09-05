using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement {

    public Transform body;
    public Transform head;
    public float sideSpeed;
    public float verticalSpeed;
    public float maxHeadYAngle;

    public float pitch { get; private set; }  // X-axis 
    public float yaw { get; private set; }  // Y-axis 
    public float bodyYaw { get; private set; }  // Y-axis 

    private float mouseX;
    private float mouseY;

    public CameraMovement(Transform body, Transform head) {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        this.body = body;
        this.head = head;

        pitch = 0f;
        yaw = 0f;
        bodyYaw = 0f;
    }

    public void Update(float deltaTime) {
        GetMouseInput(deltaTime);
        UpdateRotation();
        ApplyRotation();
    }

    private void GetMouseInput(float deltaTime) {
        mouseX = Input.GetAxis("Mouse X") * verticalSpeed * deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sideSpeed * deltaTime;
    }

    private void UpdateRotation() {
        pitch = Mathf.Clamp(pitch + mouseY, -90f, 90f);

        yaw += mouseX;
        if (Mathf.Abs(yaw) >= maxHeadYAngle) {
            bodyYaw += Mathf.Sign(yaw) * (Mathf.Abs(yaw) - maxHeadYAngle);
            yaw = Mathf.Clamp(yaw, -maxHeadYAngle, maxHeadYAngle);
        }
    }

    private void ApplyRotation() {
        head.localRotation = Quaternion.Euler(pitch, yaw, 0f);
        body.rotation = Quaternion.Euler(0f, bodyYaw, 0f);
    }
    
}
