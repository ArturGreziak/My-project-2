using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet : MonoBehaviour
{
    [SerializeField] private float movementSpeed =1.0f;
    [SerializeField] private string grandTag = "Grand";

    private void Update()
    {
        

        float moveValue = movementSpeed * Time.deltaTime;
        transform.position += transform.right * moveValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(grandTag))
        {
            Destroy(gameObject);
        } else if(collision.TryGetComponent( out Player player))
        {
            player.TakeDamage();
            Destroy(gameObject);
        }
    }
}
