using UnityEngine;

public enum SoundType
{
    ENEMY_BULLET,
    PLAYER_BULLET,
    ENEMY_DESTROYED,
    PLAYER_DESTROYED
}


[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    
    public static SoundManager instance;
    
    [SerializeField] private AudioClip[] soundList;
    [SerializeField] private AudioSource SFXAudioSource;
    
    
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioClip musicClip;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        PlayMusic();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        instance.SFXAudioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }

    public static void PlaySoundAtPosition(SoundType sound, Vector3 position, float volume = 1)
    {
        AudioClip clip = instance.soundList[(int)sound];
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    public void PlayMusic()
    {
        musicAudioSource.clip = musicClip;
        musicAudioSource.loop = true;
        musicAudioSource.spatialBlend = 0;
        musicAudioSource.Play();
    }

    public void StopMusic()
    {
        musicAudioSource.Stop();
    }
}
