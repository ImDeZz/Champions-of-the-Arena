using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    VirtualJoystick joystick;
    GameObject playerMoji;
    Vector3 currentRot;

    void Start()
    {
        joystick = GameObject.Find("VirtualJoystick").GetComponent<VirtualJoystick>();
        playerMoji = GameObject.Find("emoji_kezzel");
	}
    
    void Update()
    {
        playerMoji.transform.rotation = Quaternion.LookRotation(getRotation()) * Quaternion.Euler(0, 90f, 0); ;
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
