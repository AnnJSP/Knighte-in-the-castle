using UnityEngine;
using UnityEngine.AI;


public class WayPointGolem : MonoBehaviour
{
    #region WayPointGolem

    public Transform[] Points;
    private int _destinationPoint = 0;
    private NavMeshAgent _agent;
    private Animator _animator;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.autoBraking = false;

        _animator = GetComponent<Animator>();

        GoToNextPoint();
    }

    void Update()
    {
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            GoToNextPoint();
        _animator.SetTrigger("Wallk");
    }

    void GoToNextPoint()
    {
        if (Points.Length == 0)
            return;

        _agent.destination = Points[_destinationPoint].position;
        _destinationPoint = (_destinationPoint + 1) % Points.Length;
    }

    #endregion
}
