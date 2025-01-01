using UnityEngine;

public class AudioSFX : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gameOverClip;
    [SerializeField] private AudioClip magicMissileLaunchClip;
    [SerializeField] private AudioClip takeDamageClip;

    public void PlayGameOver()
    {
        audioSource.PlayOneShot(gameOverClip);
    }
    public void PlayMagicMissileLaunchCli()
    {
        audioSource.PlayOneShot(magicMissileLaunchClip);
    }
    public void PlayTakeDamageClip()
    {
        audioSource.PlayOneShot(takeDamageClip);
    }
}