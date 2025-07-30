using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] damageSoundClips;
    [SerializeField] private AudioClip[] hurtSoundClips;
    [SerializeField] private AudioClip deathSoundClip;

    public Player player;
    public ParticleSystem Explode;
    public ScreenFlashUI flash;
    public int lives = 3;
    public int score = 0;

    public GameObject GameOverPanel;

    private void Awake()
    {
        GameOverPanel.SetActive(false);
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {

        SoundManager.instance.PlayRandomSoundClip(damageSoundClips, transform, 1f);

        this.Explode.transform.position = asteroid.transform.position;
        this.Explode.Play();

        if (asteroid.size < 0.75f)
        {
            this.score += 100;
        }
        else if (asteroid.size < 1.0f)
        {
            this.score += 50;
        }
        else
        {
            this.score += 25;
        }
    }


    public float respawnTime = 3.0f;
    public float respawnInvTime = 3.0f;

    public void PlayerDied()
    {
        StartCoroutine(flash.FlashRoutine());

        SoundManager.instance.PlayRandomSoundClip(hurtSoundClips, transform, 1f);

        this.Explode.transform.position = this.player.transform.position;
        this.Explode.Play();

        this.lives--;

        if (this.lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    private void Respawn()
    {

        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        this.player.gameObject.SetActive(true);

        Invoke(nameof(TurnOnCollisions), this.respawnInvTime);
    }
    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
    private void GameOver()
    {
        SoundManager.instance.PlaySoundClip(deathSoundClip, transform, 1f);

        GameOverPanel.SetActive(true);
       // this.lives = 3;
       // this.score = 0;
       // Invoke(nameof(Respawn), this.respawnTime);
    }

    

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }
    public void SetHighScore(int score)
    {
        PlayerPrefs.SetInt("HighScore", score);
    }
}