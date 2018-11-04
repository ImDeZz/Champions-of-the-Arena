using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawn : Photon.MonoBehaviour
{
    private List<string> weapons = new List<string>();
    [SerializeField] private byte weaponCount = 4;
    [SerializeField] private byte mapDiv = 6;
    private System.Random r = new System.Random();
    private static bool[,] mapObjects;
    private List<KeyValuePair<int, int>> weaponPlaces = new List<KeyValuePair<int, int>>();
    public int targetFrameRate = 30;

    readonly List<string> weaponList = new List<string>()
    {
        "WeaponFi",
        "WeaponFr",
        "WeaponFr",
        "WeaponB",
        "WeaponB",
        "WeaponB",
    };
    

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }

    void Update()
    {
        if (weapons.Count < weaponCount)
        {
            if (PhotonNetwork.isMasterClient)
            {
                GetComponent<PhotonView>().RPC("spawnObjects", PhotonTargets.All);
            }
        }
    }

    [PunRPC]
    public void spawnObjects()
    {
        mapObjects = new bool[mapDiv, mapDiv+1];

        for (int i = 0; i < weaponCount; i++)
        {
            string weaponType = "";
            if (weaponList.Count > 0)
            {
                weaponType = weaponList[r.Next(0, weaponList.Count)];

                weapons.Add(weaponType);       
            }
        }

        for (int i = 0; i < weaponCount; i++)
        {
            int ErrorCount = 0;
            int x = 1;
            int y = 1;
            do
            {
                x = r.Next(1, mapDiv - 1);
                y = r.Next(1, mapDiv);
                ErrorCount++;
            } while (positionsAreNotOkay(x, y) && ErrorCount < mapDiv);

            mapObjects[x, y] = true;
            weaponPlaces.Add(new KeyValuePair<int, int>(x,y));
        }

        int currentWeapon = 0;
        foreach (KeyValuePair<int, int> place in weaponPlaces)
        {
            configureWeaponModel(currentWeapon, place);
            currentWeapon++;
        }
    }

    private void configureWeaponModel(int currentWeapon, KeyValuePair<int, int> place)
    {
        GameObject Instantiate = PhotonNetwork.Instantiate(weapons[currentWeapon], new Vector3(((place.Key * 10) - 25), 5.36f, ((place.Value * 9) - 30)), new Quaternion(0,0,0,0), 0) as GameObject;
        Instantiate.name = weapons[currentWeapon] + "_" +currentWeapon;
    }

    private bool positionsAreNotOkay(int x, int y)
    {
        return (mapObjects[x, y] == true || mapObjects[x - 1, y] == true
                || mapObjects[x + 1, y] == true || mapObjects[x, y - 1] == true
                || mapObjects[x, y + 1] == true);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
