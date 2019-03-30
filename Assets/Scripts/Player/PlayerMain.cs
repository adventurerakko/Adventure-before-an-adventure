using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public class PlayerMain : MonoBehaviour
{
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] Animator animator;
    [SerializeField] public CameraMain cameraMain;
    [HideInInspector] public CharacterController characterController;
    [HideInInspector]public string currentStateName = "";
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        Assert.IsNotNull(cameraMain);
    }

    // Update is called once per frame
    void Update()
    {
        float cameraRotationAxis = playerInput.CheckCameraRotate();
        if (!Mathf.Approximately(cameraRotationAxis, 0))
        {
            cameraMain.RotateCamera(cameraRotationAxis);
        }
    }
}