using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Уже есть, если вы добавляли ранее

public class CharHp : MonoBehaviour
{
    Image RedHpImg;
    Image GreenHpImg;
    public float CurHp = 100;
    float CurDamage = 0;
    Animator animator;

    // --- НОВОЕ: Событие при получении урона ---
    public System.Action onTakeDamage;
    // --- КОНЕЦ НОВОГО ---

    void Start()
    {
        RedHpImg = transform.GetChild(1).GetComponent<Image>();
        GreenHpImg = transform.GetChild(2).GetComponent<Image>();
        animator = GetComponent<Animator>();
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
            CurHp = 0;
            OnPlayerDeath();
        }
    }

    public void TakeDamage(float damage)
    {
        CurHp -= damage;
        CurDamage += damage;
        UpdateUiHp();

        // --- НОВОЕ: Вызываем событие ---
        onTakeDamage?.Invoke();
        // --- КОНЕЦ НОВОГО ---
    }

    void OnPlayerDeath()
    {
        if (animator != null)
        {
            animator.SetTrigger("Dead");
        }
        else
        {
            Debug.LogWarning("Animator component not found on this object!");
        }
    }

    public void GoToMenu()
    {
        Debug.Log("✅ Метод GoToMenu вызван. Начинаю загрузку сцены 'Menu'...");

        try
        {
            Debug.Log("🔄 Пытаюсь загрузить сцENU 'Menu'...");
            SceneManager.LoadScene("Menu");
            Debug.Log("🎉 Сцена 'Menu' успешно загружена!");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"❌ КРИТИЧЕСКАЯ ОШИБКА: {e.Message}");
            Debug.LogError($"📌 StackTrace: {e.StackTrace}");
        }
    }
}