using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject key;
    public AudioSource pickup;
    public GameManager gameManager;
    public int keyIndex;
    public bool interactable;
    public float rotationSpeed = 50f;

    void Update()
    {
        
        key.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        if (interactable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                interactable = false; 

                
                if (pickup != null)
                {
                   
                    pickup.Play();
                }

                key.SetActive(false);
                gameManager.CollectKey(keyIndex);
            }
        }
    }
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
}