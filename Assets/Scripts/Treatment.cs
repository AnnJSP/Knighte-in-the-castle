using UnityEngine;


public class Treatment : MonoBehaviour
{
    #region Treatment

    [SerializeField] public float HelthPoint;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController medicine = collision.gameObject.GetComponent<PlayerController>();
            medicine.Treatment(HelthPoint);

            if (collision.gameObject.CompareTag("Player"))
            {
                _animator.SetBool("EnterIN", !_animator.GetBool("EnterIN"));
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _animator.SetBool("EnterIN", !_animator.GetBool("EnterIN"));
        }
    }

    #endregion
}
