using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBehaviours : MonoBehaviour
{
    [SerializeField] private float plantHealth;
    [SerializeField] private float coolDown;
    [SerializeField] private float workRate;
    [SerializeField] private float damage;
    [SerializeField] private float startupTime;
    [SerializeField] private float sunCost;
    [SerializeField] private PlantType plantType;
    [SerializeField] private GameObject plantProjectile;
    [SerializeField] private LayerMask zombieLayer;
    [SerializeField] private Vector2 mapEdge;

    [SerializeField] private int row;

    private bool canAttack, isDead;
    private RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        switch (plantType)
        {
            case PlantType.sunfower:
                InvokeRepeating("ProduceSun", workRate, workRate);
                break;
            case PlantType.peashooter:
                canAttack = false;
                StartCoroutine(ShootDelay());
                //InvokeRepeating("ShootNormalPea", workRate, workRate);
                break;
            case PlantType.wallnut:
                
                //Nothing
                break;
            case PlantType.cabbagepult:
                canAttack = false;
                StartCoroutine(ShootDelay());
                // InvokeRepeating("FlingCabage", workRate, workRate);
                break;
            case PlantType.potatomine:
                canAttack = false;
                InvokeRepeating("Arm", startupTime, workRate);
                break;
            case PlantType.chomper:
                canAttack = true;
                //Nothing
                break;
            case PlantType.snowpea:
                canAttack = false;
                StartCoroutine(ShootDelay());
                //InvokeRepeating("ShootIcePea", workRate, workRate);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        hit = Physics2D.Raycast(transform.position, mapEdge,mapEdge.x - transform.position.x, zombieLayer);

        if (hit.collider != null)
        {
            if (canAttack)
            {
                switch (plantType)
                {
                    case PlantType.peashooter:
                        ShootPea();
                        break;
                    case PlantType.cabbagepult:                        
                        FlingCabage(hit.transform);
                        break;
                    case PlantType.snowpea:
                        ShootPea();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    //Setters
    public void SetRow(int newRow)
    {
        row = newRow;
    }
    //Getters
    public float GetCooldown()
    {
        return coolDown;
    }

    public float GetSunCost()
    {
        return sunCost;
    }

    public int GetRow()
    {
        return row;
    }

    public void Damage(float damage)
    {
        plantHealth -= damage;
        if (plantHealth <= 0)
        {
           Die();
        }
    }

    private void Die()
    {
        if (!isDead)
        {
            isDead = true;
            Destroy(this.gameObject);
        }
    }

    private void ProduceSun()
    {
        Instantiate(plantProjectile, this.transform.position, Quaternion.identity);
        //Debug.Log("Made Sun");
    }

    private void ShootPea()
    {
        GameObject pea =  Instantiate(plantProjectile, this.transform.position, Quaternion.identity);

        pea.GetComponent<Projectile>().SetDamage(damage);
        pea.GetComponent<Projectile>().SetRow(row);
        canAttack = false;
        //Debug.Log("Fired");
        StartCoroutine(ShootDelay());
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(workRate);
        canAttack = true;
    }

    private void FlingCabage(Transform target)
    {
        GameObject cabbage = Instantiate(plantProjectile);
        cabbage.GetComponent<Projectile>().SetTargetTransform(target);
        cabbage.GetComponent<Projectile>().SetStartTransform(this.transform);
        cabbage.GetComponent<Projectile>().SetDamage(damage);
        cabbage.GetComponent<Projectile>().SetRow(row);
        canAttack = false;
        //Debug.Log("Thrown");
        StartCoroutine(ShootDelay());
 
    }

    private void Chomp(ZombieBehaviours other)
    {
        //Debug.Log("Eaten");
        other.Die();
        canAttack = false;
        InvokeRepeating("ChompCooldown", startupTime, startupTime);
    }

    private void ChompCooldown()
    {
        CancelInvoke("ChompCooldown");
        //Debug.Log("Chomp Ready");
        canAttack = true;
    }

    private void Arm()
    {
        //Debug.Log("Armed");
        canAttack = true;
        plantHealth = 9999999;
        CancelInvoke("Arm");
    }

    private void Boom(ZombieBehaviours other)
    {
        //Debug.Log("Boom");
        if (other.GetZombieType() != ZombieType.Pole)
        {
            other.Die();
            Destroy(this.gameObject);
        }
    }

    public void Trigger(ZombieBehaviours zombie)
    {
        switch (plantType)
        {
            case PlantType.potatomine:
                if (canAttack)
                    Boom(zombie);
                break;
            case PlantType.chomper:
                if (canAttack)
                    Chomp(zombie);
                break;
            default:
                break;
        }
    }

    /*private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.TryGetComponent<ZombieBehaviours>(out ZombieBehaviours zombie))
        {
            switch (plantType)
            {
                case PlantType.sunfower:
                    break;
                case PlantType.peashooter:
                    break;
                case PlantType.wallnut:
                    break;
                case PlantType.cabbagepult:
                    break;
                case PlantType.potatomine:
                    if (canAttack)
                        Boom(zombie);
                    break;
                case PlantType.chomper:
                    if (canAttack)
                        Chomp(zombie);
                    break;
                case PlantType.snowpea:
                    break;
                default:
                    break;
            }
        }
    }*/


}
