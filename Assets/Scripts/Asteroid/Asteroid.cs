using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    [Header("Display")] 
    [SerializeField] private Sprite[] _spritesAsteroid;
    [SerializeField] private string _rootCanvasTag = "GameCanvas";

    [Header("Movement Settings")] [SerializeField]
    private float _speedMax = 5f;

    [SerializeField] private float _speedMin = 3f;

    [Header("Score system")] [SerializeField]
    private uint _destroyPoints = 10;

    [Header("Hit Settings")] [SerializeField]
    private AudioClip _hitSound;

    [SerializeField] private float _hitAnimDuration = 0.3f;

    [Header("Decay asteroid on shards")] [SerializeField]
    private float _countDecayShardsAsteroid = 3f;

    [SerializeField] private GameObject _prefabShard;


    private Vector3 _forwardVector = Vector3.zero;
    private Rect _canvasRect;
    private float _speed;

    private void Start()
    {
        AutoGenerateAsteroid();
        _speed = Random.Range(_speedMin, _speedMax);
        Transform canvas = Utils.FindRootWithTag(transform, _rootCanvasTag);
        if (canvas) _canvasRect = canvas.GetComponent<RectTransform>().rect;
    }

    void Update()
    {
        MoveAsteroid();
    }

    private void SetRandomAsteroidSprite()
    {
        if (_spritesAsteroid.Length == 0) return;

        int index = Random.Range(0, _spritesAsteroid.Length);
        Sprite sprite = _spritesAsteroid[index];
        GetComponent<Image>().sprite = sprite;
        GetComponent<RectTransform>().sizeDelta = sprite.textureRect.size;
        GetComponent<CapsuleCollider2D>().size = sprite.textureRect.size;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Handles the match with the bullet
        if (collider.tag.Equals("Bullet"))
        {
            Utils.PlayAudio(GetComponent<AudioSource>(), _hitSound);
            GetComponent<HealthComponents>().DecHealth();
            if (GetComponent<HealthComponents>().IsDead())
            {
                Transform bulletOwner = collider.GetComponent<Bullet>().GetOwner();
                bulletOwner.GetComponent<ScoreСomponent>().AddScore(_destroyPoints);
                OnDead();
            }
            else
            {
                GetComponent<Animator>().SetBool("HaveHit", true);
                Invoke("EndHitAnim", _hitAnimDuration);
            }
        }
    }

    private void OnDead()
    {
        EnabledComponents(false); //disable so that he could not contact objects until he is destroyed 
        DecayOnShards();
        GetComponent<BonusSpawner>().SpawnBonus();
        Destroy(gameObject, _hitSound.length);
    }

    private void EndHitAnim()
    {
        GetComponent<Animator>().SetBool("HaveHit", false);
    }

    private void AutoGenerateAsteroid()
    {
        SetRandomAsteroidSprite();
        _forwardVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
    }
    
    //Splits an asteroid into shards
    private void DecayOnShards()
    {
        if (_prefabShard == null) return;
        for (int i = 0; i < _countDecayShardsAsteroid; i++)
        {
            GameObject obj = Instantiate(_prefabShard, transform.parent);
            obj.transform.localPosition = transform.localPosition;
        }
    }

    private void MoveAsteroid()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + _forwardVector,
            Time.deltaTime * _speed);
        transform.localPosition = Utils.WrapScreen(transform.localPosition, _canvasRect);
    }

    private void EnabledComponents(bool enabled)
    {
        GetComponent<Image>().enabled = enabled;
        GetComponent<CapsuleCollider2D>().enabled = enabled;
    }
}