using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AimState : AimBaseState
{   // public static event Action aimed;
   
    public override void EnterState(AimStateManager aim)
    {       
        aim.anim.SetBool("Aiming", true);
    }

    public override void UpdateState(AimStateManager aim)
    {  
        //aimed?.Invoke();

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            aim.SwitchState(aim.hip);
        }
    }

}
