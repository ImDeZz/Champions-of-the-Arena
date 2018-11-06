using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats :Photon.MonoBehaviour
{
    [SerializeField] string playerName = "asd";
    private bool playerHasWeapon = false;
    private string playerWeapon = "";

    //public DeathMenu deathMenu;
    //public GameObject[] asd;
    public GameObject prefab;
    GameObject prefabClone;
   
    
   

    public void Start()
    {
        prefabClone = Instantiate(prefab, new Vector3(497, 252, 0), Quaternion.identity) as GameObject;
        
        prefabClone.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        prefabClone.SetActive(false);
    }
   

    public void RestartGame()
    {
        Debug.Log("MOST KENE LATSZODNIA");
        GameObject.Find("VirtualJoystick").SetActive(false);
        GameObject.Find("AttackButton").SetActive(false);
        prefabClone.SetActive(true);
       


    }
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
        //RestartGame();
    }

    public void Attack()
    {
        Debug.Log(playerName + " has just attacked with " + playerWeapon);
        playerHasWeapon = false;
        playerWeapon = "";
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Debug.Log("U dedTRIGGER");
            this.photonView.RPC("killPlayer", PhotonTargets.All, this.photonView.viewID);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log("U dedcollision");
            this.photonView.RPC("killPlayer", PhotonTargets.All, this.photonView.viewID);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Projectile")
        {
            Debug.Log("U dedcollider");
            this.photonView.RPC("killPlayer", PhotonTargets.All, this.photonView.viewID);
            RestartGame();
        }
    }

    [PunRPC]
    public void killPlayer(int viewID)
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.Destroy(PhotonView.Find(viewID));
            RestartGame();
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }

}
