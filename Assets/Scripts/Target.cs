using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Target : MonoBehaviour
{
    public float health = 50f;

    public void TakeDamage(float amount)
    {
        health = health - amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}