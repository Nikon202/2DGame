using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharHp : MonoBehaviour
{
    Image RedHpImg;
    Image GreenHpImg;
    float CurHp = 100;
    float CurDamage = 0;
    void Start()
    {
        RedHpImg = transform.GetChild(1).GetComponent<Image>();
        GreenHpImg = transform.GetChild(2).GetComponent<Image>();
    }

    public void HillHp(float hill)
    {
        CurHp += hill;
        CurDamage -= hill;
        GreenHpImg.fillAmount = CurHp / 100;
        RedHpImg.fillAmount = CurDamage / 100;
    }
    public void TakeDamage(float damage)
    {
        CurHp -= damage;
        CurDamage += damage;
        GreenHpImg.fillAmount = CurHp / 100;
        RedHpImg.fillAmount = CurDamage / 100;
    }
}
