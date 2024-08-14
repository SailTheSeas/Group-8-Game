using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviours : MonoBehaviour
{
    [SerializeField] private string zombieName;
    [SerializeField] private float zombieHealth;
    [SerializeField] private float zombieSpeed;
    [SerializeField] private float zombieDamage;

    private float currentSpeed;
    bool isDead, canMove;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        canMove = true;
        currentSpeed = zombieSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float Damage)
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
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
            transform.position = new Vector3(transform.position.x - currentSpeed, transform.position.y, transform.position.z);
    }

    IEnumerator SlowDuration()
    {
        yield return new WaitForSeconds(4f);
        currentSpeed = zombieSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {        
        if (other.transform.TryGetComponent<Projectile>(out Projectile projectile))
        {
            Damage(projectile.GetDamage());
            if (projectile.IsIce())
            {
                currentSpeed = zombieSpeed / 2;
                StartCoroutine(SlowDuration());
            }
            projectile.DestroyProjectile();
        }
        

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.TryGetComponent<PlantBehaviours>(out PlantBehaviours plant))
        {
            plant.Damage(zombieDamage);
            canMove = false;
            plant.Trigger(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canMove = true;        
    }
}
