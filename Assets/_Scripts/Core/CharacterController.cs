using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _spriteTransform;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private ScriptableInt _score;
    [SerializeField] private ScriptableGameEvent _gameOverEvent;

    [Header("Setup")]
    [SerializeField] private float _speed;
    [SerializeField] private int _hp;

    private static Transform _transform;
    private Vector2 _movement;

    [ExecuteInEditMode]
    private void Awake()
    {
        _transform = transform;
    }
    public static Transform GetTransform => _transform;

    void Update()
    {
        UpdateMovement();
        transform.LookToMouse(Direction.Up);
        TryShoot();
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movement;
    }

    private void UpdateMovement()
    {
        float x = Input.GetAxis(Constants.Inputs.Horizontal) * _speed;
        float y = Input.GetAxis(Constants.Inputs.Vertical) * _speed;
        _movement = new Vector2(x, y);
    }

    private void TryShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(_bulletPrefab, transform.position, _spriteTransform.rotation);
        }
    }

    private void Hit()
    {
        _hp -= 5000;

        if (_hp <= 0)
        {
            Explode();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.Tag.Enemy))
        {
            Hit();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Limit"))
        {
            Hit();
        }
    }

    private void Explode()
    {
        Instantiate(_explosionPrefab, transform.position, quaternion.identity);
        _gameOverEvent.Call();
        Destroy(gameObject);
    }
}
