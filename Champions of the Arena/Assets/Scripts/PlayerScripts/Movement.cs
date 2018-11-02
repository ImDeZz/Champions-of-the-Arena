using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// EHHEZ A FÁJLHOZ SENKI NE NYÚLJON
/// </summary>
public class Movement :  Photon.MonoBehaviour
{
    [SerializeField] float speed = 20.0f;
    [SerializeField] bool teleportEnabled;
    [SerializeField] byte teleportThreshold;

    private PhotonView pv;
    private Quaternion targetRot;
    private Vector3 targetPos;

    VirtualJoystick joystick; 
    CharacterController cC;
    GameObject playerMoji;
    Vector3 currentRot;
    float prevy;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        joystick = GameObject.Find("VirtualJoystick").GetComponent<VirtualJoystick>();
        playerMoji = transform.Find("emoji_kezzel").gameObject;
        cC = GetComponent<CharacterController>();
        prevy = cC.gameObject.transform.position.y;
    }


    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(playerMoji.transform.rotation);
        }
        else
        {      
            targetPos = (Vector3)stream.ReceiveNext();
            targetRot = (Quaternion)stream.ReceiveNext();
        }
    }

    void Update ()
    {
        if (photonView.isMine)
        {
            cC.Move(joystick.InputDirection * speed * Time.deltaTime);
            if (cC.transform.position.y != prevy)
            {
                cC.gameObject.transform.position = new Vector3(cC.gameObject.transform.position.x, prevy, cC.gameObject.transform.position.z);
            }
            playerMoji.transform.rotation = Quaternion.LookRotation(getRotation()) * Quaternion.Euler(0, 90f, 0);
        }
        else
        {   
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.25f);
            playerMoji.transform.rotation = targetRot;        

            if (teleportEnabled == true)
            {
                if (Vector3.Distance(this.transform.position, targetPos) > teleportThreshold)
                {
                    this.transform.position = targetPos;
                }
            }
        }
	}

    public Vector3 getRotation()
    {
        if (joystick.InputDirection == Vector3.zero)
        {
            return currentRot;
        }

        currentRot = joystick.InputDirection;
        return new Vector3(joystick.InputDirection.x, joystick.InputDirection.y, joystick.InputDirection.z);
    }

}
