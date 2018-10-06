using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCarry : MonoBehaviour {

    GameObject[] weapons;
    GameObject player;
    bool[] weaponBeingCarried;
    bool carryWeapon = false;
    Dictionary<string, int> playerPerWeapon = new Dictionary<string, int>(); //PlayerName, WeaponIndex

	void Start ()
    {
        player = GameObject.Find("Player");
        weapons = GameObject.FindGameObjectsWithTag("Weapons");
        weaponBeingCarried = new bool[weapons.Length];
        for (int i = 0; i < weapons.Length; i++)
        {
            weaponBeingCarried[i] = false;
        }
	}
	
	void Update ()
    {
        if (carryWeapon)
        {
            weapons[playerPerWeapon[player.name]].transform.localPosition = player.transform.localPosition;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (!carryWeapon)
        {
            Debug.Log(collision.gameObject.name);
            if (collision.gameObject.tag == "Weapons")
            {
                int weaponIndex = int.Parse(collision.gameObject.name);
                playerPerWeapon[player.name] = weaponIndex;
                weaponBeingCarried[weaponIndex] = true;
                carryWeapon = true;
            }
        }
    }

    public void DropWeapon()
    {

    }

}
