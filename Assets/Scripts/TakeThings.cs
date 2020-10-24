using UnityEngine;


public class TakeThings : MonoBehaviour
{
    #region TakeThings

    [SerializeField] private GameObject _lock;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(_lock);
            Destroy(gameObject);
        }
    }

    #endregion
}
