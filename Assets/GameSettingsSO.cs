using UnityEngine;

public class GameSettingsSO : ScriptableObject
{
    public enum AsteroidIncomingDirection {
        random,
        top,
        down,
        left,
        right
    }

    public enum PlayerShipMode {
        hyperspeed,
        rapidfire,
        normal
    }

    public PlayerShipMode _playershipmode;
    public AsteroidIncomingDirection _asteroidsincomingdirection;
    
    public float AsteroidRotationSpeed;
    public int PlayerDamage;
    public int AsteroidDamage;
    public GameObject AsteroidPrefab;
}
