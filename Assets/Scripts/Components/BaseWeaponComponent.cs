using UnityEngine;

public class BaseWeaponComponent : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private AudioClip[] _laserSounds;
    [SerializeField] private string _rootCanvasTag = "GameCanvas";
    private AudioSource _audioSource;
    private int _laserSoundsIndex = 0;

    private void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.loop = false;
        _audioSource.playOnAwake = false;
    }

    public void FireStart()
    {
        Shoot();
    }

    private void CreateBullet(Transform root, Vector3 position)
    {
        GameObject obj = Instantiate(_bulletPrefab, root);
        obj.transform.localPosition = position;
        obj.transform.up = transform.up;
        obj.GetComponent<Bullet>().SetOwner(transform);
    }

    private void PlaySoundLaser()
    {
        Utils.PlayAudio(_audioSource, _laserSounds[_laserSoundsIndex]);
        _laserSoundsIndex = (int) Mathf.Repeat(++_laserSoundsIndex, _laserSounds.Length);
    }
    
    private void Shoot()
    {
        Transform canvas = Utils.FindRootWithTag(transform, _rootCanvasTag);
        if (canvas)
        {
            CreateBullet(canvas, transform.localPosition);
            PlaySoundLaser();
        }
    }
}