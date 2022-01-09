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
    
    [SerializeField] private uint PointsDestroyBig = 10;
    [SerializeField] private uint PointsDestroyMed = 20;
    [SerializeField] private uint PointsDestroySmall = 40;
    [SerializeField] private uint PointsDestroyTiny = 80;
    
    [SerializeField] private int HealthBig = 4;
    [SerializeField] private int HealthMed = 3;
    [SerializeField] private int HealthSmall = 2;
    [SerializeField] private int HealthTiny = 1;

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
    
    public uint GetPoints(AsteroidType asteroidType)
    {
        switch (asteroidType)
        {
            case AsteroidType.Big:
                return PointsDestroyBig;
            case AsteroidType.Med:
                return PointsDestroyMed;
            case AsteroidType.Small:
                return PointsDestroySmall;
            case AsteroidType.Tiny:
                return PointsDestroyTiny;
        }

        return PointsDestroyBig;
    }

    public float GetHealths(AsteroidType asteroidType)
    {
        switch (asteroidType)
        {
            case AsteroidType.Big:
                return HealthBig;
            case AsteroidType.Med:
                return HealthMed;
            case AsteroidType.Small:
                return HealthSmall;
            case AsteroidType.Tiny:
                return HealthTiny;
        }

        return PointsDestroyBig;
    }
    
}
