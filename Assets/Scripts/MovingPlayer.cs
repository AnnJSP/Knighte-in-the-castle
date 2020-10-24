using UnityEngine;


public class MovingPlayer : MonoBehaviour
{
    #region MovingPlayer

    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    private Vector3 _direction;

    void Update()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");
        _direction.Normalize();

        float speed = _direction.sqrMagnitude > 0 ? _speed : 0f;
        speed *= Time.deltaTime;
        transform.position += transform.forward * speed;

        Vector3 desireForward = Vector3.RotateTowards(transform.forward, _direction, _rotationSpeed * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(desireForward);
    }

    #endregion
}
