using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float _minX=1f;
    [SerializeField] private float _maxX=15f;
    [SerializeField] private float _screenWidthInUnits = 16f;

    private void Update()
    {
        float mousePosInUnits = Input.mousePosition.x / Screen.width * _screenWidthInUnits;

        transform.position = new Vector2(Mathf.Clamp(mousePosInUnits,_minX,_maxX),transform.position.y);
    }
}
