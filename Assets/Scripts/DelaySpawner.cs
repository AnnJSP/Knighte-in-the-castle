using UnityEngine;
using UnityEngine.Events;


public class DelaySpawner : MonoBehaviour
{
    #region DelaySpawner

    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private Transform[] _spawners;
    [SerializeField] private float _spawnDelay = 4.0f;
    [SerializeField] private float _currentTime = 4.0f;

    [SerializeField] private UnityEvent _onSpawned;

    private GameObject _currentEnemy;

    private void Start()
    {
        if (_onSpawned == null) _onSpawned = new UnityEvent();
        _currentTime = _spawnDelay;
    }

    private void Update()
    {
        if (_currentEnemy == null)
        {
            _currentTime -= Time.deltaTime;

            if (_currentTime <= 0)
            {
                _currentTime = _spawnDelay;
                Spawn();
            }
        }
    }

    private void Spawn()
    {
        var enemyNum = Random.Range(0, _enemies.Length);
        var spawnerNum = Random.Range(0, _spawners.Length);

        var enemy = _enemies[enemyNum];
        var spawner = _spawners[spawnerNum];

        _currentEnemy = Instantiate(enemy, spawner.position, spawner.rotation);

        _onSpawned.Invoke();
    }

    #endregion
}
