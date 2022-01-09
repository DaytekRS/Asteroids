using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    void Update()
    {
        GameObject Player = transform.parent.GetComponent<PlayerUI>().GetPlayer();
        var score = Player.GetComponent<ScoreСomponent>().GetScore();
        GetComponent<Text>().text = "SCORE: " + score;
    }
}
