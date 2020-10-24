using UnityEngine;


public class MovingCamera : MonoBehaviour
{
    #region MovingCamera

    [SerializeField] private Transform _playerTransfotm;
    [SerializeField] private float _lerpSpeed = 0.3f;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, GerCurrentPlayerPositon(), _lerpSpeed);
    }

    private Vector3 GerCurrentPlayerPositon()
    {
        return _playerTransfotm.position + Vector3.back * 10 + Vector3.up * 6;
    }

    #endregion
}
