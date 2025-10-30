using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillItem : MonoBehaviour
{
    CharHp ch;
    public float hill = 25;
    void OnEnable()
    {
        ch = FindObjectOfType<CharHp>();
    }
    private void OnMouseDown()
    {
        if (ch.CurHp <= (100 - hill))
        {
            ch.HillHp(hill);
            Destroy(gameObject);
        }
    }
}
