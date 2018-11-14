using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats :Photon.MonoBehaviour
{
    [SerializeField] string playerName = "asd";
    int playerScore;

    private bool playerHasWeapon = false;
    private string playerWeapon = "";
    public Sprite fireSpell;
    public Sprite basicSpell;
    public Sprite frostSpell;



    public GameObject spellImage;
    GameObject spellImageClone;

    /*//public GameObject spellImage;
	//public Image myImage;
	public Sprite basicSpellImage;
	public Sprite fireSpellImage;
	public Sprite frostSpellImage;*/

    public GameObject prefab;
    GameObject prefabClone;

    public void Start()
    {
        //Minikep letrehozasa
        spellImageClone = Instantiate(spellImage, new Vector2(850, 85), Quaternion.identity) as GameObject;
        spellImageClone.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        spellImageClone.SetActive(false);
        //Halalkepernyo letrehozasa
        prefabClone = Instantiate(prefab, new Vector3(497, 252, 0), Quaternion.identity) as GameObject;
        prefabClone.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        //prefabClone.SetActive(false);
        // GameObject.Find("spellImageFrost").SetActive(false);
        //Score nullazasa
        playerScore = 0;
    }
    

    public void RestartGame()
    {
        Debug.Log("MOST KENE LATSZODNIA");
        GameObject.Find("VirtualJoystick").SetActive(false);
        GameObject.Find("AttackButton").SetActive(false);
        GameObject.Find("spellImage(Clone)").SetActive(false);
        prefabClone.SetActive(true);
        


    }

    public void activeSpell()
    {

    }
    public void setWeapon(string weapon)
    {
        if (weapon.Contains("_"))
        {
            playerWeapon = weapon.Substring(0, weapon.IndexOf("_"));
            if (weapon.Contains("B"))
            {
                Debug.Log("FIRE SPELL");
                Debug.Log("SIKFOSHOGY");
                spellImageClone.GetComponent<Image>().sprite = basicSpell;
                spellImageClone.SetActive(true);
            }
            else
            {
                if (weapon.Contains("Fr"))
                {
                    Debug.Log("Frost SPELL");
                    Debug.Log("SIKFOSHOGY");

                    spellImageClone.GetComponent<Image>().sprite = frostSpell;
                    spellImageClone.SetActive(true);
                }
                else
                {
                    Debug.Log("Basic SPELL");
                    Debug.Log("SIKFOSHOGY");
                    spellImageClone.GetComponent<Image>().sprite = fireSpell;
                    spellImageClone.SetActive(true);
                    // GameObject.Find("spellImageBasic").SetActive(true);
                }
            }
            
        }
        else
        {
            playerWeapon = weapon;

            
        }
        playerHasWeapon = true;
		//myImage.sprite = basicSpellImage;
		//RestartGame();
		//GameObject.Find("spellImage").SetActive(false);

	}

    public void Attack()
    {
        Debug.Log(playerName + " has just attacked with " + playerWeapon);
        playerHasWeapon = false;
        playerWeapon = "";
        spellImageClone.SetActive(false);

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
            RestartGame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log("U dedcollision");
            this.photonView.RPC("killPlayer", PhotonTargets.All, this.photonView.viewID);
            RestartGame();
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
            
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }

}
