using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float _spawnRadius = 10f;
    [SerializeField] private float _initialSpawnTime = 1f;
    [SerializeField] private float _minSpawnTime = 0.25f;
    [SerializeField] private GameObject _baseEnemy;
    [SerializeField] private GameObject _specialEnemy;
    [SerializeField] private CharacterController _characterController;
    private List<GameObject> _spawnedList;
    private List<GameObject> _specialSpawnedList;
    private float _spawnTime;

    private void Start()
    {
        _spawnedList = new List<GameObject>();
        _specialSpawnedList = new List<GameObject>();
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        _spawnTime = _initialSpawnTime;
        GameObject spawnPrefab = _baseEnemy;
        
        while (_characterController)
        {
            GameObject spawn = Instantiate(spawnPrefab, GetSpawnPoint(), Quaternion.identity);
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    private Vector3 GetSpawnPoint()
    {
        if (!_characterController) return Vector3.zero;
        
        float angle = Random.Range(0f, 360f);
        float x = _characterController.transform.position.x + _spawnRadius * Mathf.Cos(angle);
        float y = _characterController.transform.position.y + _spawnRadius * Mathf.Sin(angle);

        return new Vector3(x, y, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(.75f, .1f, .1f, .25f);
        Gizmos.DrawSphere(_characterController.transform.position, _spawnRadius);
    }
}