using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);

        AkSoundEngine.PostEvent("stop_main_theme", gameObject);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
