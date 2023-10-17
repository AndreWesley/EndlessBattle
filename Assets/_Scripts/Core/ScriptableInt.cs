using UnityEngine;

[CreateAssetMenu(fileName = "int", menuName = "Primitives/int", order = 0)]
public class ScriptableInt : ScriptableObject
{
    [SerializeField] private int _value;

    public int Value { get => _value; set => _value = value; }
}
