using UnityEngine;

public class AsteroidSpawner : Singleton<AsteroidSpawner>
{
    public Asteroid asteroidPrefab;
    public ShopAsteroid shopPrefab;

    //public Asteroid[] asteroids;
    public Powerup[] powerups;

    //TwinShotPickup, FullAutoPickup, MagBuffPickup

    public float trajectoryVariance = 15.0f;
    public float spawnRate = 2.0f;
    public float spawnDistance = 15.0f;
    public int spawnAmount = 1;

    public bool canSpawnItems = true;
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }
    private void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetTrajectory(rotation * -spawnDirection);

            //Shop
        }
    }
}