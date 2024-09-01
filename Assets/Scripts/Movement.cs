using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement {

    private static readonly float G = 9.81f;

    public CharacterController chController;
    public Transform transform;
    public float yVel { get; private set; }
    public Vector3 velocity { get; private set; }
    public float speed;
    public float jumpSpeed;

    private float xVel;
    private float zVel;

    public Movement(Transform transform, CharacterController controller) {

        this.transform = transform;
        chController = controller;
        yVel = 0;
        velocity = new Vector3(0, 0, 0);
    }

    public void Update(float time) {

        xVel = Input.GetAxis("Horizontal") * speed;
        zVel = Input.GetAxis("Vertical") * speed;

        if (chController.isGrounded) {
            if (Input.GetKey(KeyCode.Space)) {
                yVel = jumpSpeed;
            } else {
                yVel = -G * 0.1f;
            }
        } else {
            xVel *= 0.8f;
            zVel *= 0.8f;
            yVel -= G * time;
        }

        velocity = transform.TransformDirection(new Vector3(xVel, yVel, zVel) * time);
        chController.Move(velocity);
    }

}
