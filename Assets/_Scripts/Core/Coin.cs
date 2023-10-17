using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private ScriptableInt _score;
    [SerializeField] private ScriptableGameEvent _scoreEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _score.Value++;
            _scoreEvent.Call();
            Destroy(gameObject);
        }
    }
}
