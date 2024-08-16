using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviours : MonoBehaviour
{
    [SerializeField] private string zombieName;
    [SerializeField] private float zombieHealth;
    [SerializeField] private float zombieSpeed;
    [SerializeField] private float zombieDamage;
    [SerializeField] private ZombieType zombieType;
    [SerializeField] private float accessoryHealth;
    [SerializeField] private float jumpSpeed;

    [SerializeField] private GameObject accessory;

    [SerializeField] private int row;
    private float currentSpeed;
    bool isDead, canMove;
    bool isJumping;

    private Collider2D plant;

    private Vector3 jumpStart, jumpEnd;
    private float jumpStartTime, jumpLength;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        canMove = true;
        isJumping = false;
        currentSpeed = zombieSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (zombieType != ZombieType.Pole)
        {
            if (!plant)
                canMove = true;
        } else
        {

        }
    }

    private void FixedUpdate()
    {
        if (canMove && !isJumping)
            transform.position = new Vector3(transform.position.x - currentSpeed, transform.position.y, transform.position.z);
        else if (isJumping)
        {
            float distanceCovered = (Time.time - jumpStartTime) * jumpSpeed;
            float fractionOfJump = distanceCovered / jumpLength;
            transform.position = Vector3.Lerp(jumpStart, jumpEnd, fractionOfJump);
            if (fractionOfJump >= 0)
            {
                isJumping = false;
                zombieType = ZombieType.Basic;
                zombieSpeed = 0.003f;
                currentSpeed = zombieSpeed;
            }
        }
    }

    public void Damage(float damage)
    {
        zombieHealth -= damage;
        if (zombieHealth <= 0)
        {
            Die();           
        }
    }

    public void DamageAccessory(float damage)
    {
        accessoryHealth -= damage;
        if (zombieHealth <= 0)
        {
            zombieType = ZombieType.Basic;
            accessory.SetActive(false);
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

    public void SetRow(int newRow)
    {
        row = newRow;
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
            if (projectile.GetRow() == row)
            {
                switch (zombieType)
                {
                    case ZombieType.Basic:
                        Damage(projectile.GetDamage());
                        break;
                    case ZombieType.Cone:
                        DamageAccessory(projectile.GetDamage());
                        break;
                    case ZombieType.Bucket:
                        DamageAccessory(projectile.GetDamage());
                        break;
                    case ZombieType.Door:
                        DamageAccessory(projectile.GetDamage());
                        break;
                    case ZombieType.Pole:
                        Damage(projectile.GetDamage());
                        break;
                    case ZombieType.Rugby:
                        DamageAccessory(projectile.GetDamage());
                        break;
                    default:
                        break;
                }


                if (projectile.IsIce())
                {
                    currentSpeed = zombieSpeed / 2;
                    StartCoroutine(SlowDuration());
                }
                projectile.DestroyProjectile();
            }
        }
        if (zombieType == ZombieType.Pole)
        {
            if (other.transform.TryGetComponent<PlantBehaviours>(out PlantBehaviours plant))
            {
                jumpStart = this.transform.position;
                jumpEnd = plant.transform.position - new Vector3(1.2f, 0, 0);
                Debug.Log(jumpEnd);
                isJumping = true;
                jumpStartTime = Time.time;
                jumpLength = Vector3.Distance(jumpStart,jumpEnd);
            }
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.TryGetComponent<PlantBehaviours>(out PlantBehaviours plant))
        {
            if (plant.GetRow() == row)
            {
                if (zombieType != ZombieType.Pole)
                {
                    plant.Damage(zombieDamage);
                    canMove = false;
                    plant.Trigger(this);
                    this.plant = other;
                }
            }
        }
    }

    
}
