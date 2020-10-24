using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ReycastFollow : MonoBehaviour
{
    #region ReycastFollow

    [SerializeField] private LayerMask _mask;
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed;
    [SerializeField] private float _waitTime;
    [SerializeField] Transform _startPosition;

    private void FixedUpdate()
    {
        RaycastHit hit;
        var startPosition = transform.position;
        var direction = _player.position - startPosition;
        var rayCast = Physics.Raycast(startPosition, direction, out hit, 2, _mask);

        if (rayCast)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Vector3 relativePos = _player.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos);
                transform.rotation = rotation;

                transform.position += transform.forward * _speed * Time.deltaTime;
            }
            else StartCoroutine(Return());
        }
    }

    private IEnumerator Return()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = _startPosition.position;

        yield return new WaitForSeconds(_waitTime);
    }

    #endregion
}
