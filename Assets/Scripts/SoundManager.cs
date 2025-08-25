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
    public AudioSource PlaySoundClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volume;



        audioSource.Play();

        Destroy(audioSource.gameObject, audioSource.clip.length);

        return audioSource; 
    }

    public void PlayRandomSoundClip(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {
        //assign a random index
        int rand = Random.Range(0, audioClip.Length); 

        //spawn in game
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);

        //assign audio clip
        audioSource.clip = audioClip[rand];

        audioSource.volume = volume;

        //play sound
        audioSource.Play();

        //get length
        float clipLength = audioSource.clip.length;

        //destroy clip after its done
        Destroy(audioSource.gameObject, clipLength);

    }
}
