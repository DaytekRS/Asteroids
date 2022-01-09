using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private float ShieldTimeLife = 5f;

    void Start()
    {
        Invoke("OnTimeLeft", ShieldTimeLife);
    }

    
    private void OnTimeLeft()
    {
        Destroy(gameObject);
    }
}
