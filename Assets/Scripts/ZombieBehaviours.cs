using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviours : MonoBehaviour
{
    [SerializeField] private float zombieName;
    [SerializeField] private float zombieHealth;
    [SerializeField] private float zombieSpeed;
    [SerializeField] private float zombieDamage;

    bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamaged(float Damage)
    {
        zombieHealth -= Damage;
        if (zombieHealth <= 0)
        {
            Die();
            
        }
    }

    public void Die()
    {
        if (!isDead)
        {
            Debug.Log(zombieName + " Died");
            isDead = true;
        }
    }
}
