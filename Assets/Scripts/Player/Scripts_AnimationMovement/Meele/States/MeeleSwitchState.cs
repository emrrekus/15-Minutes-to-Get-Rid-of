using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleSwitchState : MeeleBaseStates
{
    

    public override void EnterState(MeeleStateManager meele)
    {
      
    }

    public override void UpdateState(MeeleStateManager meele)
    {
        if (Input.GetKey(KeyCode.Mouse0) && meele.anim.GetInteger("isActiveWeap") == 1)
        {
            meele.SwitchState(meele.MeeleAttackState);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && ((PlayerInventory.inventory & ChevyPickUp.baseballandFlareGun) == ChevyPickUp.baseballandFlareGun))
        {
            meele.anim.SetTrigger("B"); 
            meele.SwitchState(meele.MeeleIdleState);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && ((PlayerInventory.inventory & Rifle.rifleVal) == Rifle.rifleVal))
        {
            meele.anim.SetTrigger("switchTrig");;
            meele.SwitchState(meele.GunSwitchState);
        }
    }


}
