using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement {

    private static readonly float G = 12.5f;
    private static readonly float SLOW_G = -G/10;

    public CharacterController chController;
    public Transform head;
    public float yVel { get; private set; }
    public Vector3 velocity { get; private set; }
    public float speed;
    public float jumpSpeed;
    public float runningModifier;

    private float tempSpeed;

    public float xVel { get; private set; }
    public float zVel { get; private set; }

    public Movement(Transform head, CharacterController controller) {
        this.head = head;
        chController = controller;
        yVel = 0;
        velocity = Vector3.zero;
    }

    public void Update(float deltaTime) {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        xVel = -inputX * speed;
        zVel = -inputZ * speed;

        if (chController.isGrounded) {
            yVel = Input.GetKey(KeyCode.Space) ? jumpSpeed : SLOW_G;
            tempSpeed = speed * (Input.GetKey(KeyCode.LeftShift) ? runningModifier : 1f);
        } else {
            yVel -= G * deltaTime;
            tempSpeed = speed;
        }

        Vector2 moveTemp = new Vector2(xVel, zVel).normalized;
        Vector3 move = new Vector3(moveTemp.x * tempSpeed, yVel, moveTemp.y * tempSpeed);
        move = Quaternion.Euler(0f, head.eulerAngles.y, 0f) * move;

        //velocity = head.TransformDirection(move) * deltaTime;
        chController.Move(move * deltaTime);
    }

    public float GetXZVelocity() {
        return Mathf.Sqrt(xVel*xVel + zVel*zVel);
    }

}
