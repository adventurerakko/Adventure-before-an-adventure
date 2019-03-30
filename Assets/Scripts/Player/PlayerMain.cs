using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public class PlayerMain : MonoBehaviour
{
    [HideInInspector] public PlayerInput playerInput;
    public float movementSpeed = 10;
    public float turnSpeed = 90;
    [HideInInspector] public Animator animator;
    [HideInInspector] public StateManager stateManager = new StateManager();
    [SerializeField] public CameraMain cameraMain;
    [HideInInspector] public CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        stateManager.ChangeState(new PlayerIdleState(this));
        Assert.IsNotNull(cameraMain);
    }

    // Update is called once per frame
    void Update()
    {
        print(stateManager.GetCurrentState());
        stateManager.ExecuteCurrentState();
        float cameraRotationAxis = playerInput.CheckCameraRotate();
        if (!Mathf.Approximately(cameraRotationAxis, 0))
        {
            cameraMain.RotateCamera(cameraRotationAxis);
        }
    }
}