using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private AudioClip _blockDestroy;
    [SerializeField] private GameObject _blockSparkles;
    [SerializeField] private Sprite[] _hitSprites;

    private int _currentHits;
    private Level _level;

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        _level = FindObjectOfType<Level>();

        if (tag == "Breakable")
            _level.CountBlocks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
            HandleHit();
    }

    private void HandleHit()
    {
        _currentHits++;

        int maxHits = _hitSprites.Length + 1;

        if (_currentHits == maxHits)
            DestroyBlock();
        else
            ShowNextHitSprite();
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = _currentHits-1;

        if (_hitSprites[spriteIndex]!=null)
            GetComponent<SpriteRenderer>().sprite = _hitSprites[spriteIndex];
    }

    private void DestroyBlock()
    {
        FindObjectOfType<GameStatus>().AddToScore();
        AudioSource.PlayClipAtPoint(_blockDestroy, Camera.main.transform.position);
        Destroy(gameObject);
        _level.DestroyBlock();
        TriggerSparklesVFX();
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkls = Instantiate(_blockSparkles, transform.position, transform.rotation);
        Destroy(sparkls, 1f);
    }
}
