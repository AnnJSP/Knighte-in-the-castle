using UnityEngine;


public class FollowPlayer : MonoBehaviour
{
    #region Follow Player

    [SerializeField] private float _speed;
    private float _maxDistance = 10;
    private Transform _player;
    private Animator _animator;

    private void Start()
    {
        Transform _golem = gameObject.GetComponent<Transform>();
        _player = GameObject.FindWithTag("Player").transform;
        _animator = GetComponent<Animator>();
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
            transform.position += transform.forward * _speed * Time.deltaTime;
            _animator.SetTrigger("Wallk");
        }
    }

    #endregion
}
