using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPun, IPunObservable
{
    [Header("Player Settings")]
    public float movementSpeed;
    public float rotationSpeed;
    public GameObject localPlayerObject, netPlayerObject;

    private float xRotation;
    private CharacterController controller;
    private Vector3 netPosition;
    private Quaternion netRotation;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (photonView.IsMine)
        {
            Cursor.lockState = CursorLockMode.Locked;
            gameObject.layer = LayerMask.NameToLayer("LocalPlayer");
        }
        else
        {
            localPlayerObject.SetActive(false);
            netPlayerObject.SetActive(true);
        }
        gameObject.name = photonView.Owner.NickName;
    }

    private void Update()
    {
        Debug.Log(netPosition);
        

        if (photonView.IsMine)
        {
            MovementAndRotation();
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, netPosition, Time.deltaTime * 5f);
            transform.rotation = Quaternion.Lerp(transform.rotation,netRotation,10f*Time.deltaTime);

        }
    }


    void MovementAndRotation()
    {
        Vector3 _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        _input = transform.TransformDirection(_input);
        controller.Move(_input.normalized * movementSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0f, Input.GetAxisRaw("Mouse X") * rotationSpeed * Time.deltaTime, 0f));

        xRotation -= Input.GetAxisRaw("Mouse Y") * rotationSpeed * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        localPlayerObject.transform.localEulerAngles = new Vector3(xRotation, 0f, 0f);

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(xRotation);

        }
        else
        {
            netPosition = (Vector3)stream.ReceiveNext();
            netRotation = (Quaternion)stream.ReceiveNext();
            xRotation = (float)stream.ReceiveNext();
            
        }
    }
}
