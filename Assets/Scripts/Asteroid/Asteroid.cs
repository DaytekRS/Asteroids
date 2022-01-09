using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _speedMax = 5f;
    [SerializeField] private float _speedMin = 3f;
    [SerializeField] private AsteroidData[] _asteroidDatas = { };
    [SerializeField] public uint _asteroidType = AsteroidType.Big;
    [SerializeField] private bool _autoGenerate = true;
    [SerializeField] private float _hitAnimDuration = 0.3f;
    [SerializeField] private AudioClip hitSound;

    private Vector3 ForwardVector = Vector3.zero;
    private Rect _CanvasRect;
    private float _speed;
    public AsteroidData _currentAsteroidData;


    private void Start()
    {
        if (_autoGenerate) AutoGenerateAsteroid();
        SetAsteroidData(_currentAsteroidData);
        _speed = Random.Range(_speedMin, _speedMax);
        Transform canvas = AutoTeleport.FindGameCanvas(transform);
        if (canvas) _CanvasRect = canvas.GetComponent<RectTransform>().rect;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + ForwardVector,
            Time.deltaTime * _speed);
        transform.localPosition = AutoTeleport.Teleport(transform.localPosition, _CanvasRect);
    }

    private void SetRandomAsteroid()
    {
        if (_asteroidDatas.Length > 0)
        {
            int index = Random.Range(0, _asteroidDatas.Length);
            _currentAsteroidData = _asteroidDatas[index];
        }
    }

    public void SetAsteroidData(AsteroidData asteroidData)
    {
        if (asteroidData == null) return;
        int index = Random.Range(0, asteroidData.GetSprites(_asteroidType).Length);
        Sprite sprite = asteroidData.GetSprites(_asteroidType)[index];
        GetComponent<Image>().sprite = sprite;
        GetComponent<RectTransform>().sizeDelta = sprite.textureRect.size;
        GetComponent<CapsuleCollider2D>().size = sprite.textureRect.size;
        _currentAsteroidData = asteroidData;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        print(collider);
        if (collider.tag.Equals("Bullet"))
        {
            GetComponent<Animator>().SetBool("HaveHit", true);
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.clip = hitSound;
            audioSource.Play();
            GetComponent<HealthComponents>().DecHealth();
            Invoke("EndHitAnim", _hitAnimDuration);
            if (GetComponent<HealthComponents>().IsDead())
            {
                Transform bulletOwner = collider.GetComponent<Bullet>().owner;
                bulletOwner.GetComponent<ScoreСomponent>().AddScore(GetAsteroidPoints());
                OnDead();
            }
        }
    }

    private void OnDead()
    {
        DecayOnShards();
        GetComponent<Image>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<BonusSpawner>().SpawnBonus();
        //transform.parent.GetComponent<AsteroidSpawner>().DecayOnShards(this);
        Destroy(gameObject, hitSound.length);
       
    }

    private void EndHitAnim()
    {
        GetComponent<Animator>().SetBool("HaveHit", false);
    }

    private void AutoGenerateAsteroid()
    {
        SetRandomAsteroid();
        ForwardVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
    }

    public uint GetAsteroidPoints()
    {
        return _currentAsteroidData.GetPoints(_asteroidType);
    }
    
    public void DecayOnShards()
    {
        if (_asteroidType < AsteroidType.CountType - 1)
        {
            uint newAsteroidType = _asteroidType + 1;
            for (int i = 0; i < _currentAsteroidData.GetCountShards(_asteroidType); i++)
            {
                GameObject obj = Instantiate(gameObject, transform.parent);
                obj.transform.localPosition = transform.localPosition;
                obj.GetComponent<Asteroid>()._asteroidType = newAsteroidType;
                obj.GetComponent<Asteroid>().SetAsteroidData(_currentAsteroidData);
            }
        }
    }
    
}