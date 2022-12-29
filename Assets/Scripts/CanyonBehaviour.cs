using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

//Este script contiente las funcionalidades de los cañones, tanto el disparo, destruccion del mismo
//y la sincronizacion de este en linea.

public class CanyonBehaviour : MonoBehaviourPun
{
    public static ObjectPool bulletPool; //Creamos el object pool de las balas.
    public int bulletsToInstantiate; //Variable para definir cuantas balas queremos crear.
    public float cadence; //Creamos la variable numerica que definira la cadencia del disparo.
    private float timer=0; //Un timer para aplicar la cadencia.
    public static List <GameObject> activedBullet;


    private void Awake()
    {
        bulletPool = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();
        bulletPool.Init(bulletsToInstantiate); //Inicializamos como tal el object pooling.
        activedBullet = new List<GameObject>(bulletsToInstantiate);
    }

    private void Update()
    {
        timer += Time.deltaTime; //Suma del tiempo al timer de cadencia.

        /* Si el timer llega a 1 disparamos y lo reiniciamos para que empiece de 0.
         * Por otro lado, si el timer de destruccion de bala llega a 3, se llama al RPC 
         * para que desactive el mencionado objeto.
         */
        if (timer > cadence)  
        {
            timer = 0f;
            Debug.Log("Timer resetado");
            photonView.RPC(nameof(RPC_CanyonShoot), RpcTarget.All);
        }
        
    }

    //Funcion RPC que manda el mensaje a los demas de disparar
    [PunRPC]
    void RPC_CanyonShoot()
    {
        Debug.Log("Voy a disparar");
        activedBullet.Add(bulletPool.SpawnBullet());
        Debug.Log(activedBullet);
    }

}
