using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class PhotonNetworkManager : PunBehaviour
{

    private string gameVersion = "alpha0.1";

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(gameVersion);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PhotonNetwork.connectionStateDetailed.ToString());
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer other)
    {
        Debug.Log("OnPhotonPlayerConnected() " + other.NickName); // not seen if you're the player connecting


        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected


            LoadArena();
        }
    }


    public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
    {
        Debug.Log("OnPhotonPlayerDisconnected() " + other.NickName); // seen when other disconnects


        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("OnPhotonPlayerDisonnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected


            LoadArena();
        }
    }

    void LoadArena()
    {
        if (!PhotonNetwork.isMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        Debug.Log("PhotonNetwork : Loading Level : " + PhotonNetwork.room.PlayerCount);
        PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.room.PlayerCount);
    }
}
