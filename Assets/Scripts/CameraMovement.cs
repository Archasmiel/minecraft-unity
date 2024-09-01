using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement {

    public Transform player;
    public Transform camera;
    public float sideSpeed;
    public float verticalSpeed;

    public float pitch { get; private set; }  // X-axis 
    public float yaw { get; private set; }  // Y-axis 
    private float mouseX;
    private float mouseY;

    public CameraMovement(Transform player, Transform camera) {
        this.player = player;
        this.camera = camera;

        pitch = 0f;
        yaw = 0f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update(float time) {

        mouseX = Input.GetAxis("Mouse X") * verticalSpeed * time;
        mouseY = Input.GetAxis("Mouse Y") * sideSpeed * time;

        yaw += mouseX;
        pitch += mouseY;

        pitch = Mathf.Clamp(pitch, -90f, 90f);

        player.rotation = Quaternion.Euler(0f, yaw, 0f);
        camera.localRotation = Quaternion.Euler(-pitch, 0f, 0f);
    }
    
}
