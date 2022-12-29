using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerInventory : MonoBehaviour // switching weapon
{   
    Animator playerAnim; 
  
    [SerializeField] GameObject crossHair;

    [SerializeField] GameObject[] weaponDisplay;
    [SerializeField] GameObject[] activeWeapons;
    [SerializeField] GameObject[] inactiveWeapons;
   
    public static byte inventory = 0; 

    bool isWeaponDisplayed = false;

    public static System.Action itemSwitched;
    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        playerAnim.SetLayerWeight(0, 1f);
        playerAnim.SetLayerWeight(1, 0f);
        playerAnim.SetLayerWeight(2, 0f);
        inventory = 0;     
    }

    void EquipGun()
    {
        itemSwitched?.Invoke();
        activeWeapons[0].SetActive(false);
        activeWeapons[1].SetActive(true);
        inactiveWeapons[1].SetActive(false);

        crossHair.SetActive(true);

        playerAnim.SetLayerWeight(0, 0f);
        playerAnim.SetLayerWeight(1, 1f);
        playerAnim.SetLayerWeight(2, 0f);
        playerAnim.SetInteger("isActiveWeap", 2);
    }

    void UnEquipGun()
    {
        itemSwitched?.Invoke();
        activeWeapons[0].SetActive(false);
        activeWeapons[1].SetActive(false);
        inactiveWeapons[1].SetActive(true);
     
        crossHair.SetActive(false);

        playerAnim.SetLayerWeight(0, 0f);
        playerAnim.SetLayerWeight(1, 1f);
        playerAnim.SetLayerWeight(2, 0f);
        playerAnim.SetInteger("isActiveWeap", 0);

    }
    void EquipMeele()
    {
        itemSwitched?.Invoke();
        activeWeapons[0].SetActive(true);
        activeWeapons[1].SetActive(false);
        inactiveWeapons[0].SetActive(false);

        playerAnim.SetLayerWeight(0, 0f);
        playerAnim.SetLayerWeight(1, 0f);
        playerAnim.SetLayerWeight(2, 1f);
        playerAnim.SetLayerWeight(3, 1f);
        playerAnim.SetInteger("isActiveWeap", 1);
    }

    void UnEquipMeele()
    {
        itemSwitched?.Invoke();
        activeWeapons[0].SetActive(false);
        activeWeapons[1].SetActive(false); 
        inactiveWeapons[0].SetActive(true);

        playerAnim.SetLayerWeight(2, 1f);
        playerAnim.SetLayerWeight(1, 0f);
        playerAnim.SetLayerWeight(0, 0f);
        playerAnim.SetLayerWeight(3, 1f);
        playerAnim.SetInteger("isActiveWeap", 0);
    }

    void SwitchWeaponAnim()
    {
        if (playerAnim.GetInteger("isActiveWeap") == 2)
        {
            EquipMeele();
            inactiveWeapons[1].SetActive(true);
            crossHair.SetActive(false);
        }

        else if (playerAnim.GetInteger("isActiveWeap") == 1)
        {
            EquipGun();
            inactiveWeapons[0].SetActive(true);
        }
    }

    void AddWeaponToInventory(byte itemValue)
    {
        inventory |= itemValue;

        weaponDisplay[0].SetActive(false);
        inactiveWeapons[itemValue - 1].SetActive(true);

        if (!isWeaponDisplayed)
        {
            weaponDisplay[itemValue].SetActive(true);
            isWeaponDisplayed = true;
        }

        //Debug.Log(System.Convert.ToString(inventory, 2).PadLeft(8, '0') + " inventroy");
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchInventoryItem(1);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchInventoryItem(2);
        }
    }

    void SwitchInventoryItem(byte item)
    {
        if (item == 2 && (inventory & Rifle.rifleVal) == Rifle.rifleVal)
        {
            weaponDisplay[1].SetActive(false);
            weaponDisplay[2].SetActive(true);
        }

        if (item == 1 && (inventory & ChevyPickUp.baseballandFlareGun) == ChevyPickUp.baseballandFlareGun)
        {
            weaponDisplay[2].SetActive(false);
            weaponDisplay[1].SetActive(true);
        }
    }


    private void OnEnable()
    {
        IInteractable.weaponPicked += AddWeaponToInventory;      
    }

    private void OnDisable()
    {
        IInteractable.weaponPicked -= AddWeaponToInventory;
    }

}
