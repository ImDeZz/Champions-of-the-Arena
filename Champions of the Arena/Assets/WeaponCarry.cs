using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCarry : MonoBehaviour {

    GameObject weapon;
    GameObject playerCube;
    bool carryWeapon = false;

	// Use this for initialization
	void Start ()
    {
        playerCube = GameObject.Find("Player");
        weapon = GameObject.Find("Weapon");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (carryWeapon)
        {
            weapon.transform.localPosition = playerCube.transform.localPosition;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == weapon.name)
        {
            carryWeapon = true;
        }
    }
}
