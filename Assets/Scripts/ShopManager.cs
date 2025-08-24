using Unity.VisualScripting;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private AudioClip ShopMusic;


    public GameObject ShopEnvironment;
    public GameObject ShopAsteroid;
    public GameObject ShopExit;


    public static ShopManager instance;
    private AudioSource shopMusicSource;

    void Start()
    {
        instance = this;
        ShopEnvironment.SetActive(false);
    }

    public void EnterShop()
    {
        Debug.Log("entering shop");

        ShopEnvironment.SetActive(true);
        GameManager.instance.Respawn();

        shopMusicSource = SoundManager.instance.PlaySoundClip(ShopMusic, transform, 1f);
    }
    public void ExitShop()
    {
        ShopEnvironment.SetActive(false);
        GameManager.instance.Respawn();

        if (shopMusicSource != null)
        {
            shopMusicSource.Stop();
            Destroy(shopMusicSource.gameObject);
            shopMusicSource = null;
        }
    }
}
