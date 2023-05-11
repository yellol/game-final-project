using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class UIFollow : MonoBehaviour
{
    public GameObject _player;
    private Camera _main;
    private Vector2 _initialPos;
    private Transform _canvas;

    // Start is called before the first frame update
    void Awake()
    {
        _main = GetComponentInParent<Canvas>().worldCamera;
        _canvas = transform.parent;

    }

    // Update is called once per frame
    void Update()
    {
        var position = _player.transform.position;
        _initialPos = new Vector2(position.x, position.y);
        ((RectTransform)transform).anchoredPosition = _canvas.InverseTransformPoint(_initialPos);
        //_main.WorldToScreenPoint(new Vector2(_player.transform.position.x, _player.transform.position.y)) - new Vector3(_canvasRect.rect.width * 2, _canvasRect.rect.height * 4, 0) ;
    }
}
