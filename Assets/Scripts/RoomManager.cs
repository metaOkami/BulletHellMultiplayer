using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public string nickname = "Vaporeon es un";
    public string roomName;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.SerializationRate = 8;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = nickname;
    }
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom("sala");
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Te has unido");
        Debug.Log("Hay " + PhotonNetwork.PlayerList.Length + " jugadores en la sala");
        if (PhotonNetwork.IsMasterClient == true)
        {
            PhotonNetwork.LoadLevel(0);
        }
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("Ha habido un fallo al unirte");
    }
}
