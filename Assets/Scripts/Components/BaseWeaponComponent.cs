using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeaponComponent : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private AudioClip[] _laserSounds;
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

    private void Shoot()
    {
        Transform canvas = AutoTeleport.FindGameCanvas(transform);
        if (canvas)
        {
            GameObject obj = Instantiate(bulletPrefab, canvas);
            obj.transform.localPosition = transform.localPosition;
            obj.transform.up = transform.up;
            obj.GetComponent<Bullet>().owner = transform;
            _audioSource.clip = _laserSounds[_laserSoundsIndex];
            _audioSource.Play();
            _laserSoundsIndex = (int) Mathf.Repeat(++_laserSoundsIndex, _laserSounds.Length);
        }
    }
}