using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    const float speed = 20.0f;

    VirtualJoystick joystick; 
    CharacterController cC;

    // Use this for initialization
    void Start()
    {
        joystick = GameObject.Find("VirtualJoystick").GetComponent<VirtualJoystick>();
        cC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update ()
    {
        cC.Move(joystick.InputDirection*speed*Time.deltaTime);
	}

}
