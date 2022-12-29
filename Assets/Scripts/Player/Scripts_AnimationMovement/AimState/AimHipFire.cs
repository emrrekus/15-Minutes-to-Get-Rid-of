using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimHipFire : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.anim.SetBool("Aiming",false);
    }
    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKey(KeyCode.Mouse1) && aim.anim.GetInteger("isActiveWeap") == 2)
        {
            aim.SwitchState(aim.Aim);
        }
    }
}
