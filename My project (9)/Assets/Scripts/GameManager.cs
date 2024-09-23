using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class GameManager : MonoBehaviour
{
    public GameObject deathPanel; 
    public GameObject victoryPanel; 
    public TMP_Text finalTimeText; 
    public TMP_Text timeDisplay; 
    public GameObject[] keyImages; 
    public Sprite[] keySprites; 

    private float timeElapsed = 0f; 
    public bool isGameOver = false; 
    private int keysCollected = 0;

    void Update()
    {
        if (!isGameOver)
        {
            timeElapsed += Time.deltaTime; 
            UpdateTimeDisplay(); 
        }
    }

    private void UpdateTimeDisplay()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        timeDisplay.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    public void CollectKey(int index)
    {
        if (index >= 0 && index < keyImages.Length)
        {
            keyImages[index].SetActive(true); 
            keyImages[index].GetComponent<Image>().sprite = keySprites[index]; // Change the sprite to the key sprite
            keysCollected++;
        }
    }

    public void UseKey(int index)
    {
        if (index >= 0 && index < keyImages.Length)
        {
            keyImages[index].SetActive(false); // Hide the key sprite when used
        }
    }

    public bool HasKey(int index)
    {
        return index < keysCollected; // Check if the player has collected keys up to the specified index
    }

    public void PlayerWon()
    {
        isGameOver = true; // Set the game state
        DisplayFinalTime(); 
        victoryPanel.SetActive(true); 
        timeDisplay.gameObject.SetActive(false); // Hide the current time text
        Time.timeScale = 0; // Pause the game
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true; 
    }

    private void DisplayFinalTime()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        finalTimeText.text = string.Format("Your time: {0:00}:{1:00}", minutes, seconds);
    }

    public void RestartGame()
    {
        Time.timeScale = 1; 
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1; 
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true; 
        SceneManager.LoadScene("MainMenu"); 
    }

    public void PlayerDied()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(true); // Show the death panel
        }
        Time.timeScale = 0; // Stop the game
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isGameOver = true; 

        // Stop sound on all obstacles
        foreach (var obstacle in FindObjectsOfType<Obstacle>())
        {
            obstacle.GetComponent<AudioSource>().Stop();
        }
    }
    public bool IsGameOver()
    {
        return isGameOver;
    }
}