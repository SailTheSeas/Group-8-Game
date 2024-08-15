using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunPickup : MonoBehaviour
{
    private SunController sunController;
    public Vector2 FinalPosition;
    // Start is called before the first frame update
    void Start()
    {
        sunController = GameObject.Find("Canvas").GetComponent<SunController>();
        FinalPosition = new Vector2(gameObject.transform.position.x, Random.Range(-3.25f, 2.5f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, FinalPosition, 0.0009f);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            sunController.PickupSun();
            Destroy(gameObject);
        }
    }
}
