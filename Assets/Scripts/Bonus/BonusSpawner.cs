using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _bonuses;
    [Range(0, 100)] [SerializeField] private int _bonusProcent;
    [SerializeField] private string _rootCanvasTag = "GameCanvas";

    public void SpawnBonus()
    {
        if (Random.Range(0, 100) > _bonusProcent) return;
        Transform canvas = Utils.FindRootWithTag(transform, _rootCanvasTag);
        if (canvas)
        {
            int index = Random.Range(0, _bonuses.Length);
            GameObject bonus = Instantiate(_bonuses[index], canvas);
            bonus.transform.localPosition = transform.localPosition;
        }
    }
}