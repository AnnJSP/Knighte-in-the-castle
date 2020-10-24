using UnityEngine;


public class Poison : MonoBehaviour
{
    #region Poison

    [SerializeField] private float Damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController poison = collision.gameObject.GetComponent<PlayerController>();
            poison.Hurt(Damage);
        }
    }

    #endregion
}