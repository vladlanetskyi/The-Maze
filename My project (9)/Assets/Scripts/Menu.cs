using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    public GameObject loadingscreen, menuObj, settingsObj, controlsObj;
    public string sceneName;
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame()
    {
        loadingscreen.SetActive(true);
        PlayerPrefs.SetInt("level", 1);
        PlayerPrefs.Save();

        // Start the coroutine to delay before loading the scene
        StartCoroutine(LoadSceneWithDelay(1.5f)); 
    }

    public void SettingsMenu()
    {
        menuObj.SetActive(false);
        settingsObj.SetActive(true);
    }

    public void ControlsMenu()
    {
        menuObj.SetActive(false);
        controlsObj.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("This will quit the game, only works in actual build, not in Unity editor.");
        Application.Quit();
    }

    public void backToMenu()
    {
        settingsObj.SetActive(false);
        controlsObj.SetActive(false);
        menuObj.SetActive(true);
    }

    private IEnumerator LoadSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}