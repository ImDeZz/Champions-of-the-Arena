using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCast : Photon.MonoBehaviour
{
    [SerializeField] GameObject Player; 
    [SerializeField] PlayerStats playerStat;
    private const float spawnDistance = 3.35f;
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
            else if (weaponType.Contains("WeaponB"))
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

        Vector3 playerPos = playerMoji.transform.position;
        Vector3 playerDirection = playerMoji.transform.right *-1;
        Quaternion playerRotation = playerMoji.transform.rotation;

        Debug.Log(playerDirection);

        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;

        playerRotation = playerRotation * Quaternion.Euler(90f, 0, 90f);
        GameObject Instantiate = PhotonNetwork.InstantiateSceneObject("ProjectileFr", spawnPos, playerRotation, 0, null) as GameObject;
        Instantiate.GetComponent<Rigidbody>().AddRelativeForce((Instantiate.transform.forward * -1) * 300 * Time.deltaTime, ForceMode.VelocityChange);
        Instantiate.tag = "Projectile";
    }

    public void CastBasicSpell()
    {
        playerStat.Attack();
        attack.attackHappened();
        Debug.Log("BASIC SPELL");
        Vector3 playerPos = playerMoji.transform.position;
        Vector3 playerDirection = playerMoji.transform.right * -1;
        Quaternion playerRotation = playerMoji.transform.rotation;

        Debug.Log(playerDirection);

        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;

        playerRotation = playerRotation * Quaternion.Euler(90f, 0, 90f);
        GameObject Instantiate = PhotonNetwork.InstantiateSceneObject("ProjectileB", spawnPos, playerRotation, 0, null) as GameObject;
        Instantiate.GetComponent<Rigidbody>().AddRelativeForce((Instantiate.transform.forward * -1) * 300 * Time.deltaTime, ForceMode.VelocityChange);
        Instantiate.tag = "Projectile";
    }

    public void CastFireSpell()
    {
        playerStat.Attack();
        attack.attackHappened();
        Debug.Log("FIRE SPELL");
        Vector3 playerPos = playerMoji.transform.position;
        Vector3 playerDirection = playerMoji.transform.right * -1;
        Quaternion playerRotation = playerMoji.transform.rotation;

        Debug.Log(playerDirection);

        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;

        playerRotation = playerRotation * Quaternion.Euler(90f, 0, 90f);
        GameObject Instantiate = PhotonNetwork.InstantiateSceneObject("ProjectileFi", spawnPos, playerRotation, 0, null) as GameObject;
        Instantiate.tag = "Projectile";
        Instantiate.GetComponent<Rigidbody>().AddRelativeForce((Instantiate.transform.forward * -1) * 300 * Time.deltaTime, ForceMode.VelocityChange);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
