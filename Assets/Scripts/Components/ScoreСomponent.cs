using UnityEngine;

public class ScoreСomponent : MonoBehaviour
{
    private ulong _score = 0;

    public void AddScore(ulong increase)
    {
        if (increase <= 0) return;
        _score += increase;
    }

    public ulong GetScore()
    {
        return _score;
    }
}