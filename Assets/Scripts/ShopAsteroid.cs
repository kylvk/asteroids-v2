using UnityEngine;

public class ShopAsteroid : MonoBehaviour
{

    [SerializeField] private AudioClip[] damageSoundClips;

    public ParticleSystem Explode;
    private Rigidbody2D rb;

    public float speed = 50.0f;
    public float maxLifetime = 30.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetTrajectory(Vector2 direction)
    {
        rb.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            ShopManager.instance.EnterShop();
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            SoundManager.instance.PlayRandomSoundClip(damageSoundClips, transform, 1f);
            this.Explode.transform.position = this.transform.position;
            this.Explode.Play();
            Destroy(gameObject);
            
        }
    }
}
