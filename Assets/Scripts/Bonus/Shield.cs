using UnityEngine;

public class Shield : MonoBehaviour
{
    [Tooltip("Working hours shield in sec")] [Range(0, 100)] [SerializeField]
    private float _shieldTimeLife = 5f;

    void Start()
    {
        Invoke("OnTimeLeft", _shieldTimeLife);
    }

    private void OnTimeLeft()
    {
        Destroy(gameObject);
    }
}