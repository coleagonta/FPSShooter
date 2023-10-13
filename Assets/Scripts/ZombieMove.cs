using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class ZombieMove : MonoBehaviour
{
    public int zombieHealth = 100;
    public float zombieSpeed = 1;
    public List<Rigidbody> getRigidbodies = new List<Rigidbody>();

    public GameObject Player;
    private Animator ZombieAnimator;
    private AnimatorStateInfo ZombieStateInfo;

    private void Start()
    {
        InitializeZombie();
    }

    void Update()
    {
        ZombieController();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ZombieAnimator.SetBool("Attack", true);
            other.gameObject.GetComponent<PlayerHealth>().TakePlayerDamage(50);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ZombieAnimator.SetBool("Attack", false);
        }
    }

    void InitializeZombie()
    {
        ZombieAnimator = gameObject.GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");

        SetRigidBodyIsKinematic(true);
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }

    void SetRigidBodyIsKinematic(bool isKinematic)
    {
        ZombieAnimator.enabled = isKinematic;

        foreach (Rigidbody rb in getRigidbodies)
        {
            rb.isKinematic = isKinematic;
        }
    }

    void ZombieController()
    {
        ZombieStateInfo = ZombieAnimator.GetCurrentAnimatorStateInfo(0);

        if (zombieHealth > 0)
        {
            if (ZombieStateInfo.IsName("Zombie Attack") || ZombieStateInfo.IsName("Zombie Reaction Hit"))
            {
                zombieSpeed = 0;
            }
            else
            {
                zombieSpeed = 1;
                gameObject.transform.LookAt(Player.transform.position);
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Player.transform.position, zombieSpeed * Time.deltaTime);
            }
        }
    }

    public bool TakeDamage(int damage)
    {
        zombieHealth -= damage;
        ZombieAnimator.SetTrigger("Hit");

        if (zombieHealth <= 0)
        {
            SetRigidBodyIsKinematic(false);
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            return true;
        }

        return false;
    }
}