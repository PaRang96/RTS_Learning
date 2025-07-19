using System;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    private Camera _cam;
    private NavMeshAgent _agent;
    
    [SerializeField]
    private float speed;

    public LayerMask ground;

    private void Start()
    {
        _cam = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
        if (_agent != null)
        {
            _agent.speed = speed;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, ground))
            {
                _agent.SetDestination(hit.point);
            }
        }
    }
}
