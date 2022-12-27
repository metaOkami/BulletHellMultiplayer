using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Spawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public float yPosition;

    private void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(GameObject.Find("Spawn1").transform.position.x, yPosition, GameObject.Find("Spawn1").transform.position.z), Quaternion.identity);
    }
}
