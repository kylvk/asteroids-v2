using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private AudioClip[] damageSoundClips;
    [SerializeField] private AudioClip equipSoundClip;

    public ParticleSystem Explode;
    public PowerupEffect powerupEffect;
    public string displayName = "name goes here";

    // New variables for shop functionality (SOME ASPECTS OF THE FOLLOWING CODE INCLUDE CONCEPTS DERRIVED FROM AI QUERIES)
    public int price = 1000;         
    public bool isShopItem = false;  

    private bool purchased = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isShopItem)
            {
                // Only allow purchase if not already bought
                if (!purchased)
                {
                    if (GameManager.instance.TrySpend(price))
                    {
                        ApplyPowerup(collision.gameObject);
                        Debug.Log($"Purchased {displayName} for {price}.");
                        purchased = true;
                        Destroy(gameObject);
                    }
                    else
                    {
                        Debug.Log($"Not enough funds to buy {displayName} ({price}).");
                    }
                }
            }
            else
            {
                //incase implementing powerups to be spawned like asteroids
                ApplyPowerup(collision.gameObject);
                Destroy(gameObject);
            }
        }

        else if (collision.CompareTag("Bullet"))
        {
            SoundManager.instance.PlayRandomSoundClip(damageSoundClips, transform, 1f);
            this.Explode.transform.position = this.transform.position;
            this.Explode.Play();
            Destroy(gameObject);
        }
    }

    private void ApplyPowerup(GameObject player)
    {
        SoundManager.instance.PlaySoundClip(equipSoundClip, transform, 1f);
        powerupEffect.Apply(player);
    }
}