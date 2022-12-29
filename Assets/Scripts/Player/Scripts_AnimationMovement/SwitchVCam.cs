
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchVCam : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private int priorityBoostAmount = 10;
    [SerializeField]
    private Canvas thirdPersonCanvas;
    [SerializeField]
    private Canvas aimCanvas;

    private CinemachineVirtualCamera virtualCamera;
    private InputAction aimAction;

    private void Awake()
    {
        virtualCamera=GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"];
     
    }
    private void OnEnable()
    {       
      aimAction.performed += _ => StartAim();
       // AimState.aimed += StartAim;
       aimAction.canceled += _ => CancelAim();
    }
    private void OnDisable()
    {
        //AimState.aimed -= StartAim;
        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => CancelAim();
    }

    private void StartAim()
    { 
        virtualCamera.Priority+=priorityBoostAmount;
        aimCanvas.enabled = true;
        thirdPersonCanvas.enabled = false;


    } private void CancelAim()
    {
        Debug.Log("can");

        virtualCamera.Priority-=priorityBoostAmount;
        aimCanvas.enabled = false;
        thirdPersonCanvas.enabled = true;

    }
}
