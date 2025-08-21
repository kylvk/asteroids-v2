using Unity.VisualScripting;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private AudioClip ShopMusic;


    public GameObject ShopEnvironment;
    public GameObject ShopAsteroid;

    public static ShopManager instance;

    void Start()
    {
        instance = this;
        ShopEnvironment.SetActive(false);
    }

    public void EnterShop()
    {
        Debug.Log("entering shop");
        Destroy(ShopAsteroid);

        ShopEnvironment.SetActive(true);
        GameManager.instance.Respawn(); //for position?

        SoundManager.instance.PlaySoundClip(ShopMusic, transform, 1f);

    }
    public void ExitShop()
    {
        ShopEnvironment.SetActive(false);
        GameManager.instance.Respawn(); //for position?
    }
}
