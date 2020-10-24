using UnityEngine;


public class SpawnerEnemies : MonoBehaviour
{
    #region SpawnerEnemies

    [SerializeField] private GameObject _enemy;
    [SerializeField] private Transform _spawnPoint;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(_enemy, _spawnPoint.position, _spawnPoint.rotation);
            Destroy(gameObject);
        }
    }

    #endregion
}
