using UnityEngine;


public class Hammer : MonoBehaviour
{
    #region Hammer

    private Transform _player;
    public float Damage;
    private float _maxDistance = 1f;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        Vector3 relativePos = _player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;

        Vector3 _hammer = gameObject.transform.position;
        Vector3 _target = _player.transform.position;

        if ((_hammer - _target).sqrMagnitude < _maxDistance * _maxDistance) 
        {
            PlayerController hammer = _player.GetComponent<PlayerController>();
            hammer.Hurt(Damage);
        }
    }

    #endregion
}
