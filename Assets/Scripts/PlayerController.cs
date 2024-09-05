using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private CharacterController controller;
    private Movement mvmnt;
    private CameraMovement cam;

    [Header("Body Settings")]
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _body;
    [SerializeField] private Animator animator;

    [Header("Movement Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float runningModifier;

    [Header("Camera Settings")]
    [SerializeField] private float cameraXSpeed;
    [SerializeField] private float cameraYSpeed;
    [SerializeField] private float maxHeadYAngle;

    void Start() {
        controller = GetComponent<CharacterController>();

        mvmnt = new Movement(_head, controller);
        mvmnt.speed = speed;
        mvmnt.jumpSpeed = jumpSpeed;
        mvmnt.runningModifier = runningModifier;

        cam = new CameraMovement(_body, _head);
        cam.sideSpeed = cameraXSpeed;
        cam.verticalSpeed = cameraYSpeed;
        cam.maxHeadYAngle = maxHeadYAngle;
    }

    void Update() {

        mvmnt.Update(Time.deltaTime);
        cam.Update(Time.deltaTime);
        animator.SetFloat("Velocity", mvmnt.GetXZVelocity());
    }

}
