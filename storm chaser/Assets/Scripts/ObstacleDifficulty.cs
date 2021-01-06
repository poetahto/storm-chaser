using UnityEngine;

[CreateAssetMenu]
public class ObstacleDifficulty : ScriptableObject
{
    public float initialSpawnRatePerMin = 10;
    public float finalSpawnRatePerMin = 60;

    public float initialVelocityMult = 5;
    public float finalVelocityMult = 10;
    
    public float initialYBoost = 2;
    public float finalYBoost = 2;
    
    public float initialTorque = 50;
    public float finalTorque = 50;
}