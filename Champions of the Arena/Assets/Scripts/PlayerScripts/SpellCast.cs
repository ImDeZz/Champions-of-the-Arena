using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCast : Photon.MonoBehaviour
{
    [SerializeField] GameObject Player; 
    [SerializeField] PlayerStats playerStat;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject ice;
    [SerializeField] private GameObject basic;
    private const float spawnDistance = 2f;
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
                    //    this.photonView.RPC(
                    //        "SpawnProjectile",
                    //        PhotonTargets.All,
                    //        new object[] { playerStat.getWeaponType() });
                    SpawnProjectile(playerStat.getWeaponType());
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

    private void SpawnProjectile(string weaponType)
    {
        var ping = PhotonNetwork.GetPing();
        Vector3 playerPos = playerMoji.transform.position;
        Vector3 playerDirection = playerMoji.transform.right * -1;
        Quaternion playerRotation = playerMoji.transform.rotation;
        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
        playerRotation = playerRotation * Quaternion.Euler(90f, 0, 90f);

        if (weaponType.Contains("WeaponFi"))
        {
            this.photonView.RPC("CastFireSpell", PhotonTargets.All, spawnPos, playerRotation);
        }
        else if (weaponType.Contains("WeaponB"))
        {
            this.photonView.RPC("CastBasicSpell", PhotonTargets.All, spawnPos, playerRotation);
        }
        else if (weaponType.Contains("WeaponFr"))
        {
            this.photonView.RPC("CastFrostSpell", PhotonTargets.All, spawnPos, playerRotation);
        }  
    }

    [PunRPC]
    public void CastFrostSpell(Vector3 spawnPos, Quaternion playerRotation)
    {
        Debug.Log("FROST SPELL");
        GameObject tmp = Instantiate(ice, spawnPos, playerRotation) as GameObject;
        tmp.tag = "Projectile";
        
    }

    [PunRPC]
    public void CastBasicSpell(Vector3 spawnPos, Quaternion playerRotation)
    {
        Debug.Log("BASIC SPELL");
        GameObject tmp = Instantiate(basic, spawnPos, playerRotation) as GameObject;
        tmp.tag = "Projectile";
    }

    [PunRPC]
    public void CastFireSpell(Vector3 spawnPos, Quaternion playerRotation)
    {
        Debug.Log("FIRE SPELL");
        GameObject tmp = Instantiate(fire, spawnPos + Vector3.forward * -2, playerRotation) as GameObject;
        GameObject tmp1 = Instantiate(fire, spawnPos + Vector3.forward * 2, playerRotation) as GameObject;
        tmp.tag = "Projectile";
        tmp1.tag = "Projectile";
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
