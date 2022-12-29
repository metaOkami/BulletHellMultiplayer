using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BulletBehaviour : MonoBehaviourPun
{
    Rigidbody bulletRb;
    public float bulletSpeed;
    public float timeOfLife;
    private float timer;
    private void Start()
    {
        timer = 0;
        bulletRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        bulletRb.AddForce(this.transform.forward * bulletSpeed * Time.fixedDeltaTime, ForceMode.Force);

        if (timer >= timeOfLife)
        {
            photonView.RPC(nameof(RPC_DestroyBullet), RpcTarget.All);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        photonView.RPC(nameof(RPC_DestroyBullet), RpcTarget.All);

    }

    [PunRPC]
    void RPC_DestroyBullet()
    {
        CanyonBehaviour.activedBullet.Remove(this.gameObject);
        CanyonBehaviour.bulletPool.AddInstanceToQueue(this.gameObject);
    }


}
