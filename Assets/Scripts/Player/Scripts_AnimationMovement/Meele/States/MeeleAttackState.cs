using UnityEngine;

public class MeeleAttackState :MeeleBaseStates
{
    public override void EnterState(MeeleStateManager meele)
    {
        meele.anim.SetTrigger("M");
    }

    public override void UpdateState(MeeleStateManager meele)
    {
        if (Input.GetKeyUp(KeyCode.Mouse0)) 
        {
            meele.SwitchState(meele.MeeleSwitchState);
        }
    }

}
