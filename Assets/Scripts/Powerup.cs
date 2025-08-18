using UnityEngine;

public class Powerup : MonoBehaviour

{
    [SerializeField] private AudioClip[] damageSoundClips;

    public ParticleSystem Explode;

    public PowerupEffect powerupEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            powerupEffect.Apply(collision.gameObject);
        }

        else if (collision.gameObject.tag == "Bullet")
        {
            SoundManager.instance.PlayRandomSoundClip(damageSoundClips, transform, 1f);

            Destroy(gameObject);
            this.Explode.transform.position = this.transform.position;
            this.Explode.Play();
        }
    }
}
