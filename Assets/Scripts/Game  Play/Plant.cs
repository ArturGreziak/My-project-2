using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [Header("Refernces")]
    [SerializeField] private GameObject destroyParticlePrefab;
    [SerializeField] private float destroyParticleLifeTime = 1f;
    [SerializeField] private EnemyDamageCollider damageCollider;
    [SerializeField] private AudioClip damageSound;

    [Header("Shooting")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject pllantBuletPrefab;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private float shootDelay = 1f;

    [Header("Animations")]
    [SerializeField] private Animator animator;
    [SerializeField] private string shootTrigger;

    private float shootTimer = 1.0f;

    private void Start()
    {
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
        shootTimer += Time.deltaTime;
        if(shootTimer >= shootDelay)
        {
            shootTimer -= shootDelay;
            Shoot();
        }
    }

    private void Shoot()
    {
        animator.SetTrigger(shootTrigger);
        var spawnerBulet = Instantiate(pllantBuletPrefab, transform.position, Quaternion.identity);
        spawnerBulet.transform.right = -transform.right;

        AudioSource.PlayClipAtPoint(shootSound, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.TakeDamage();
        }
    }
}
