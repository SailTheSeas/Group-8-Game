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
    [SerializeField] private float postDeathHealth;

    [SerializeField] private float[] accessoryHealthGates;

    [SerializeField] private GameObject accessory;
    [SerializeField] private Sprite visualChange1, visualChange2;

    [SerializeField] private int row;
    [SerializeField] private LayerMask deadLayer;
    private float currentSpeed;
    bool isDead, canMove;
    bool isJumping;

    private ZombieSpawner zombieSpawner;
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
            if (fractionOfJump >= 1)
            {
                isJumping = false;
                canMove = true;
                zombieType = ZombieType.Basic;
                zombieSpeed = 0.006f;
                currentSpeed = zombieSpeed;
            }
        }
    }

    public ZombieType GetZombieType()
    {
        return zombieType;
    }

    public void SetZombieSpawner(ZombieSpawner newZombieSpawner)
    {
        zombieSpawner = newZombieSpawner;
    }

    public void Damage(float damage)
    {
        zombieHealth -= damage;
        if (zombieHealth <= 0)
        {
            if (!isDead)
            {
                isDead = true;
                //this.GetComponent<SpriteRenderer>().color = Color.red;
                StartCoroutine(DeathDelay());
            } else 
            {

                //gameObject.layer = LayerMask.NameToLayer(deadLayer.ToString());
                postDeathHealth -= damage;
                if (postDeathHealth <= 0)
                    this.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    public void DamageAccessory(float damage)
    {
        accessoryHealth -= damage;
        if (accessoryHealth <= accessoryHealthGates[0] && accessoryHealth > accessoryHealthGates[1])
        {
            accessory.GetComponent<SpriteRenderer>().sprite = visualChange1;
        } else if (accessoryHealth <= accessoryHealthGates[1] && accessoryHealth > 0)
        {
            accessory.GetComponent<SpriteRenderer>().sprite = visualChange2;

        } else if (accessoryHealth <= 0)
        {
            zombieType = ZombieType.Basic;
            accessory.SetActive(false);
        } 

    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(2.5f);
        Die();
    }

    public void Die()
    {
        isDead = true;
        zombieSpawner.KillZombie();
        Destroy(this.gameObject);
    }

    public void SetRow(int newRow)
    {
        row = newRow;

        this.GetComponent<SpriteRenderer>().sortingOrder = (5 - row);
        if (accessoryHealth > 0)
            accessory.GetComponent<SpriteRenderer>().sortingOrder = (5 - row);
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
                        if (projectile.GetProjectileType() == ProjectileType.cabbage)
                            Damage(projectile.GetDamage());
                        else
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


                if (projectile.IsIce() && zombieType != ZombieType.Door)
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
                isJumping = true;
                jumpStartTime = Time.time;
                jumpLength = Vector3.Distance(jumpStart,jumpEnd);
            }
        }

        if (other.transform.TryGetComponent<Lawnmower>(out Lawnmower lawnMower))
        {
            if (lawnMower.GetRow() == row)
            {
                lawnMower.ActivateMower();
                Die();
            }
        }

        if (other.transform.TryGetComponent<GameController>(out GameController gameController))
        {
            gameController.LoseGame();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.TryGetComponent<PlantBehaviours>(out PlantBehaviours plant))
        {
            plant.Trigger(this);
            if (plant.GetRow() == row)
            {
                if (zombieType != ZombieType.Pole)
                {
                    plant.Damage(zombieDamage);
                    canMove = false;
                    
                    this.plant = other;
                }
            }
        }
    }

    
}
