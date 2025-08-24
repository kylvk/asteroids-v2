using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private AudioClip[] damageSoundClips;
    [SerializeField] private AudioClip equipSoundClip;

    public ParticleSystem Explode;
    public PowerupEffect powerupEffect;

    public string displayName = "Powerup";
    public int price = 10;                  // Set by ShopManager
    public bool isShopItem = false;         

    private bool isEquipping = false;
    private bool purchased = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isEquipping || !collision.CompareTag("Player")) return;

        if (isShopItem)
        {
            if (!purchased && GameManager.instance.TrySpend(price))
            {
                purchased = true;
                EquipPowerup(collision.gameObject);
            }
        }
        else
        {
            EquipPowerup(collision.gameObject);
        }
    }

    private void EquipPowerup(GameObject player)
    {
        if (isEquipping) return;

        isEquipping = true;
        SoundManager.instance.PlaySoundClip(equipSoundClip, transform, 1f);
        powerupEffect.Apply(player);
        Destroy(gameObject);
    }

    private void HandleBulletHit()
    {
        SoundManager.instance.PlayRandomSoundClip(damageSoundClips, transform, 1f);
        Explode.transform.position = transform.position;
        Explode.Play();
        Destroy(gameObject);
    }
}