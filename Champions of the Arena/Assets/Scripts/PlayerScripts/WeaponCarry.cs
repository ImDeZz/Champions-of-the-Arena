using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCarry : MonoBehaviour {

    [SerializeField] GameObject player;
    [SerializeField] string weapon;
    private PlayerStats playerStatScript;
    PhotonView photonView;

    void Start()
    {
        playerStatScript = player.GetComponent<PlayerStats>();
        photonView = player.GetComponent<PhotonView>();
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Weapons")
        {
            if (!playerStatScript.getHasWeapon())
            {
                photonView.RPC(
                    "hitSomething",
                    PhotonTargets.AllBufferedViaServer,
                    new object[] { hit.gameObject.GetComponent <PhotonView>().viewID }
                );
                weapon = hit.gameObject.name;
                playerStatScript.setWeapon(hit.gameObject.name);
                Debug.Log(playerStatScript.getName() + " has: " + playerStatScript.getWeaponType());
            }
        }
    }

    [PunRPC]
    private void hitSomething(int id)
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.Destroy(PhotonView.Find(id));
        }
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
