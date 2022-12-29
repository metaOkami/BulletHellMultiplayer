using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab; //Objeto que se instanciara
    private List<GameObject> _instantiatedObjects; //Lista de objetos instanciados pero no usados
    private Queue<GameObject> _pooledObjects; //Lista de objetos en uso
    
    /*La funcion que nos encontramos abajo se encarga de iniciar el patron
     * instanciando los objetos que le pasamos como parametro. Dichos objetos
     * Se meten en una cola de GameObjects a la espera de ser llamados.
     */
    public void Init(int numberOfBullets) 
    {
        _pooledObjects = new Queue<GameObject>(numberOfBullets);
        _instantiatedObjects = new List<GameObject>(numberOfBullets);

        for (int i = 0; i < numberOfBullets; i++)
        {
            string name = "BulletSpawner"+(i+1).ToString();
            GameObject instance = PhotonNetwork.Instantiate(prefab.name, GameObject.Find(name).transform.position, GameObject.Find(name).transform.rotation);
            instance.name = i.ToString();
            instance.SetActive(false);
            _pooledObjects.Enqueue(instance);
        }
    }

    //Esta funcion se encarga de decidir la posicion de spawn de la bala dependiendo de su nombre
    public Transform SpawnPosition(string name) 
    {
        switch (name)
        {
            case "0":
                return GameObject.Find("BulletSpawner1").transform;
            case "1":
                return GameObject.Find("BulletSpawner2").transform;
            case "2":
                return GameObject.Find("BulletSpawner3").transform;
            case "3":
                return GameObject.Find("BulletSpawner4").transform;
            case "4":
                return GameObject.Find("BulletSpawner5").transform;
            case "5":
                return GameObject.Find("BulletSpawner6").transform;
            case "6":
                return GameObject.Find("BulletSpawner7").transform;
            case "7":
                return GameObject.Find("BulletSpawner8").transform;
        }
        return null;
    }

   

    //Esta funcion se encarga de decidir la rotacion de spawn de la bala dependiendo de su nombre
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
    
    //Esta funcion activa una bala, la añade a la lista de objetos en uso y te devuelve dicho objeto
    public GameObject SpawnBullet()
    {
        GameObject recyclableObject = GetInstance();
        _instantiatedObjects.Add(recyclableObject);
        recyclableObject.SetActive(true);
        return recyclableObject;
    }
    
    //GetInstance se limita a sacar la bala de la cola de "objetos en espera" en caso de que 
    //haya objetos disponibles, en caso contrario avisa de este problema.
    private GameObject GetInstance()
    {
        if (_pooledObjects.Count > 0)
        {
            return _pooledObjects.Dequeue();
        }
        Debug.Log("No quedan balas, aumenta las instancias");
        return null;
    }

    //Aqui tenemos la ultima funcionalidad del object pooling, una funcion para resetear
    //la bala a su posicion inicial y ponerla a la espera de ser activada otra vez.
    public void AddInstanceToQueue(GameObject instance)
    {
        instance.SetActive(false);
        _instantiatedObjects.Remove(instance);
        instance.transform.position = SpawnPosition(instance.name).localPosition;
        instance.transform.rotation = SpawnRotation(instance.name);
        _pooledObjects.Enqueue(instance);
    }
}
