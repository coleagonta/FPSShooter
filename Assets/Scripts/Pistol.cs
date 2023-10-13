using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Pistol : MonoBehaviour
{
    public Camera cam;
    public float range = 50;
    public float impactForce = 1000f;
    public float fireRate = 1f;
    public float nextTimeToFire = 0f;
    public int zombieDamage = 50;
    public ParticleSystem ShootEffect;
    public ParticleSystem HitEffect;
    public AudioSource ShootAudio;
    public GameManager gameManager;

    private int _counter;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire)
        {
            Shoot();
            nextTimeToFire = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        PlayEffects();

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out var hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            ZombieMove zombieMove = hit.transform.GetComponent<ZombieMove>();

            ApplyImpactForce(hit);
            ApplyZombieDamage(zombieMove);

            CreateHitParticleEffect(hit);
        }
    }

    void PlayEffects()
    {
        ShootEffect.Play();
        ShootAudio.Play();
    }

    void ApplyImpactForce(RaycastHit hit)
    {
        if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * impactForce);
        }
    }

    void ApplyZombieDamage(ZombieMove zombieMove)
    {
        if (zombieMove != null)
        {
            bool deadZombie = zombieMove.TakeDamage(zombieDamage);
            if (deadZombie)
            {
                _counter++;
            }
            HandleWin();
        }
    }

    void CreateHitParticleEffect(RaycastHit hit)
    {
        ParticleSystem CreateHit = Instantiate(HitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(CreateHit.gameObject, 0.2f);
    }

    private void HandleWin()
    {
        if (_counter == 5)
        {
            Cursor.lockState = CursorLockMode.Confined;
            gameManager.ChangeGameState(GameManager.GameState.Win);
        }
    }
}