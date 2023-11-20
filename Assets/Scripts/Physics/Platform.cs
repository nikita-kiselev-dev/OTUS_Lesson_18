using System.Collections.Generic;
using UnityEngine;

namespace Physics
{
    public class Platform : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private float _speed;

        [SerializeField]
        private List<Transform> m_Points;
        
        private Transform _currentTarget;

        private int _targetIndex;


        private void Start()
        {
            StartMoving();
        }

        private void StartMoving()
        {
            _targetIndex = 0;
            _currentTarget = m_Points[_targetIndex];
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            var distance = _currentTarget.position - transform.position;

            TryChangeTarget(distance);

            var direction = distance.normalized;

            _rigidbody.velocity = _speed * direction;
        }

        private void TryChangeTarget(Vector3 distance)
        {
            var magnitude = distance.magnitude;
            if (magnitude <= 0.1f)
            {
                if (_currentTarget == m_Points[^1])
                {
                    StartMoving();
                }
                else
                {
                    _targetIndex++;
                    _currentTarget = m_Points[_targetIndex];
                }
            }
        }
    }
}