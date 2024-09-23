using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepSounds : MonoBehaviour
{
    public AudioSource footstepswalk, footstepssprint;
    public bool sprinting;
    public Player playerScript; 

    void Update()
    {
        // if playerDead
        if (playerScript != null && playerScript.isDead)
        {
            footstepswalk.enabled = false;
            footstepssprint.enabled = false;
            return; 
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                sprinting = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                sprinting = false;
            }

            if (sprinting)
            {
                footstepswalk.enabled = false;
                footstepssprint.enabled = true;
            }
            else
            {
                footstepswalk.enabled = true;
                footstepssprint.enabled = false;
            }
        }
        else
        {
            footstepswalk.enabled = false;
            footstepssprint.enabled = false;
            sprinting = false;
        }
    }
    public void StopFootstepSounds()
    {
        footstepswalk.Stop(); 
        footstepssprint.Stop(); 
        footstepswalk.enabled = false;
        footstepssprint.enabled = false; 
    }
}