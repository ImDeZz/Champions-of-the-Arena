using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCast : Photon.MonoBehaviour
{
    [SerializeField] GameObject Player; 
    [SerializeField] PlayerStats playerStat;
    private Attack attack;
    private PhotonView pv;
    private GameObject playerMoji;

    private void Start ()
    {
        pv = GetComponent<PhotonView>();
        attack = GameObject.Find("AttackButton").GetComponent<Attack>();
        playerMoji = transform.Find("emoji_kezzel").gameObject;
    }

    private void Update()
    {
        if (playerStat.getHasWeapon())
        {
            if (attack.isAttacking())
            {
                if (pv.isMine)
                {
                    this.photonView.RPC(
                        "SpawnProjectile",
                        PhotonTargets.All,
                        new object[] { playerStat.getWeaponType() });
                }
                
                playerStat.Attack();
                attack.attackHappened();
            }
        }
        else
        {
            attack.attackCantHappen();
        }
	}

    [PunRPC]
    private void SpawnProjectile(string weaponType)
    {
        if (PhotonNetwork.isMasterClient)
        {
            if (weaponType.Contains("WeaponFi"))
            {
                CastFireSpell();
            }
            else if (weaponType.Contains("WeaponFi"))
            {
                CastBasicSpell();
            }
            else if (weaponType.Contains("WeaponFr"))
            {
                CastFrostSpell();
            }
        }   
    }

    public void CastFrostSpell()
    {
        playerStat.Attack();
        attack.attackHappened();
        Debug.Log("FROST SPELL");
        Quaternion q = playerMoji.transform.rotation;
        q = q * Quaternion.Euler(90f, 0, 90f);
        GameObject Instantiate = PhotonNetwork.InstantiateSceneObject("ProjectileFr", playerMoji.transform.position, q, 0, null) as GameObject;
        Instantiate.tag = "Projectile";
    }

    public void CastBasicSpell()
    {
        playerStat.Attack();
        attack.attackHappened();
        Debug.Log("BASIC SPELL");
        Quaternion q = playerMoji.transform.rotation;
        q = q* Quaternion.Euler(90f, 0, 90f);
        GameObject Instantiate = PhotonNetwork.InstantiateSceneObject("ProjectileB", playerMoji.transform.position, q, 0, null) as GameObject;
        Instantiate.tag = "Projectile";
    }

    public void CastFireSpell()
    {
        playerStat.Attack();
        attack.attackHappened();
        Debug.Log("FIRE SPELL");
        Quaternion q = playerMoji.transform.rotation;
        q = q * Quaternion.Euler(90f, 0, 90f);
        GameObject Instantiate = PhotonNetwork.InstantiateSceneObject("ProjectileFi", playerMoji.transform.position, q, 0, null) as GameObject;
        Instantiate.tag = "Projectile";
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
