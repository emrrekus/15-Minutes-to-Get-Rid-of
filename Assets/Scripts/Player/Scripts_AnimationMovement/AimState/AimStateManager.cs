using UnityEngine;

public class AimStateManager : MonoBehaviour
{
    AimBaseState currentState;
    [SerializeField] GameObject[] rifles;
    public AimHipFire hip = new AimHipFire();

    public AimState Aim = new AimState();

    [HideInInspector] public Vector3 dir;

    [SerializeField]
    private float rotationSpeed = 3f;
    private Transform cameraTransform;

    [HideInInspector] public Animator anim;
    [SerializeField]
    private Transform aimPos;
    [SerializeField]
    private float aimSmoothSpeed;
    [SerializeField]
    private LayerMask aimMask;


    void Start()
    {
        anim = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
      
        SwitchState(hip);
    }

    private void Update()
    {
        currentState.UpdateState(this);        
    }

    void LateUpdate()
    {
        dir = dir.x * cameraTransform.right.normalized + dir.z * cameraTransform.forward.normalized;
        Quaternion rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

    }
    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }


    void setAimRifle()
    {
        rifles[0].SetActive(true);
        rifles[1].SetActive(false);
    }

    void setIdleRifle()
    {
        rifles[1].SetActive(true);
        rifles[0].SetActive(false);
    }
}
