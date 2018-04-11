using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour {

    [SerializeField] private float weaponSwitchTime;
    [SerializeField] public GameObject primaryWeapon, secondaryWeapon;

    public GameObject currentWeapon;

    private GameObject weaponToEquip;

    private bool canSwitchWeapon = true;

    private bool hasPrimary = false, hasSecondary = true, primaryEquiped = false, secondaryEquiped;

	// Use this for initialization
	void Start () {
        currentWeapon = secondaryWeapon;
	}
	
	// Update is called once per frame
	void Update () {
        CheckForWeapon();  
        SwitchWeapons();
        //Pickup Weapons

        //Drop Weapons
	}

    void SwitchWeapons()
    {
        if (canSwitchWeapon)
        {
            if (hasPrimary && Input.GetKeyDown(KeyCode.Alpha1))
            {
                canSwitchWeapon = false;
                weaponToEquip = primaryWeapon;
                EquipWeapon();
                primaryEquiped = true;
                secondaryEquiped = false;
                StartCoroutine(WaitTillWeaponSwitch());
            }
            else if (hasSecondary && Input.GetKeyDown(KeyCode.Alpha2))
            {
                canSwitchWeapon = false;
                weaponToEquip = secondaryWeapon;
                EquipWeapon();
                primaryEquiped = false;
                secondaryEquiped = true;
                StartCoroutine(WaitTillWeaponSwitch());
            }
        }
    }

    void CheckForWeapon()
    {
        if (primaryWeapon != null)
            hasPrimary = true;
        else
            hasPrimary = false;

        if (secondaryWeapon != null)
            hasSecondary = true;
        else
            hasSecondary = false;
    }

    void EquipWeapon()
    {
        currentWeapon.SetActive(false);
        currentWeapon = weaponToEquip;
        currentWeapon.SetActive(true);
    }

    IEnumerator WaitTillWeaponSwitch()
    {
        yield return new WaitForSeconds(weaponSwitchTime);
        canSwitchWeapon = true;
    }
}
