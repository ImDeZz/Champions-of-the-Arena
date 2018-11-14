using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Attack : MonoBehaviour, IPointerClickHandler
{
    private bool attacked = false;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        attacked = true;
       /* GameObject.Find("spellImageBasic").SetActive(true);
        GameObject.Find("spellImageFrost").SetActive(true);
        GameObject.Find("spellImageFire").SetActive(true);*/
    }

    public bool isAttacking()
    {
        return attacked;
    }

    public void attackHappened()
    {
        attacked = false;
    }

    public void attackCantHappen()
    {
        attacked = false;
       /* GameObject.Find("spellImageBasic").SetActive(false);
        GameObject.Find("spellImageFrost").SetActive(false);
        GameObject.Find("spellImageFire").SetActive(false);*/
    }
}
