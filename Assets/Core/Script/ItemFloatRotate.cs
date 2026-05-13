using UnityEngine;

public class ItemFloatRotate : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 90f;   // градусов в секунду

    [Header("Floating")]
    [SerializeField] private float bobSpeed = 2f;         // частота покачивания
    [SerializeField] private float bobAmount = 0.1f;      // амплитуда по высоте

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Вращение вокруг оси Y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

        // Вертикальное движение (как в Minecraft)
        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobAmount;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
