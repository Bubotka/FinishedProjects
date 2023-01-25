using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHealth : MonoBehaviour
{ 
    private Text _healthScore;
    private Player _player;

    private void Start()
    {
        _healthScore = GetComponent<Text>();
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        _healthScore.text = _player.GetHealth().ToString();
    }
}
