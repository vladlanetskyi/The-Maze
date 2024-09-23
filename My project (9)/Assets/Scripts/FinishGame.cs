using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{
    public GameManager gameManager; 

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") && gameManager != null)
        {
            gameManager.PlayerWon(); 
        }
    }
}
