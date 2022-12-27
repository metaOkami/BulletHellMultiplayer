using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ObjectPool : MonoBehaviourPun
{
    public GameObject prefab;
    private readonly HashSet<GameObject> _instantiatedObjects;
    private Queue<GameObject> _pooledObjects;
    private Vector3 _spawnPos;

    public ObjectPool(GameObject _prefab)
    {
        prefab = _prefab;
        _instantiatedObjects = new HashSet<GameObject>();
    }

    public void Init(int numberOfBullets)
    {
        _pooledObjects = new Queue<GameObject>(numberOfBullets);

        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject instance=new GameObject();
            instance.name = i.ToString();
            instance = PhotonNetwork.Instantiate(prefab.name, SpawnPosition(instance.name), SpawnRotation(instance.name));
            instance.SetActive(false);
            _pooledObjects.Enqueue(instance);
        }
    }

    public Vector3 SpawnPosition(string name)
    {
        switch (name)
        {
            case "0":
                return GameObject.Find("BulletSpawner1").transform.position;
            case "1":
                return GameObject.Find("BulletSpawner2").transform.position;
            case "2":
                return GameObject.Find("BulletSpawner3").transform.position;
            case "3":
                return GameObject.Find("BulletSpawner4").transform.position;
            case "4":
                return GameObject.Find("BulletSpawner5").transform.position;
            case "5":
                return GameObject.Find("BulletSpawner6").transform.position;
            case "6":
                return GameObject.Find("BulletSpawner7").transform.position;
            case "7":
                return GameObject.Find("BulletSpawner8").transform.position;
        }
        return Vector3.zero;
    }
    public Quaternion SpawnRotation(string name)
    {
        switch (name)
        {
            case "0":
                return GameObject.Find("BulletSpawner1").transform.rotation;
            case "1":
                return GameObject.Find("BulletSpawner2").transform.rotation;
            case "2":
                return GameObject.Find("BulletSpawner3").transform.rotation;
            case "3":
                return GameObject.Find("BulletSpawner4").transform.rotation;
            case "4":
                return GameObject.Find("BulletSpawner5").transform.rotation;
            case "5":
                return GameObject.Find("BulletSpawner6").transform.rotation;
            case "6":
                return GameObject.Find("BulletSpawner7").transform.rotation;
            case "7":
                return GameObject.Find("BulletSpawner8").transform.rotation;
        }
        return Quaternion.identity;
    }

    public GameObject SpawnBullet()
    {
        var recyclableObject = GetInstance();
        _instantiatedObjects.Add(recyclableObject);
        recyclableObject.gameObject.SetActive(true);
        return recyclableObject;
    }

    private GameObject GetInstance()
    {
        if (_pooledObjects.Count > 0)
        {
            return _pooledObjects.Dequeue();
        }
        Debug.Log("No quedan balas, aumenta las instancias");
        return null;
    }
    public void AddInstanceToQueue(GameObject instance)
    {
        instance.SetActive(false);
        instance.transform.position = SpawnPosition(instance.name);
        _pooledObjects.Enqueue(instance);
    }
}
