using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    public GameObject GetPlayer()
    {
        return Player;
    }
}