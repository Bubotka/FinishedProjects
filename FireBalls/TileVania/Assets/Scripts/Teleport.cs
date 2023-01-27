using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Teleport : MonoBehaviour
{
    [SerializeField] private GameObject _finishPoint;
    [SerializeField] private float _timeToTeleport=1f;

    private Animator _animator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player _player))
        {
            _animator = _player.GetComponent<Animator>();
            StartCoroutine(PlayerTeleport(_player));
        }
    }

    IEnumerator PlayerTeleport(Player player)
    {
        _animator.SetTrigger("Teleport");
        yield return new WaitForSeconds(_timeToTeleport);
        player.transform.position = _finishPoint.transform.position;
        Destroy(gameObject);
    }
}
