using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lawnmower : MonoBehaviour
{
    [SerializeField] private GameObject lawnMower;
    [SerializeField] private Vector3 lawnMowerEnd;
    [SerializeField] private float lawnMowerSpeed;
    [SerializeField] private int row;

    private Vector3 mowStart;
    private float mowStartTime;
    private bool isActive, isUsed;
    private float mowDistance;
    void Start()
    {
        isActive = false;
        isUsed = false;
        mowStart = this.transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isActive)
        {            
            
            mowDistance = Vector3.Distance(mowStart, lawnMowerEnd);
            float distanceCovered = (Time.time - mowStartTime) * lawnMowerSpeed;
            float fractionOfJump = distanceCovered / mowDistance;
            transform.position = Vector3.Lerp(mowStart, lawnMowerEnd, fractionOfJump);
            if (fractionOfJump >= 1)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void ActivateMower()
    {
        if (!isUsed)
        {
            mowStartTime = Time.time;
            isActive = true;
            isUsed = true;
        }
    }

    public int GetRow()
    {
        return row;
    }
}
