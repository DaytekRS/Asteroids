using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    public GameObject GetPlayer()
    {
        return Player;
    }
}