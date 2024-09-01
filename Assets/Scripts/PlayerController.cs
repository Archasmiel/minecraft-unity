using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private CharacterController controller;
    private Movement mvmnt;
    private CameraMovement cam;

    [Header("Movement Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;

    [Header("Camera Settings")]
    [SerializeField] private Transform _camera;
    [SerializeField] private float cameraSideSpeed;
    [SerializeField] private float cameraVerticalSpeed;

    void Start() {

        controller = GetComponent<CharacterController>();
        mvmnt = new Movement(transform, controller);
        mvmnt.speed = speed;
        mvmnt.jumpSpeed = jumpSpeed;
        cam = new CameraMovement(transform, _camera);
        cam.sideSpeed = cameraSideSpeed;
        cam.verticalSpeed = cameraVerticalSpeed;
    }

    void Update() {

        mvmnt.Update(Time.deltaTime);
        cam.Update(Time.deltaTime);
    }

}
