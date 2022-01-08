using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _speedMax = 5f;
    [SerializeField] private float _speedMin = 3f;
    private Vector3 ForwardVector;
    private Rect _CanvasRect;
    private float _speed;

    private void Start()
    {
        _speed = Random.Range(_speedMin, _speedMax);
        _CanvasRect = transform.parent.GetComponent<RectTransform>().rect;
        
        ForwardVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
    }


    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + ForwardVector,
            Time.deltaTime * _speed);
        transform.localPosition = AutoTeleport.Teleport(transform.localPosition, _CanvasRect);
    }
}