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
    [SerializeField] private Vector3 mapEdge;

    [SerializeField] private Sprite visualChange1, visualChange2;
    [SerializeField] private int row;

    private Vector2Int positionInGrid;
    private bool isActive;
    private bool canAttack, isDead;
    private RaycastHit2D hit;

    [SerializeField] private Renderer plantRenderer;
    [SerializeField] private Material plantMaterial;
    [SerializeField] private Color originalColor;
    [SerializeField] private float brightnessChangeDuration;
    // Start is called before the first frame update
    void Start()
    {
        
        isActive = false;
        this.GetComponent<Collider2D>().enabled = false;
        plantRenderer = GetComponent<Renderer>();
        brightnessChangeDuration = workRate;
        if (plantRenderer != null)
        {
            plantMaterial = plantRenderer.material;
            originalColor = plantMaterial.color; 
        }
    }

    public void EnablePlant()
    {
        isDead = false;
        isActive = true;
        
        this.GetComponent<Collider2D>().enabled = true;
        switch (plantType)
        {
            case PlantType.sunfower:
                InvokeRepeating("ProduceSun", workRate, workRate);
                StartCoroutine(ChangeBrightness());
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
        if (plantType == PlantType.chomper)
            hit = Physics2D.Raycast(transform.position, mapEdge,2.5f, zombieLayer);
        else
            hit = Physics2D.Raycast(transform.position, mapEdge, mapEdge.x - transform.position.x, zombieLayer);
        
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
                    case PlantType.chomper:
                        Chomp(hit.transform.GetComponent<ZombieBehaviours>());
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
        this.GetComponent<SpriteRenderer>().sortingOrder = (5-row);
    }

    public void SetGridPosition(Vector2Int newPosition)
    {
        positionInGrid = newPosition;
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
        if (plantType == PlantType.wallnut)
        {
            if (plantHealth <= 2667 && plantHealth > 1333)
                this.GetComponent<SpriteRenderer>().sprite = visualChange1;
            else if (plantHealth <= 1333 )
                this.GetComponent<SpriteRenderer>().sprite = visualChange2;
        }
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
            FindObjectOfType<GridManager>().SetCellOccupied(positionInGrid, false);
            Destroy(this.gameObject);
        }
    }

    private void ProduceSun()
    {
        GameObject sun = Instantiate(plantProjectile, this.transform.position, Quaternion.identity);
        sun.GetComponent<Projectile>().SetRow(row);
        //Debug.Log("Made Sun");
    }


    private IEnumerator ChangeBrightness()
    {
        if (plantMaterial == null)
        {
            yield break; // Exit if the material is not set
        }

        while (true)
        {
            float elapsedTime = 0f;
            while (elapsedTime < workRate)
            {
                elapsedTime += Time.deltaTime;
                float lerpFactor = Mathf.InverseLerp(workRate - brightnessChangeDuration, workRate, elapsedTime);
                float brightness = Mathf.Lerp(0.5f, 1f, lerpFactor); 
                plantMaterial.color = originalColor * brightness; 
                yield return null;
            }
            yield return null;
        }
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
        if (isActive)
        {
            //Debug.Log("Eaten");
            other.Die();
            canAttack = false;
            this.GetComponent<SpriteRenderer>().sprite = visualChange1;
            InvokeRepeating("ChompCooldown", startupTime, startupTime);
        }
    }

    private void ChompCooldown()
    {
        CancelInvoke("ChompCooldown");
        this.GetComponent<SpriteRenderer>().sprite = visualChange2;
        //Debug.Log("Chomp Ready");
        canAttack = true;
    }

    private void Arm()
    {

        //Debug.Log("Armed");
        this.GetComponent<SpriteRenderer>().sprite = visualChange1;
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
            Die();
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
            /*case PlantType.chomper:
                if (canAttack)
                    Chomp(zombie);
                break;*/
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
