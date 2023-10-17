using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "EndlessBattle/Bullet", order = 1)]
public class ScriptableBullet : ScriptableObject
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;

    public int Damage { get => _damage;}
    public float Speed { get => _speed;}
}
