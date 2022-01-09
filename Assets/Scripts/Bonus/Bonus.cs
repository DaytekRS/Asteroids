using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private string _trigerTag = "Player";
    [SerializeField] private float _timeLife = 5f;

    private void Start()
    {
        Invoke("OnTimeLeft", _timeLife);
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals(_trigerTag))
        {
            Destroy(gameObject);
        }
    }

    private void OnTimeLeft()
    {
        Destroy(gameObject);
    }
}
