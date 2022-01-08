using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _speedMax = 5f;
    [SerializeField] private float _speedMin = 3f;
    [SerializeField] private AsteroidData[] _asteroidDatas = { };
    [SerializeField] private AsteroidType _asteroidType = AsteroidType.Big;
    private Vector3 ForwardVector;
    private Rect _CanvasRect;
    private float _speed;
    private AsteroidData _currentAsteroidData;
    private bool _autoGenerate = true;


    private void Start()
    {
        if (_autoGenerate) SetRandomAsteroid();
        SetAsteroidData(null);

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

    private void SetRandomAsteroid()
    {
        print(_asteroidDatas.Length);
        if (_asteroidDatas.Length > 0)
        {
            int index = Random.Range(0, _asteroidDatas.Length);
            _currentAsteroidData = _asteroidDatas[index];
        }
    }

    private void SetAsteroidData(AsteroidData asteroidData)
    {
        if (asteroidData == null) return;
        int index = Random.Range(0, asteroidData.GetSprites(_asteroidType).Length);
        Sprite sprite = asteroidData.GetSprites(_asteroidType)[index];
        print(sprite.rect);
        GetComponent<Image>().sprite = sprite;
        GetComponent<RectTransform>().sizeDelta = sprite.textureRect.size;
        GetComponent<CapsuleCollider2D>().size = sprite.textureRect.size;
    }
}