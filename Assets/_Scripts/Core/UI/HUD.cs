using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreValue;
    [SerializeField] private ScriptableInt _score;

    private void Start()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        _scoreValue.text = _score.Value.ToString();
    }
}