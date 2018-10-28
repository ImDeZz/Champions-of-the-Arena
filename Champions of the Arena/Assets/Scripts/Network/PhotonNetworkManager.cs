using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class PhotonNetworkManager : Photon.MonoBehaviour
{
    [SerializeField] private Text connectText;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject lobbyCam;

    private string gameVersion = "alpha0.1";

    // Use this for initialization
    void OnEnable()
    {
        PhotonNetwork.ConnectUsingSettings(gameVersion);
        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 30;
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PhotonNetwork.connectionStateDetailed.ToString());
    }


    public virtual void OnJoinedLobby()
    {
        Debug.Log("Joined the lobby");
        PhotonNetwork.JoinOrCreateRoom("NEW", null, null);
    }

    public virtual void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(player.name, new Vector3(((0 * 10) - 25), 5.36f, ((2 * 9) - 30)), spawnPoint.rotation, 0);
        lobbyCam.SetActive(false);
        connectText.text = "";
    }
   
}
