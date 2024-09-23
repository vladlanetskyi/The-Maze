using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class Door : MonoBehaviour
{
    public GameObject lockedText; 
    public GameManager gameManager; 
    public int keyIndex; 
    public Animator doorAnim; 
    public AudioSource doorOpenSound; 
    public AudioSource lockedSound; 
    private bool interactable;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            interactable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
           
            interactable = false;
        }
    }

    void Update()
    {
        if (interactable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (gameManager.HasKey(keyIndex))
                {
                    doorAnim.SetTrigger("open"); // Open the door
                    doorOpenSound.Play(); // Play the sound of opening the door
                    gameManager.UseKey(keyIndex); // Use the key
                }
                else
                {
                    lockedText.SetActive(true);
                    lockedSound.Play(); // Play the sound when the door is locked
                    StartCoroutine(DisableText(lockedText));
                }
            }
        }
    }

    private IEnumerator DisableText(GameObject text)
    {
        yield return new WaitForSeconds(2.0f);
        text.SetActive(false);
    }
}