using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTeleport : MonoBehaviour
{
    public static Vector3 Teleport(Vector3 position, Rect rectTeleport)
    {
        if (position.x < rectTeleport.x) position.x += rectTeleport.width;
        else if (position.x > rectTeleport.xMax) position.x -= rectTeleport.width;

        if (position.y < rectTeleport.y) position.y += rectTeleport.height;
        else if (position.y > rectTeleport.yMax) position.y -= rectTeleport.height;

        return position;
    }

    public static Transform FindGameCanvas(Transform obj)
    {
        Transform canvas = obj.parent;
        while (canvas && !canvas.tag.Equals("GameCanvas"))
        {
            canvas = canvas.parent;
        }

        return canvas;
    }
}