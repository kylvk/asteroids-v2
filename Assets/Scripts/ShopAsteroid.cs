using UnityEngine;

public class ShopAsteroid : MonoBehaviour
{

    [SerializeField] private AudioClip[] damageSoundClips;
    [SerializeField] private AudioClip equipSoundClip;

    public ParticleSystem Explode;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetTrajectory(Vector2 direction)
    {
        rb.AddForce(direction, ForceMode2D.Impulse);
    }


private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Player")
        //{
            
        //   Destroy(gameObject);
        //   ShopManager.instance.EnterShop();

        //}
       if (collision.gameObject.tag == "Bullet")
        {
            SoundManager.instance.PlayRandomSoundClip(damageSoundClips, transform, 1f);

            Destroy(gameObject);
            this.Explode.transform.position = this.transform.position;
            this.Explode.Play();
        }
    }
}
