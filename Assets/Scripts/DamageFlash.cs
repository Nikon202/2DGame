using UnityEngine;
using System.Collections;

public class DamageFlash : MonoBehaviour
{
    [Tooltip("Спрайт, используемый для обводки")]
    public Sprite flashSprite; // Присвоим спрайт '01' через Inspector

    [Tooltip("Продолжительность эффекта обводки в секундах")]
    public float flashDuration = 0.2f;

    [Tooltip("Слой для обводки (например, UI или Default)")]
    public int sortingOrder = 10; // Обычно выше основного спрайта

    private SpriteRenderer flashRenderer;
    private Coroutine flashCoroutine;

    void Start()
    {
        // Создаём GameObject для обводки
        GameObject flashObject = new GameObject("DamageFlash");
        flashObject.transform.SetParent(transform, false); // Привязываем к персонажу, но сохраняем его позицию/поворот/масштаб
        flashObject.transform.localPosition = Vector3.zero; // Убедимся, что он в центре персонажа

        // Добавляем SpriteRenderer
        flashRenderer = flashObject.AddComponent<SpriteRenderer>();
        flashRenderer.sortingOrder = sortingOrder; // Устанавливаем слой отображения
        flashRenderer.enabled = false; // Сначала скрываем

        // Подписываемся на событие получения урона
        CharHp charHp = GetComponent<CharHp>();
        if (charHp != null)
        {
            charHp.onTakeDamage += OnTakeDamage;
        }
        else
        {
            Debug.LogError("DamageFlash: Не найден компонент CharHp на этом объекте!");
        }
    }

    void OnDestroy()
    {
        // Отписываемся от события при уничтожении объекта (хорошая практика)
        CharHp charHp = GetComponent<CharHp>();
        if (charHp != null)
        {
            charHp.onTakeDamage -= OnTakeDamage;
        }
    }

    void OnTakeDamage()
    {
        // Отменяем предыдущий эффект, если он ещё не закончился
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        // Запускаем эффект обводки
        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        // Включаем отображение спрайта обводки
        flashRenderer.sprite = flashSprite;
        flashRenderer.enabled = true;

        // Ждём заданное время
        yield return new WaitForSeconds(flashDuration);

        // Выключаем отображение спрайта обводки
        flashRenderer.enabled = false;

        // Сбрасываем корутину
        flashCoroutine = null;
    }
}