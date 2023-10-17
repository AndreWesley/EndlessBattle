using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _smoothTime;
    private Vector2 velocity;

    void LateUpdate()
    {
        if (!_target) return;
        
        Vector2 targetPosition = _target.position + _target.up;
        Vector3 mov = Vector2.SmoothDamp(transform.position, _target.position, ref velocity, _smoothTime);
        mov.z = transform.position.z;
        transform.position = mov;
    }
}
