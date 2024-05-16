using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{


    public event Action OnLanding;
    [SerializeField] private string groundTag;

    private int groundCounter = 0;

    public bool IsGrounded
    {
        get
        {
            return groundCounter > 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(groundTag))
        {
            if(groundCounter == 0)
            {
                OnLanding?.Invoke();
            }
            ++groundCounter;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(groundTag))
        {
            --groundCounter;
        }
    }

}
