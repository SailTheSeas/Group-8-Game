using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileType type;
    [SerializeField] private float projectileDamage;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float journeyTime;
    private Transform zombie, projectile, start;
    private Rigidbody2D RB;
    private float startTime;
    // Start is called before the first frame update
    private void Start()
    {
        switch (type)
        {
            case ProjectileType.sun:
                break;
            case ProjectileType.pea:
                RB = GetComponent<Rigidbody2D>();
                RB.velocity = Vector2.right * projectileSpeed;
                StartCoroutine(DestroySelf());
                break;
            case ProjectileType.icepea:
                RB = GetComponent<Rigidbody2D>();
                RB.velocity = Vector2.right * projectileSpeed;
                StartCoroutine(DestroySelf());
                break;
            case ProjectileType.cabbage:
                startTime = Time.time;
                //projectile = this.transform;
                break;
            default:
                break;
        }

    }

    private void FixedUpdate()
    {
        if (type == ProjectileType.cabbage)
        {
            //projectile = this.transform;
            if (zombie != null)
            {
                Vector3 center = (start.position + zombie.position) * 0.5f;

                center -= new Vector3(0, 1, 0);

                Vector3 riseRelCenter = start.position - center;
                Vector3 setRelCenter = zombie.position - center;

                float fracComplete = (Time.time - startTime) / journeyTime;

                transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete) + center;
                //transform.position += center;
            } else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void SetTargetTransform(Transform newZombie)
    {
        zombie = newZombie;
    }

    public void SetStartTransform(Transform newStart)
    {
        start = newStart;
    }

    public float GetDamage()
    {
        return projectileDamage;
    }

    public void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
    
    public bool IsIce()
    {
        return type == ProjectileType.icepea;
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(8f);
        Destroy(this.gameObject);
    }

    private void OnMouseDown()
    {
        if (type == ProjectileType.sun)
        {
            Destroy(this.gameObject);
        }
    }
}
