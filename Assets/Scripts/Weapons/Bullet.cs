using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Sprite _bulletSprite;
    [SerializeField] private int _bulletSpeed = 10;
    private Transform _owner;
    private Rect _canvasRect;

    private void Awake()
    {
        Image image = GetComponent<Image>();
        image.sprite = _bulletSprite;
        
        RectTransform recTransform = GetComponent<RectTransform>();
        recTransform.sizeDelta = _bulletSprite.textureRect.size;

        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.size = _bulletSprite.textureRect.size;

        _canvasRect = transform.parent.GetComponent<RectTransform>().rect;
    }

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * _bulletSpeed;
    }

    private void Update()
    {
        if (!_canvasRect.Contains(transform.localPosition)) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Asteroid"))
        {
            Destroy(gameObject);
        }
    }

    public void SetOwner(Transform owner)
    {
        _owner = owner;
    }
    
    public Transform GetOwner()
    {
        return _owner;
    }
}