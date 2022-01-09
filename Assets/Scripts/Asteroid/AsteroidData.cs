using UnityEngine;
using UnityEngine.UI;

public class AsteroidType
{
    public const uint Big = 0;
    public const uint Med = 1;
    public const uint Small = 2;
    public const uint Tiny = 3;
    public const uint CountType = 4;
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
    
    [SerializeField] private uint HealthBig = 4;
    [SerializeField] private uint HealthMed = 3;
    [SerializeField] private uint HealthSmall = 2;
    [SerializeField] private uint HealthTiny = 1;
    
    [SerializeField] private uint CountsShardsBig = 3;
    [SerializeField] private uint CountsShardsMed = 2;
    [SerializeField] private uint CountsShardsSmall = 1;
    [SerializeField] private uint CountsShardsTiny = 0;

    public Sprite [] GetSprites(uint asteroidType)
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
    
    public uint GetPoints(uint asteroidType)
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

        return 0;
    }

    public uint GetHealths(uint asteroidType)
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

        return 0;
    }
    
    public uint GetCountShards(uint asteroidType)
    {
        switch (asteroidType)
        {
            case AsteroidType.Big:
                return CountsShardsBig;
            case AsteroidType.Med:
                return CountsShardsMed;
            case AsteroidType.Small:
                return CountsShardsSmall;
            case AsteroidType.Tiny:
                return CountsShardsTiny;
        }
        return 0;
    }
    
}
