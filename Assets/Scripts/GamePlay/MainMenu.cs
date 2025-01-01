using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public string fristLevelName;
    public void StartGame()
    {
        audioSource.PlayOneShot(clip);
        SceneManager.LoadScene(fristLevelName);
    }
    public void QuitGame()
    {
        audioSource.PlayOneShot(clip);
        Application.Quit();
    }

}
