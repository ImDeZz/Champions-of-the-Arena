using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    const float speed = 20.0f;

    VirtualJoystick joystick; 
    CharacterController cC;
    float prevy;

    // Use this for initialization
    void Start()
    {
        joystick = GameObject.Find("VirtualJoystick").GetComponent<VirtualJoystick>();
        cC = GetComponent<CharacterController>();
        prevy = cC.gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update ()
    {
        cC.Move(joystick.InputDirection*speed*Time.deltaTime);
        if (cC.transform.position.y != prevy)
        {
            cC.gameObject.transform.position = new Vector3(cC.gameObject.transform.position.x, prevy, cC.gameObject.transform.position.z);
        }         
	}

}
