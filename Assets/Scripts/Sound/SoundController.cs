using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* 音乐音效控制 */
public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    public AudioSource audioSource;
    public AudioSource audioSource1;
    public AudioClip playTackDamageClip;
    public AudioClip playAttackClip;
    public AudioClip playDieClip;
    public AudioClip pickupEXPClip;
    public Slider musicSlider;
    public Slider soundSlider;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayTackDamageSound()
    {
        audioSource1.PlayOneShot(playTackDamageClip);
    }
    public void PlayAttackDamageSound()
    {
        audioSource1.PlayOneShot(playAttackClip);
    }
    public void PlayDieSound()
    {
        audioSource1.PlayOneShot(playDieClip);
    }
    public void PlayPickupEXPSound()
    {
        audioSource1.PlayOneShot(pickupEXPClip);
    }
    public void StopSounds()
    {
        audioSource.Stop();
        audioSource1.Stop();
    }
    public void SetMusicVolume(float volume)
    {
        audioSource.volume = volume;
    }
    public void SetSoundVolume(float volume)
    {
        audioSource1.volume = volume;
    }
}
