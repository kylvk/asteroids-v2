using Unity.VisualScripting;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private AudioClip ShopMusic;
    [SerializeField] private AudioClip ShopBeep;

    public bool InShop { get; private set; }

    public GameObject ShopEnvironment;
    public GameObject ShopAsteroid;
    public GameObject ShopExit;

    public static ShopManager instance;
    private AudioSource shopMusicSource;

    public Transform shopItemPointA;
    public Transform shopItemPointB;
    public GameObject[] powerupPrefabs;

    public TMPro.TextMeshProUGUI itemAText;
    public TMPro.TextMeshProUGUI itemBText;

    public TMPro.TextMeshProUGUI itemAPriceText;
    public TMPro.TextMeshProUGUI itemBPriceText;

    void Start()
    {
        instance = this;
        ShopEnvironment.SetActive(false);
        
        GameObject.FindGameObjectsWithTag("Asteroid");

        if (ShopManager.instance != null && ShopManager.instance.InShop)
        {
            Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
            foreach (var col in colliders)
            {
                col.enabled = false;
            }
        }

    }

    public void EnterShop()
    {
        Debug.Log("entering shop");
        InShop = true;

        ShopEnvironment.SetActive(true);
        GameManager.instance.Respawn();

        SoundManager.instance.PlaySoundClip(ShopBeep, transform, 1f);

        shopMusicSource = SoundManager.instance.PlaySoundClip(ShopMusic, transform, 1f);

        DisableAllAsteroidColliders();

        SpawnShopItems();
    }
    public void ExitShop()
    {
        InShop = false;
        ShopEnvironment.SetActive(false);
        GameManager.instance.Respawn();

        SoundManager.instance.PlaySoundClip(ShopBeep, transform, 1f);

        if (shopMusicSource != null)
        {
            shopMusicSource.Stop();
            Destroy(shopMusicSource.gameObject);
            shopMusicSource = null;

            EnableAllAsteroidColliders();

            ClearShopItems();
        }
    }

    //collider bs (SOME ASPECTS OF THE FOLLOWING CODE INCLUDE CONCEPTS DERRIVED FROM AI QUERIES)
    private void DisableAllAsteroidColliders()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        Debug.Log($"Found {asteroids.Length} asteroids to disable");

        foreach (GameObject asteroid in asteroids)
        {
            Collider2D[] colliders = asteroid.GetComponentsInChildren<Collider2D>();

            foreach (Collider2D col in colliders)
            {
                col.enabled = false;
                Debug.Log($"Disabled collider on: {asteroid.name}");
            }
        }
    }

    private void EnableAllAsteroidColliders()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        foreach (GameObject asteroid in asteroids)
        {
            Collider2D collider = asteroid.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = true;
            }
        }
    }

    //item management (SOME ASPECTS OF THE FOLLOWING CODE INCLUDE CONCEPTS DERRIVED FROM AI QUERIES)
    private GameObject itemA;
    private GameObject itemB;

    private int priceA;
    private int priceB;

    private void SpawnShopItems()
    {
        if (powerupPrefabs.Length < 2)
        {
            Debug.LogWarning("add more powerups");
            return;
        }

        int indexA = Random.Range(0, powerupPrefabs.Length);

        int indexB;
        do
        {
            indexB = Random.Range(0, powerupPrefabs.Length);
        } while (indexB == indexA); // makes sure no two powerups are the same

        itemA = Instantiate(powerupPrefabs[indexA], shopItemPointA.position, Quaternion.identity, shopItemPointA);
        itemB = Instantiate(powerupPrefabs[indexB], shopItemPointB.position, Quaternion.identity, shopItemPointB);


        //declare and assign powerup a and b and prices (SOME ASPECTS OF THE FOLLOWING CODE INCLUDE CONCEPTS DERRIVED FROM AI QUERIES)
        Powerup powerupA = itemA.GetComponent<Powerup>();
        Powerup powerupB = itemB.GetComponent<Powerup>();

        powerupA.price = priceA;
        powerupA.isShopItem = true;
        powerupB.price = priceA;
        powerupB.isShopItem = true;

        priceA = Random.Range(1000, 3001);
        priceB = Random.Range(1000, 3001);

        itemAText.text = powerupA != null ? powerupA.displayName : "Unknown Item";
        itemBText.text = powerupB != null ? powerupB.displayName : "Unknown Item";

        itemAPriceText.text = priceA.ToString() + "$";
        itemBPriceText.text = priceB.ToString() + "$";

    }
    private void ClearShopItems()
    {
        if (itemA != null)
            Destroy(itemA);

        if (itemB != null)
            Destroy(itemB);

        itemAText.text = "";
        itemBText.text = "";

        itemAPriceText.text = "";
        itemBPriceText.text = "";
    }

    //purchasing logic (SOME ASPECTS OF THE FOLLOWING CODE INCLUDE CONCEPTS DERRIVED FROM AI QUERIES)
    public void BuyItemA()
    {
        if (itemA == null) return;

        if (GameManager.instance.TrySpend(priceA))
        {
            // Grant powerup (you can customize this)
            //GrantPowerup(itemA);
            Debug.Log("Purchased Item A for " + priceA);
            Destroy(itemA);
            itemAText.text = "SOLD";
            itemAPriceText.text = "";
        }
        else
        {
            Debug.Log("Not enough money to buy Item A.");
        }
    }

    public void BuyItemB()
    {
        if (itemB == null) return;

        if (GameManager.instance.TrySpend(priceB))
        {
            //GrantPowerup(itemB);
            Debug.Log("Purchased Item B for " + priceB);
            Destroy(itemB);
            itemBText.text = "SOLD";
            itemBPriceText.text = "";
        }
        else
        {
            Debug.Log("Not enough money to buy Item B.");
        }
    }
}
