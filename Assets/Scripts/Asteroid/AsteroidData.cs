using UnityEngine;
using UnityEngine.UI;

public enum AsteroidType
{
    Big,
    Med,
    Small,
    Tiny
}

public class AsteroidData : MonoBehaviour
{
    [SerializeField] private Sprite [] Big;
    [SerializeField] private Sprite [] Med;
    [SerializeField] private Sprite [] Small;
    [SerializeField] private Sprite [] Tiny;

    public Sprite [] GetSprites(AsteroidType asteroidType)
    {
        switch (asteroidType)
        {
            case AsteroidType.Big:
                return Big;
            case AsteroidType.Med:
                return Med;
            case AsteroidType.Small:
                return Small;
            case AsteroidType.Tiny:
                return Tiny;
        }

        return Big;
    }
}
