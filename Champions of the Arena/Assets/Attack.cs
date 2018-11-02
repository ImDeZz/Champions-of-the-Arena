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
    }
}
