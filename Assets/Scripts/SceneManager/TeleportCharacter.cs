using UnityEngine;

public class TeleportToHouse : MonoBehaviour
{
    [Tooltip("Объект, по которому нужно кликнуть, чтобы телепортироваться")]
    public GameObject teleportTriggerObject; // Сюда перетаскиваем объект в инспекторе

    [Tooltip("Координата X для телепортации персонажа")]
    public float targetX = -140.41f;

    [Tooltip("Координата Y для телепортации персонажа")]
    public float targetY = -25.76f;

    void Start()
    {
        // Проверяем, назначен ли объект-триггер
        if (teleportTriggerObject == null)
        {
            Debug.LogWarning("TeleportToHouse: Объект-триггер телепортации не назначен. Клик по нему не будет работать.");
        }
    }

    void Update()
    {
        // Проверяем, кликнул ли игрок по триггеру
        // Обычно OnMouseDown срабатывает на объекте, к которому прикреплён скрипт,
        // но мы хотим, чтобы он срабатывал на другом объекте (teleportTriggerObject).
        // Для этого используем Raycast.
        if (Input.GetMouseButtonDown(0)) // Левая кнопка мыши
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                // Проверяем, кликнули ли мы по нужному объекту
                if (hit.collider.gameObject == teleportTriggerObject)
                {
                    // Вызываем телепортацию
                    TeleportCharacter();
                }
            }
        }
    }

    void TeleportCharacter()
    {
        // Устанавливаем позицию этого GameObject (персонажа), к которому прикреплён скрипт
        transform.position = new Vector3(targetX, targetY, transform.position.z);
        Debug.Log($"Персонаж телепортирован на ({targetX}, {targetY})");
    }
}