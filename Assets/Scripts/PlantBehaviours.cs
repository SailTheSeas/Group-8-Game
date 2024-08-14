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
    [SerializeField] private PlantType plantType;
    [SerializeField] private GameObject plantProjectile;
    private bool canAttack;
    // Start is called before the first frame update
    void Start()
    {
        switch (plantType)
        {
            case PlantType.sunfower:
                InvokeRepeating("ProduceSun", workRate, workRate);
                break;
            case PlantType.peashooter:
                InvokeRepeating("ShootNormalPea", workRate, workRate);
                break;
            case PlantType.wallnut:
                //Nothing
                break;
            case PlantType.cabbagepult:
                InvokeRepeating("FlingCabage", workRate, workRate);
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
                InvokeRepeating("ShootIcePea", workRate, workRate);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ProduceSun()
    {
        Instantiate(plantProjectile, this.transform);
        Debug.Log("Made Sun");
    }

    private void ShootNormalPea()
    {
        Instantiate(plantProjectile, this.transform);
        Debug.Log("Fired");
    }

    private void ShootIcePea()
    {
        Debug.Log("Fired, Slowed");
    }

    private void FlingCabage()
    {
        Debug.Log("Thrown");
    }

    private void Chomp(ZombieBehaviours other)
    {
        Debug.Log("Eaten");
        other.Die();
        canAttack = false;
        InvokeRepeating("ChompCooldown", startupTime, startupTime);
    }

    private void ChompCooldown()
    {
        CancelInvoke("ChompCooldown");
        Debug.Log("Chomp Ready");
        canAttack = true;
    }

    private void Arm()
    {
        Debug.Log("Armed");
        canAttack = true;
        CancelInvoke("Arm");
    }

    private void Boom(ZombieBehaviours other)
    {
        Debug.Log("Boom");
        other.Die();
        Destroy(this);
    }

    private void OnTriggerStay2D(Collider2D other)
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
    }


}
