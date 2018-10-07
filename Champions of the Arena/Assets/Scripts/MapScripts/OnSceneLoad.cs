using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnSceneLoad : MonoBehaviour
{
    List<GameObject> weapons = new List<GameObject>();
    public int weaponCount = 4;
    public int mapDiv = 6;
    System.Random r = new System.Random();
    bool[,] mapObjects;
    List<KeyValuePair<int, int>> weaponPlaces = new List<KeyValuePair<int, int>>();

    readonly List<string> weaponList = new List<string>()
    {
        "emoji_kezzel",
    };

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        mapObjects = new bool[mapDiv, mapDiv+1];

        for (int i = 0; i < weaponCount; i++)
        {
            string weaponType = "";
            if (weaponList.Count > 0)
            {
                weaponType = weaponList[r.Next(0, weaponList.Count - 1)];
                GameObject weapon = ((GameObject)Instantiate(Resources.Load("Weapons\\" + weaponType)));
                weapons.Add(weapon);       
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
        weapons[currentWeapon].tag = "Weapons";
        weapons[currentWeapon].name = currentWeapon.ToString();
        weapons[currentWeapon].AddComponent<CapsuleCollider>();
        weapons[currentWeapon].AddComponent<Rigidbody>();
        weapons[currentWeapon].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        weapons[currentWeapon].transform.position = new Vector3(((place.Key * 10) - 25), 5.36f, ((place.Value * 9) - 30));
    }

    private bool positionsAreNotOkay(int x, int y)
    {
        return (mapObjects[x, y] == true || mapObjects[x-1, y] == true
             || mapObjects[x+1, y] == true || mapObjects[x, y-1] == true
             || mapObjects[x, y+1] == true);
    }
}
