using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private int _breakableBlocks; //Serialized for debugging purposes

    private SceneLoader _sceneLoader;
    public void CountBlocks()
    {
        _breakableBlocks++;
    }

    private void Start()
    {
        _sceneLoader=FindObjectOfType<SceneLoader>();
    }

    public void DestroyBlock()
    {
        _breakableBlocks--;

        if (_breakableBlocks <= 0)
        {
            _sceneLoader.LoadNextScene();
        }
    }
}
