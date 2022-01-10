using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    //if you go beyond the screen, give the opposite position 
    public static Vector3 WrapScreen(Vector3 position, Rect rectTeleport)
    {
        if (position.x < rectTeleport.x) position.x += rectTeleport.width;
        else if (position.x > rectTeleport.xMax) position.x -= rectTeleport.width;

        if (position.y < rectTeleport.y) position.y += rectTeleport.height;
        else if (position.y > rectTeleport.yMax) position.y -= rectTeleport.height;

        return position;
    }
    
    //finds a link to root by tag 
    public static Transform FindRootWithTag(Transform obj, string tag)
    {
        Transform canvas = obj.parent;
        while (canvas && !canvas.tag.Equals(tag))
        {
            canvas = canvas.parent;
        }
        return canvas;
    }

    public static void PlayAudio(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}