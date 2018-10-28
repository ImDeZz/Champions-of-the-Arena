using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworking : MonoBehaviour {

    [SerializeField] private GameObject playerCam;
    private PhotonView pV;
    // Use this for initialization
    void Start ()
    {
        pV = GetComponent<PhotonView>();

        if (!pV.isMine)
        {
            playerCam.SetActive(false);
        }
        else
        {
            playerCam.SetActive(true);
            Debug.Log("New Player Connected: " + PhotonNetwork.player.ID);
        }
	}
}
