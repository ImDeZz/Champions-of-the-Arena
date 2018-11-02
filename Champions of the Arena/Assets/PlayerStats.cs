using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] string playerName = "asd";
    private bool playerHasWeapon = false;
    private string playerWeapon = "";

    public void setWeapon(string weapon)
    {
        if (weapon.Contains("_"))
        {
            playerWeapon = weapon.Substring(0, weapon.IndexOf("_"));
        }
        else
        {
            playerWeapon = weapon;
        }
        playerHasWeapon = true;
    }

    public void playerUsedWeapon()
    {
        playerHasWeapon = false;
    }

    public bool getHasWeapon()
    {
        return playerHasWeapon;
    }

    public string getWeaponType()
    {
        return playerWeapon;
    }

    public string getName()
    {
        return playerName;
    }
}
