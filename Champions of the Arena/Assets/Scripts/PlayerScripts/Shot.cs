using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : Photon.MonoBehaviour
{

    [SerializeField] private GameObject projectile;
    [SerializeField] private int projectileAccel = 200;
    [SerializeField] private float shootCollisionDelay = 0.3f;
    [SerializeField] private float autoRemoveTime = 2.0f;
    private int maxSpeed = 20;
    private Rigidbody rigid;
    bool collisionReady = false;
    float timeStamp = 0;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        timeStamp = Time.time;
        rigid.AddRelativeForce((projectile.transform.forward * -1) * projectileAccel * Time.deltaTime, ForceMode.VelocityChange);
    }

    void Update()
    {
        if ((Time.time - timeStamp) > shootCollisionDelay)
        {
            collisionReady = true;  
        }

        if ((Time.time - timeStamp) > autoRemoveTime)
        {
            removeProjectile();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionReady)
        {
            removeProjectile();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collisionReady)
        {
            removeProjectile();
        }
    }

    public void removeProjectile()
    {  
        Destroy(this.gameObject);
    }
}
