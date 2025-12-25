using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyHp : MonoBehaviour
{
    public event Action onDeadeZomby;
    Image RedHpImg;
    Image GreenHpImg;
    public float CurHp = 100;
    float CurDamage = 0;


    void Start()
    {
        RedHpImg = transform.GetChild(0).GetComponent<Image>();
        GreenHpImg = transform.GetChild(1).GetComponent<Image>();
    }

    public void HillHp(float hill)
    {
        CurHp += hill;
        CurDamage -= hill;
        UpdateUiHp();
    }

    private void UpdateUiHp()
    {
        CurHp = Mathf.Clamp(CurHp, 0, 100);
        CurDamage = Mathf.Clamp(CurDamage, 0, 100);
        GreenHpImg.fillAmount = CurHp / 100;
        RedHpImg.fillAmount = CurDamage / 100;
        if (CurHp <= 0)
        {
            onDeadeZomby?.Invoke();
        }
    }

    public void TakeDamage(float damage)
    {
        CurHp -= damage;
        CurDamage += damage;
        UpdateUiHp();
    }
}
