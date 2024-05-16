using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRidygbody;

    public event Action OnLose;
    public void TakeDamage()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        OnLose?.Invoke();
        gameObject.SetActive(false);
    }    

    public void AddForce(float force)
    {
        playerRidygbody.velocity = new Vector2(playerRidygbody.velocity.x, 0);
        playerRidygbody.AddForce(new Vector2 (0, force), ForceMode2D.Impulse);
    }
}
