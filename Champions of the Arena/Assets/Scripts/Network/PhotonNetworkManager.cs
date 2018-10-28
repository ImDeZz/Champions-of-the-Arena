using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using System;

public class PhotonNetworkManager : Photon.MonoBehaviour
{
    [SerializeField] private Text connectText;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject lobbyCam;
    [SerializeField] private string gameVersion = "alpha0.1";
    [SerializeField] private byte weaponCount ;
    [SerializeField] private byte mapDiv;
    System.Random r = new System.Random();

    void OnEnable()
    {
        PhotonNetwork.ConnectUsingSettings(gameVersion);
        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 30;
    }

    public virtual void OnJoinedLobby()
    {
        Debug.Log("Joined the lobby");
        PhotonNetwork.JoinOrCreateRoom("NEW", null, null);
    }

    public virtual void OnJoinedRoom()
    {
        int x, y;
        x = r.Next(0, 5);
        y = r.Next(1, 6);

        Debug.Log(player.name);

        //For debugging 
        //for (int i = 0; i < 6; i++)
        //{
        //    for(int j = 1; j < 7; j++)
        //    {
        //        PhotonNetwork.Instantiate(player.name, new Vector3(((i * 10) - 25), 5.36f, (((j * 9) - 30))), spawnPoint.rotation, 0);
        //    }
        //}


        PhotonNetwork.Instantiate(player.name, new Vector3(((x * 10) - 25), 5.36f, (((y * 9) - 30))), spawnPoint.rotation, 0);
        lobbyCam.SetActive(false);
        connectText.text = "";
    }
}
