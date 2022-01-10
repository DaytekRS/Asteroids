using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private int _minCountAsteroid = 5;
    [SerializeField] private GameObject[] _asteroidPrefabs = { };
    [SerializeField] private float _delaySpawnAsteroid = 7f;
    [SerializeField] private string _rootCanvasTag = "GameCanvas";
    private Rect _canvasRect;

    private void Start()
    {
        if (_asteroidPrefabs.Length == 0) Debug.LogWarning("Not set asteroid spawn prefabs", transform);

        Transform canvas = Utils.FindRootWithTag(transform, _rootCanvasTag);
        if (canvas) _canvasRect = canvas.GetComponent<RectTransform>().rect;
        else Debug.LogWarning("Should be located on canvas with GameCanvas tag", transform);

        Invoke("OnTimeSpawnAsteroidLeft", _delaySpawnAsteroid);
    }


    private void SpawnRandomAsteroid()
    {
        if (_asteroidPrefabs.Length == 0) return;
        int index = Random.Range(0, _asteroidPrefabs.Length);
        GameObject obj = Instantiate(_asteroidPrefabs[index], transform);
        obj.transform.localPosition = RandomLocalPosition();
    }

    private void Update()
    {
        if (_asteroidPrefabs.Length > 0)
        {
            while (transform.childCount < _minCountAsteroid)
            {
                SpawnRandomAsteroid();
            }
        }
    }

    private Vector3 RandomLocalPosition()
    {
        int randomAxis = Random.Range(0, 1);
        float randomValue = randomAxis == 0
            ? Random.Range(_canvasRect.yMin, _canvasRect.yMax)
            : Random.Range(_canvasRect.xMin, _canvasRect.xMax);
        return randomAxis == 0
            ? new Vector3(_canvasRect.xMin, randomValue, -1f)
            : new Vector3(randomValue, _canvasRect.yMin, -1f);
    }

    private void OnTimeSpawnAsteroidLeft()
    {
        SpawnRandomAsteroid();
        Invoke("OnTimeSpawnAsteroidLeft", _delaySpawnAsteroid);
    }
}