using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OffScreenIndicator : MonoBehaviour
{
    public Camera cam; // основная камера
    public GameObject arrowPrefab;
    public float minScale = 0.5f;
    public float maxScale = 1.5f;
    public float edgeOffset = 50f; // расстояние от краёв экрана в пикселях

    private List<GameObject> arrowPool = new List<GameObject>();
    private Transform[] enemies;

    void Start()
    {
        cam = Camera.main;
        UpdateEnemyList();
    }

    void Update()
    {
        UpdateEnemyList();
        foreach (var arrow in arrowPool) arrow.SetActive(false); // скрываем все

        for (int i = 0; i < enemies.Length; i++)
        {
            Vector3 enemyPos = enemies[i].position;
            Vector3 screenPos = cam.WorldToViewportPoint(enemyPos);

            // Проверка за экраном
            bool offScreen = screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1;
            if (!offScreen) continue;

            // Находим угол к врагу от центра экрана
            Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0);
            Vector3 dir = (screenPos - screenCenter).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            // Определяем позицию на границе экрана
            float x = Mathf.Clamp(screenPos.x, edgeOffset / Screen.width, 1 - edgeOffset / Screen.width);
            float y = Mathf.Clamp(screenPos.y, edgeOffset / Screen.height, 1 - edgeOffset / Screen.height);
            Vector3 uiPos = new Vector3(x * Screen.width, y * Screen.height, 0);

            // Получаем или создаём стрелку
            GameObject arrow = GetArrow(i);
            arrow.SetActive(true);
            arrow.transform.position = uiPos;
            arrow.transform.rotation = Quaternion.Euler(0, 0, angle);

            // Масштаб в зависимости от расстояния
            float distance = Vector3.Distance(enemyPos, cam.transform.position);
            float scale = Mathf.Clamp(maxScale - distance * 0.1f, minScale, maxScale);
            arrow.transform.localScale = new Vector3(scale, scale, 1);
        }
    }

    GameObject GetArrow(int index)
    {
        while (arrowPool.Count <= index)
        {
            GameObject arrow = Instantiate(arrowPrefab, transform);
            arrowPool.Add(arrow);
        }
        return arrowPool[index];
    }

    void UpdateEnemyList()
    {
        // Собираем живых врагов
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = new Transform[enemyObjects.Length];
        for (int i = 0; i < enemyObjects.Length; i++)
            enemies[i] = enemyObjects[i].transform;
    }
}