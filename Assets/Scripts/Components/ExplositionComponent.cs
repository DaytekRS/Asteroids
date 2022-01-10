using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplositionComponent : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private float _deadAnimDuration = 0.3f;
    [SerializeField] private AudioClip _explosionSound;
    
    private GameObject _explositionGO;

    public void StartPlayExplosition()
    {
        _explositionGO = Instantiate(_explosionPrefab, transform);
        _explositionGO.transform.localPosition = new Vector3(0, 0, -2);
        Utils.PlayAudio(GetComponent<AudioSource>(), _explosionSound);
        Invoke("EndExplositionDuration", _deadAnimDuration);
    }
    
    //Ends animation when the specified time has elapsed
    private void EndExplositionDuration()
    {
        Destroy(_explositionGO);
        transform.localPosition = new Vector3(0, 0, -1);
        GetComponent<PolygonCollider2D>().enabled = true;
        GetComponent<Player>().ActivateShield();
    }

    public bool NowPlayExplosition()
    {
        return _explositionGO != null;
    }
}
