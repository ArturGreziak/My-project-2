using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameObject picupEffectPrefab;
    [SerializeField] private float picupEffeectLiftime = 0.3f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent (out PlayerInventory playerInventory))
        {
            playerInventory.AddFruit();

            var spawnerPrefab = Instantiate(picupEffectPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Destroy(spawnerPrefab, picupEffeectLiftime);

            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            Destroy(gameObject);
            
        }
    }
}
