using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private AudioSource audioSource;
    public GameManager gameManager; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true; 
    }

    void Update()
    {
        if (gameManager.isGameOver)
        {
            audioSource.Stop(); 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Stop(); 
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameManager != null)
        {
            gameManager.PlayerDied(); 
            audioSource.Stop(); 
        }
    }
}