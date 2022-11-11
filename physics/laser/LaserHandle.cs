using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LaserHandle : MonoBehaviour
{

    public int pointLimit = 1;
    public int maxReflectionCount = 50;
    public float rayDistance = 100f;
    public LineRenderer lineRenderer;
    

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.ArrowHandleCap(0,this.transform.position + this.transform.right * 0.25f,this.transform.rotation, 0.5f,EventType.Repaint);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position,0.25f);

        CheckRays(this.transform.position + this.transform.right *0.75f, this.transform.right,maxReflectionCount);
    }

    void CheckRays(Vector3 position, Vector3 direction,int reflectionsRemaining)
    {

        if (reflectionsRemaining == 0)
        {
            return;
        }
        Vector3 startingPosition = position;
        Ray ray = new Ray(position,direction);

        RaycastHit hit;

        if (Physics.Raycast(ray,out hit, rayDistance))
        {
            direction = Vector3.Reflect(direction,hit.normal);
            position = hit.point;
        }
        else
        {
            position += direction * rayDistance;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startingPosition,position);

        CheckRays(position, direction, reflectionsRemaining-1);

    }
 
}
