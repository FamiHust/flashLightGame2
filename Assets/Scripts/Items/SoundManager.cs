using UnityEngine;

public enum SoundType
{
    HURT,
    ENERMY,
    ATTACK,
    WALK,
    DIE,
    FLASHLIGHT,
    PLAYERDIE,
    BATTERY
}
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1f)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }

    public static void StopSound(SoundType sound)
    {
        if (sound == SoundType.WALK && instance.audioSource.isPlaying && instance.audioSource.clip == instance.soundList[(int)sound])
        {
            instance.audioSource.Stop(); // Stop the walking sound
        }
    }

}