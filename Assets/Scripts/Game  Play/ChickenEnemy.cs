using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenEnemy : MonoBehaviour
{
    [Header("Refernces")]
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private GameObject destroyParticlePrefab;
    [SerializeField] private float destroyParticleLifeTime = 1f;
    [SerializeField] private EnemyDamageCollider damageCollider;
    [SerializeField] private AudioClip damageSound;
    [Header("Setings")]
    [SerializeField] private float movementSpeed = 5f;

    
    private Vector3 leftPointPosition;
    private Vector3 rightPointPosition;
    private bool isMovingRight = true;

    private void Start()
    {
        leftPointPosition = leftPoint.position;
        rightPointPosition = rightPoint.position;

        damageCollider.OnPlayerJumped += TakeDamag;
    }

    private void TakeDamag()
    {
        AudioSource.PlayClipAtPoint(damageSound, transform.position);

        var spawnerPrefab = Instantiate(destroyParticlePrefab, transform.position, Quaternion.identity);

        Destroy(spawnerPrefab, destroyParticleLifeTime);

        Destroy(gameObject);
    }

    private void Update()
    {
      float moveValue = movementSpeed * Time.deltaTime;
      if(isMovingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightPointPosition, moveValue);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            if (transform.position == rightPointPosition)
            {
                isMovingRight = false;
            }
        }  else
        {
            transform.position = Vector3.MoveTowards(transform.position, leftPointPosition, moveValue);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (transform.position == leftPointPosition)
            {
                isMovingRight = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.TakeDamage();
        }
    }
}
