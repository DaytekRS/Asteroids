using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private int _minCountAsteroid = 5;
    [SerializeField] private GameObject asteroidPrefab;
    private Rect _CanvasRect;

    private void Start()
    {
        Transform canvas = AutoTeleport.FindGameCanvas(transform);
        if (canvas) _CanvasRect = canvas.GetComponent<RectTransform>().rect;
    }

    private void Update()
    {
        while (transform.childCount < _minCountAsteroid)
        {
            GameObject obj = Instantiate(asteroidPrefab, transform);
            obj.transform.localPosition = RandomLocalPosition();
        }
    }

    private Vector3 RandomLocalPosition()
    {
        int randomAxis = Random.Range(0, 1);
        float randomValue = randomAxis == 0
            ? Random.Range(_CanvasRect.yMin, _CanvasRect.yMax)
            : Random.Range(_CanvasRect.xMin, _CanvasRect.xMax);
        return randomAxis == 0
            ? new Vector3(_CanvasRect.xMin, randomValue, -1f)
            : new Vector3(randomValue, _CanvasRect.yMin, -1f);
    }
}