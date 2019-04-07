using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMain : MonoBehaviour
{
    [SerializeField] public CameraMain cameraMain;

    [SerializeField] float pickUpDistance = 10f, throwForce = 10f, detectEnemyDistance = 10f;
    GameObject holdingObject = null;

    List<GameObject> enemiesInRange = new List<GameObject>();
    public GameObject nearestEnemy = null;

    Animator animator;
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public CharacterController characterController;
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

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        Assert.IsNotNull(cameraMain);
    }
    void Update()
    {
        characterController.Move(Physics.gravity * Time.deltaTime);
        CheckCameraLock();
        CheckCameraRotate();
        CheckMovement();
        CheckAttack();
        CheckPickThrow();
        CheckTalk();
        CheckDodge();
    }
    void CheckTalk()
    {
        bool talk = playerInput.CheckTalk();
        if (talk)
        {
            DialogueManager.instance.Talk();
        }
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
            holdingObject.GetComponent<Throwable>().ThrowThrowable(throwForce, transform.forward);
            holdingObject = null;
        }
    }
    GameObject GetNearestEnemy()
    {
        GameObject nearestEnemy = null;
        float nearestDistanceToEnemy = Mathf.Infinity;
        Collider[] cols = Physics.OverlapSphere(transform.position, detectEnemyDistance);
        foreach (Collider col in cols)
        {
            if (col && col.tag == "Enemy")
            {
                float distanceToEnemy = Vector3.Distance(transform.position, col.gameObject.transform.position);
                if (distanceToEnemy < nearestDistanceToEnemy)
                {
                    nearestDistanceToEnemy = distanceToEnemy;
                    nearestEnemy = col.gameObject;
                }
            }
        }
        return nearestEnemy;
    }
    void CheckCameraLock()
    {
        bool cameraLock = playerInput.CheckCameraLockInput();
        nearestEnemy = GetNearestEnemy();
        animator.SetBool("IsLockedOn", false);
        animator.SetBool("IsLockedOnWithEnemy", false);
        if (cameraLock)
        {
            if(nearestEnemy != null)
            {
                cameraMain.LockCamera(this.transform.rotation.eulerAngles.y, nearestEnemy);
                animator.SetBool("IsLockedOnWithEnemy", true);
            }
            else
            {
                cameraMain.LockCamera(this.transform.rotation.eulerAngles.y);
                animator.SetBool("IsLockedOn", true);
            }
        }
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
        animator.SetFloat("MovementVector", movementVector.magnitude);
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