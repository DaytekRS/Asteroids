using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeaponComponent : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    public void FireStart()
    {
        Transform canvas = AutoTeleport.FindGameCanvas(transform);
        if (canvas)
        {
            GameObject obj = Instantiate(bulletPrefab, canvas);
            obj.transform.localPosition = transform.localPosition;
            obj.transform.up = transform.up;
        }
    }
}