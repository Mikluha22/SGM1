using UnityEngine;
using Random = UnityEngine.Random;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private PickUpItem _pickUpPrefab;   // теперь любой PickUpItem
    [SerializeField] private float _range = 2f;
    [SerializeField] private int _maxCount = 2;
    [SerializeField] private float _spawnIntervalSeconds = 5f;
    [SerializeField] private float _minSpawnIntervalSeconds = 1f;
    [SerializeField] private float _maxSpawnIntervalSeconds = 5f;

    private float _currentSpawnTimerSeconds;
    private int _currentCount;
    private float _nextSpawnDelay;
    private bool _coolingDown;

    protected void Awake()
    {
        _nextSpawnDelay = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
    }

    protected void Update()
    {
        if (_currentCount < _maxCount && !_coolingDown)
        {
            _currentSpawnTimerSeconds += Time.deltaTime;

            if (_currentSpawnTimerSeconds >= _nextSpawnDelay)
            {
                _currentSpawnTimerSeconds = 0f;
                _currentCount++;

                Vector3 randomPoint = Random.insideUnitCircle * _range;
                Vector3 randomPosition = new Vector3(randomPoint.x, 0f, randomPoint.y) + transform.position;

                PickUpItem pickUp = Instantiate(_pickUpPrefab, randomPosition, Quaternion.identity, transform);
                pickUp.OnPickUp += OnItemPickUp;

                _nextSpawnDelay = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
            }
        }
    }

    private void OnItemPickUp(PickUpItem item)
    {
        _currentCount--;
        item.OnPickUp -= OnItemPickUp;

        _coolingDown = true;
        _currentSpawnTimerSeconds = 0f;
        Invoke(nameof(EndCooldown), _spawnIntervalSeconds);
    }

    private void EndCooldown()
    {
        _coolingDown = false;
        _nextSpawnDelay = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
    }

    protected void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, _range);
#endif
    }
}