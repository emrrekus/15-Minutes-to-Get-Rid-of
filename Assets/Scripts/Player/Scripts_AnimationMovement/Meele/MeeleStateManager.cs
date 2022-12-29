using UnityEngine;

public class MeeleStateManager : MonoBehaviour
{
    MeeleBaseStates currentState;
    public MeeleIdleState MeeleIdleState = new MeeleIdleState();
    public MeeleAttackState MeeleAttackState = new MeeleAttackState();
    public MeeleSwitchState MeeleSwitchState = new MeeleSwitchState();
    public GunSwitchState GunSwitchState = new GunSwitchState();

    [HideInInspector] public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        SwitchState(MeeleIdleState);

    }

    void Update()
    {
        currentState.UpdateState(this);

    }

    public void SwitchState(MeeleBaseStates state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
