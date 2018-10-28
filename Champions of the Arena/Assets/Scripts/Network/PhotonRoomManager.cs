using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRoomManager : Photon.MonoBehaviour
{

    enum States
    {
        START_STATE = 0,
        CONNECTED_TO_SERVER,
        CONNECTED_TO_LOBBY,
        CONNECTED_TO_ROOM,
        BREAK,
        MATCHMAKING
    };

    States state;
    public int maxPlayers;

    
    void Start()
    {
        state = States.START_STATE;
    }

    void OnGUI()
    {
        StartGame();
        //switch (state)
        //{
        //    //Start
        //    case States.START_STATE:
        //        //if (GUI.Button(new Rect(280, 163, 150, 30), "Connect"))
        //        {
        //            PhotonNetwork.ConnectUsingSettings("alpha0.1");

        //            state = States.BREAK;
        //        }
        //        break;
        //    //Connected to Server
        //    case States.CONNECTED_TO_SERVER:
        //        //if (GUI.Button(new Rect(10, 10, 200, 30), "Duel Mode"))
        //        {
        //            PhotonNetwork.JoinLobby();
        //            state = States.BREAK;
        //        }
        //        break;
        //    //Connected to Lobby
        //    case States.CONNECTED_TO_LOBBY:
        //        //if (GUI.Button(new Rect(10, 10, 200, 30), "Find Match"))
        //        {
        //            PhotonNetwork.JoinRandomRoom();
        //            state = States.BREAK;
        //        }
        //        break;
        //    //Connected to Room
        //    case States.CONNECTED_TO_ROOM:

        //        break;
        //    //Break
        //    case States.BREAK:

        //        break;
        //    //Matchmaking
        //    case States.MATCHMAKING:
        //        if (PhotonNetwork.playerList.Length == maxPlayers)
        //        {
        //            StartGame();

        //        }

        //        //GUI.Label(new Rect(10, Screen.height - 50, 200, 30), "Players: " + PhotonNetwork.playerList.Length.ToString() + "/" + maxPlayers.ToString());
        //        break;
        //}
    }

    void OnConnectedToPhoton()
    {
        state = States.CONNECTED_TO_SERVER;
    }

    void OnJoinedLobby()
    {
        state = States.CONNECTED_TO_LOBBY;
    }

    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        state = States.MATCHMAKING;
    }

    void StartGame()
    {
        state = States.CONNECTED_TO_ROOM;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
