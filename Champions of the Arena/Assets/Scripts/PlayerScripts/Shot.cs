using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

    [SerializeField] private GameObject projectile;
    [SerializeField] private int projectileSpeed = 200;
    [SerializeField] private float shootCollisionDelay = 0.3f;
    private Rigidbody rigid;
    bool collisionReady = false;
    float timeStamp = 0;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        timeStamp = Time.time;
    }

    void Update()
    {
        rigid.AddRelativeForce((projectile.transform.forward * -1) * projectileSpeed * Time.deltaTime, ForceMode.Impulse);
        if ((Time.time - timeStamp) > shootCollisionDelay)
        {
            collisionReady = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (PhotonNetwork.isMasterClient)
        {
            if (collisionReady)
            {
                PhotonNetwork.Destroy(projectile);
            }
        }
    }

}
