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

            float rand = Random.value;
            //shop spawn, shop cant spawn when in shop
            if (rand < 0.1f && (ShopManager.instance == null || !ShopManager.instance.InShop))
            {
                ShopAsteroid shop = Instantiate(this.shopPrefab, spawnPoint, rotation, this.transform);
                shop.SetTrajectory(rotation * -spawnDirection); 
            }
            else
            {
                Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation, this.transform);
                asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
                asteroid.SetTrajectory(rotation * -spawnDirection);
            }

            // spawn powerup ?? might get rid of
            if (canSpawnItems && Random.value < 0.05f && powerups.Length > 0)
            {
                int index = Random.Range(0, powerups.Length);
                Instantiate(powerups[index], spawnPoint, Quaternion.identity, this.transform);
            }
        }
    }
}