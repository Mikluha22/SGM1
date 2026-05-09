using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private List<CharacterSpawnZone> _zones;

    [Header("Spawn Settings")]
    [SerializeField, Min(0.1f)] private float _spawnInterval = 3f;
    [SerializeField, Min(1)] private int _maxEnemies = 5;
    [SerializeField] private bool _excludePlayerZone = true;

    private CharacterSpawnZone _playerZone;      // запоминаем зону игрока
    private bool _playerSpawned;
    private int _currentEnemyCount;

    private void Start()
    {
        if (_zones == null || _zones.Count == 0)
            _zones = new List<CharacterSpawnZone>(FindObjectsByType<CharacterSpawnZone>());

        if (_zones.Count == 0)
        {
            Debug.LogError("Нет зон спавна!");
            return;
        }

        SpawnPlayer();
        StartCoroutine(SpawnEnemiesRoutine());
    }

    private void SpawnPlayer()
    {
        if (_playerSpawned || _zones.Count == 0)
            return;

        _playerZone = _zones[Random.Range(0, _zones.Count)];
        Instantiate(_playerPrefab, GetRandomPointInZone(_playerZone), Quaternion.identity);
        _playerSpawned = true;

        Debug.Log($"Игрок заспавнен в зоне: {_playerZone.name}");
    }

    private IEnumerator SpawnEnemiesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);

            _currentEnemyCount = FindObjectsByType<EnemyController>().Length;
            if (_currentEnemyCount >= _maxEnemies)
                continue;

            CharacterSpawnZone zone = GetRandomZoneForEnemy();
            if (zone != null)
            {
                Instantiate(_enemyPrefab, GetRandomPointInZone(zone), Quaternion.identity);
                Debug.Log($"Враг заспавнен в зоне: {zone.name}");
            }
        }
    }

    private CharacterSpawnZone GetRandomZoneForEnemy()
    {
        List<CharacterSpawnZone> availableZones = new List<CharacterSpawnZone>(_zones);

        if (_excludePlayerZone && _playerZone != null)
        {
            availableZones.Remove(_playerZone);   // исключаем зону игрока
        }

        if (availableZones.Count == 0)
        {
            Debug.LogWarning("Нет доступных зон для спавна врага.");
            return null;
        }

        return availableZones[Random.Range(0, availableZones.Count)];
    }

    private Vector3 GetRandomPointInZone(CharacterSpawnZone zone)
    {
        Vector2 randomCircle = Random.insideUnitCircle * zone.Radius;
        return zone.Position + new Vector3(randomCircle.x, 0f, randomCircle.y);
    }
}