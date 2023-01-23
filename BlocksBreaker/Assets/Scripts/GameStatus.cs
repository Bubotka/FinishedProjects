using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{
    [Range(0.1f,2)][SerializeField] private float _gamesSpeed;
    [SerializeField] private int _pointsPerDestroyedBlock = 22;
    [SerializeField] private int _currentScore=0;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        int gameStatus=FindObjectsOfType<GameStatus>().Length;

        if (gameStatus > 1) 
        { 
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
            DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        _scoreText.text = _currentScore.ToString();
        Time.timeScale = _gamesSpeed;
    }

    public void AddToScore()
    {
        _scoreText.text = _currentScore.ToString();
        _currentScore += _pointsPerDestroyedBlock;
    }

    public void ResetScore()
    {
        Destroy(gameObject);
    }
}
