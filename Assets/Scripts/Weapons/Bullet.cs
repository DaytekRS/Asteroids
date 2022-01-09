using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Sprite _BulletSprite;
    [SerializeField] private int _BulletSpeed = 10;
    public Transform owner;
    private Rect _CanvasRect;

    private void Awake()
    {
        Image image = GetComponent<Image>();
        image.sprite = _BulletSprite;
        
        RectTransform recTransform = GetComponent<RectTransform>();
        recTransform.sizeDelta = _BulletSprite.textureRect.size;

        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.size = _BulletSprite.textureRect.size;

        _CanvasRect = transform.parent.GetComponent<RectTransform>().rect;
    }

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * _BulletSpeed;
    }

    private void Update()
    {
        if (!_CanvasRect.Contains(transform.localPosition)) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Asteroid"))
        {
            Destroy(gameObject);
        }
    }
}