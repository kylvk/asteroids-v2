using UnityEngine;

public class SoundManager : MonoBehaviour
{
public static SoundManager instance;

    [SerializeField] private AudioSource soundObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void PlaySoundClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //spawn in game
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);

        //assign audio clip
        audioSource.clip = audioClip;

        audioSource.volume = volume;

        //play sound
        audioSource.Play();

        //get length
        float clipLength = audioSource.clip.length;

        //destroy clip after its done
        Destroy(audioSource.gameObject, clipLength);

    }
}
