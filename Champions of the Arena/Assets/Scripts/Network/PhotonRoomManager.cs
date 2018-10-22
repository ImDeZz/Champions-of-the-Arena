using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRoomManager : Photon.MonoBehaviour
{

    int state = 0;
    public int maxPlayers;

    
    void Start()
    {
        state = 0;
    }

    void OnGUI()
    {
        switch (state)
        {
            //Start
            case 0:
                //if (GUI.Button(new Rect(280, 163, 150, 30), "Connect"))
                {
                    PhotonNetwork.ConnectUsingSettings("alpha0.1");
                    
                    state = 4;
                }
                break;
            //Connected to Server
            case 1:
                //if (GUI.Button(new Rect(10, 10, 200, 30), "Duel Mode"))
                {
                    PhotonNetwork.JoinLobby();
                    state = 4;
                }
                break;
            //Connected to Lobby
            case 2:
                //if (GUI.Button(new Rect(10, 10, 200, 30), "Find Match"))
                {
                    PhotonNetwork.JoinRandomRoom();
                    state = 4;
                }
                break;
            //Connected to Room
            case 3:

                break;
            //Break
            case 4:

                break;
            //Matchmaking
            case 5:
                if (PhotonNetwork.playerList.Length == maxPlayers)
                {
                    StartGame();
                    
                }

                //GUI.Label(new Rect(10, Screen.height - 50, 200, 30), "Players: " + PhotonNetwork.playerList.Length.ToString() + "/" + maxPlayers.ToString());
                break;
        }
    }

    void OnConnectedToPhoton()
    {
        state = 1;
    }

    void OnJoinedLobby()
    {
        state = 2;
    }

    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        state = 5;
    }

    void StartGame()
    {
        state = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
