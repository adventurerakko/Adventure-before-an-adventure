using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public class PlayerMain : MonoBehaviour
{
    [SerializeField] public CameraMain cameraMain;
    Animator animator;

    [SerializeField] float pickUpDistance = 10f;
    GameObject holdingObject = null;

    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public List<string> animParameterList = new List<string>();
    private string CurrentStateName = "";
    [HideInInspector] public string currentStateName
    {
        get
        {
            return CurrentStateName;
        }
        set
        {
            CurrentStateName = value;
        }
    }

    [SerializeField] EventManager eventManager;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        Assert.IsNotNull(cameraMain);
        Assert.IsNotNull(eventManager);

        foreach(var parameter in animator.parameters)
        {
            if(parameter.type == AnimatorControllerParameterType.Trigger)
            {
                animParameterList.Add(parameter.name);
            }
        }
        eventManager.storyEventDelegate += PrintEvent;
    }
    void PrintEvent()
    {
        print(eventManager.GetStoryEvents("test"));
    }
    void Update()
    {
        CheckCameraLock();
        CheckCameraRotate();
        CheckMovement();
        CheckAttack();
        CheckPickThrow();
        CheckDodge();
    }
    void CheckPickThrow()
    {
        bool pickThrow = playerInput.CheckPickThrowInput();
        if (pickThrow && !holdingObject)
        {
            RaycastHit hit;
            if(Physics.SphereCast(transform.position + new Vector3(0, characterController.height, 0), characterController.height, transform.forward, out hit, pickUpDistance))
            {
                var throwable = hit.transform.gameObject.GetComponent<Throwable>();
                if (throwable)
                {
                    throwable.PickUpThrowable(this.transform);
                    holdingObject = throwable.gameObject;
                }
            }
        }
        else if (pickThrow && holdingObject)
        {
            holdingObject.GetComponent<Throwable>().ThrowThrowable();
            holdingObject = null;
        }
    }
    void CheckCameraLock()
    {
        bool cameraLock = playerInput.CheckCameraLockInput();
        if (cameraLock)
        {
            cameraMain.LockCamera(this.transform.rotation.eulerAngles.y);
        }
        animator.SetBool("IsLockedOn", cameraLock);
    }
    void CheckCameraRotate()
    {
        float cameraRotationAxis = playerInput.CheckCameraRotateInput();
        if (!Mathf.Approximately(cameraRotationAxis, 0))
            cameraMain.RotateCamera(cameraRotationAxis);
    }
    void CheckMovement()
    {
        Vector3 movementVector = playerInput.CheckMoveInput();
        if (movementVector != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            animator.SetFloat("Vertical", movementVector.z);
            animator.SetFloat("Horizontal", movementVector.x);
        }
        else
        {
            animator.SetBool("IsMoving", false);
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
        }
    }
    void CheckAttack()
    {
        if(playerInput.CheckAttackInput())
            animator.SetBool("IsAttack", true);
    }
    void CheckDodge()
    {
        if (playerInput.CheckDodgeInput())
            animator.SetBool("IsDodging", true);
    }
}