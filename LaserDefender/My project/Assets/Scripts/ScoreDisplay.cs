using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    private Text _textScore;
    private GameSession _gameSession;

    private void Start()
    {
        _textScore = GetComponent<Text>();
        _gameSession = FindObjectOfType<GameSession>();
    }

    private void Update()
    {
        _textScore.text = _gameSession.GetScore().ToString();
    }
}
