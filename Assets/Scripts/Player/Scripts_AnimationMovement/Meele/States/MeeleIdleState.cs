using UnityEngine;

public class MeeleIdleState : MeeleBaseStates
{
    public override void EnterState(MeeleStateManager meele){}

    public override void UpdateState(MeeleStateManager meele)
    {      

        if (Input.GetKeyDown(KeyCode.Alpha1) && ((PlayerInventory.inventory & ChevyPickUp.baseballandFlareGun) == ChevyPickUp.baseballandFlareGun))
        {
            meele.anim.SetTrigger("B");
            meele.SwitchState(meele.MeeleSwitchState);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && ((PlayerInventory.inventory & Rifle.rifleVal) == Rifle.rifleVal))
        {
            meele.anim.SetTrigger("S");     
            meele.SwitchState(meele.GunSwitchState);
        }

    }
}
