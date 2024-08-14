using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileType type;

    private Rigidbody2D RB;
    // Start is called before the first frame update
    void Start()
    {
        switch (type)
        {
            case ProjectileType.sun:
                break;
            case ProjectileType.pea:
                RB = GetComponent<Rigidbody2D>();
                RB.velocity = Vector2.right;
                break;
            case ProjectileType.icepea:
                break;
            case ProjectileType.cabbage:
                break;
            default:
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
