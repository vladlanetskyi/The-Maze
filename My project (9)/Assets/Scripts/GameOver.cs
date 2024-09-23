using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject deathmenu, pausemenu, settingsmenu;
    public string sceneName;
   

    public void PlayGame()
    {
        Time.timeScale = 1;

        AudioListener.pause = false;

        SceneManager.LoadScene(sceneName);
    }
}
