using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] Bonuses;
    [SerializeField] private int BonusProcent;

    public void SpawnBonus()
    {
        if (Random.Range(0, 100) > BonusProcent) return;
        Transform canvas = AutoTeleport.FindGameCanvas(transform);
        if (canvas)
        {
            int index = Random.Range(0, Bonuses.Length - 1);
            GameObject bonus = Instantiate(Bonuses[index], canvas);
            bonus.transform.localPosition = transform.localPosition;
            print(transform.gameObject.name);
        }
    }
}