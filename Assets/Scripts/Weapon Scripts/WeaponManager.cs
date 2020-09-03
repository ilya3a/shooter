using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]private WeaponHandler[] weapons;

    private int cuurrentWeaponIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        weapons[cuurrentWeaponIndex].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        SelectWeapon();
    }
    private void SelectWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TurnOnSelectedWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TurnOnSelectedWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TurnOnSelectedWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TurnOnSelectedWeapon(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            TurnOnSelectedWeapon(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            TurnOnSelectedWeapon(5);
        }
    }

    private void TurnOnSelectedWeapon(int weaponIndex)
    {
        if (weaponIndex == cuurrentWeaponIndex)
            return;
        
        weapons[cuurrentWeaponIndex].gameObject.SetActive(false);
        weapons[weaponIndex].gameObject.SetActive(true);
        cuurrentWeaponIndex = weaponIndex;
        
    }
    public WeaponHandler GetCurrentWeapon()
    {
        return weapons[cuurrentWeaponIndex];
    }
}
