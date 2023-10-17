using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private ScriptableBullet _bulletData;
    [SerializeField] private float _selfDestructionTimer = 1f;
    [SerializeField] private ScriptableGameEvent _shootEvent;
    [SerializeField] private GameObject _impact;

    private void OnEnable()
    {
        _shootEvent.Call();
        _rb.velocity = transform.GetMouseDirection().normalized * _bulletData.Speed;
        Invoke(nameof(SelfDestroy), _selfDestructionTimer);
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag(Constants.Tag.Enemy))
        {
            collider.GetComponent<AbstractEnemy>().Hit(_bulletData.Damage);
            Instantiate(_impact, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
