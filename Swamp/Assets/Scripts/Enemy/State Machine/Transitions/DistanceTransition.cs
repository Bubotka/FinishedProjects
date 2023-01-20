using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private float _transtionRange;
    [SerializeField] private float _rangeSpread;

    private void Start()
    {
        _transtionRange+= Random.Range(-_rangeSpread, _rangeSpread);
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, Target.transform.position) < _transtionRange)
        {
            NeedTransit=true;
        }
    }
}
