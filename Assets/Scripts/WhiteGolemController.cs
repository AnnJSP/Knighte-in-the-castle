using System.Collections;
using UnityEngine;


public class WhiteGolemController : MonoBehaviour
{
    #region White Golem Controller

    [SerializeField] private float _powerOfFirstAttack;
    [SerializeField] private float _powerOfSecondAttack;
    [SerializeField] private float _golemSpeed;
    [SerializeField] private LayerMask _mask;
    private Animator _animator;
    private Transform _player;
    private float _maxDistance = 10;

    public float HelthPoint;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        Vector3 relativePos = _player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;

        Vector3 follower = gameObject.transform.position;
        Vector3 _target = _player.transform.position;

        if ((follower - _target).sqrMagnitude < _maxDistance * _maxDistance)
        {
            transform.position += transform.forward * _golemSpeed * Time.deltaTime;
            _animator.SetTrigger("Wallk");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        float count = 0;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (count < 3)
            {
                PlayAnimation("Attack01");
                count++;

                PlayerController attack01 = collision.gameObject.GetComponent<PlayerController>();
                attack01.Hurt(_powerOfFirstAttack);
            }

            if (count == 3)
            {
                PlayAnimation("Attack02");
                count = 0;

                PlayerController attack02 = collision.gameObject.GetComponent<PlayerController>();
                attack02.Hurt(_powerOfSecondAttack);
            }
        }

        if (collision.gameObject.CompareTag("Thing"))
        {
            Hurt(5);
        }
    }

    #endregion


    #region Animation

    private void PlayAnimation(string trigerName)
    {
        _animator.SetTrigger(trigerName);
        _animator.SetTrigger("Idle");
    }

    #endregion


    #region CharacteristicsGolem

    private float HeaithPoint;

    public void Hurt(float Damage)
    {
        HeaithPoint -= Damage;

        PlayAnimation("Hit");

        if (HeaithPoint <= 0)
        {
            _animator.SetTrigger("Die");
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    #endregion
}
