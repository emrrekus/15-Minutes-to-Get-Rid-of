using UnityEngine;
public class GunSwitchState : MeeleBaseStates
{
    public static event System.Action GunFired; 

    public override void EnterState(MeeleStateManager meele)
    {     
    }

    public override void UpdateState(MeeleStateManager meele)
    {
        if (Input.GetMouseButton(1) /*&& Input.GetMouseButtonDown(0) */&& meele.anim.GetInteger("isActiveWeap") == 2)
        {
            GunFired?.Invoke();
        }
      
            if (Input.GetKeyDown(KeyCode.Alpha2) && ((PlayerInventory.inventory & Rifle.rifleVal) == Rifle.rifleVal))
        {
            meele.anim.SetTrigger("S"); 
            meele.SwitchState(meele.MeeleIdleState);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && ((PlayerInventory.inventory & ChevyPickUp.baseballandFlareGun) == ChevyPickUp.baseballandFlareGun) && meele.anim.GetInteger("isActiveWeap") == 2)
        {
            meele.anim.SetTrigger("switchTrig");
            meele.SwitchState(meele.MeeleSwitchState);
        }
     
    }

}
