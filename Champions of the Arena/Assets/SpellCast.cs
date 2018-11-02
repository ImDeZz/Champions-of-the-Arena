using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCast : MonoBehaviour
{
    [SerializeField] GameObject Player; 
    [SerializeField] PlayerStats playerStat;
    private Attack attack;
    private PhotonView pv;

    private void Start ()
    {
        pv = GetComponent<PhotonView>();
        attack = GameObject.Find("AttackButton").GetComponent<Attack>(); ;
    }

    private void Update()
    {
        if (playerStat.getHasWeapon())
        {
            if (attack.isAttacking())
            {
                playerStat.Attack();
                attack.attackHappened();
            }
        }
        else
        {
            attack.attackCantHappen();
        }
	}
}
