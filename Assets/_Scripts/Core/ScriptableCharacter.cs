using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "EndlessBattle/Character", order = 0)]
public class ScriptableCharacter : ScriptableObject
{
    [SerializeField] private int _maxHP;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackRange;
    [SerializeField] private bool _hasSpecialMove;
    [SerializeField, Range(0f, 1f)] private float _specialMoveChance;

    public int MaxHP { get => _maxHP;}
    public float Speed { get => _speed;}
    public float AttackRange { get => _attackRange;}
    public bool HasSpecialMove { get => _hasSpecialMove;}
    public float SpecialMoveChance { get => _specialMoveChance; }
}
