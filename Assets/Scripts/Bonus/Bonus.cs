using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private string _trigerTag = "Player";
    [Range(0, 100)] [SerializeField] private float _timeLife = 5f;

    private void Start()
    {
        Invoke("OnTimeLeft", _timeLife); //timer for automatic destroy
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals(_trigerTag))
        {
            Destroy(gameObject);
        }
    }

    private void OnTimeLeft()
    {
        Destroy(gameObject);
    }
}