using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // with code modified from https://www.youtube.com/watch?v=DU7cgVsU2rM
    // and https://www.youtube.com/watch?app=desktop&v=xswEpNpucZQ
    public static AudioManager instance;

    [SerializeField] private AudioSource soundFXObject;
    [SerializeField] private AudioSource musicSource;
    public string currState;

    public AudioClip SunlightBGM;
    public AudioClip TwilightBGM;
    public AudioClip HomebaseBGM;

    public AudioClip bgm;


    private void Awake()
    {
        Debug.Log("AudioManager is awake. currState is" + currState);

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // musicSource.clip = bgm;
        // musicSource.Play();

        // if (instance == null)
        // {
        //     instance = this;
        // }

        if (currState == "Sunlight Zone")
        {
            Debug.Log("currState is" + currState);
            musicSource.clip = SunlightBGM;
            musicSource.Play();
        }
        else if (currState == "Underwater Base")
        {
            Debug.Log("currState is" + currState);
            musicSource.clip = HomebaseBGM;
            musicSource.Play();
        }
        else
        {
            Debug.Log("At else. currState is" + currState);
            musicSource.clip = SunlightBGM;
            musicSource.Play();
            return;
        }

    }

    public void PlaySoundClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // spawn in gameObject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        // assign the audioClip
        audioSource.clip = audioClip;

        // assign volume
        audioSource.volume = volume;

        // play sound
        audioSource.Play();

        // // get length of sound effect clip
        float clipLength = audioSource.clip.length;

        // // destroy the clip after it is done playing
        Destroy(audioSource.gameObject, clipLength);
    }
}
