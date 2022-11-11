using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class RaycastReflection : MonoBehaviour
{
    public int reflections;
    public float maxLength;

    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit2D hit;
    private Vector3 direction;



    private void Awake()
    {
     
    }
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        ReflectionFor2D();
    }
    void ReflectionFor2D()
    {
        ray = new Ray(transform.position, transform.right);

        lineRenderer.positionCount = 1;

        lineRenderer.SetPosition(0, transform.position);

        float remainingLength = maxLength;

        for (int i = 0; i < reflections; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, remainingLength, LayerMask.GetMask("Reflective"));
            if (hit)
            {
                if (hit.collider.tag == "Reflectable") 
                {

                lineRenderer.positionCount += 1;

                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                remainingLength -= Vector3.Distance(ray.origin, hit.point);

                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                   
                    if (hit.collider.GetComponent<DefaultBlock>() != null)
                    {
                        if(hit.collider.GetComponent<DefaultBlock>().isRayInteracted == false) 
                        {
                        hit.collider.GetComponent<DefaultBlock>().isRayInteracted = true;
                        DefaultBlock hittedBlock = hit.collider.GetComponent<DefaultBlock>();

                        GameManagement.gameManagement.activatedDefaultBlocks.Add(hittedBlock);          
                        }     
                    } 
                }

                if (hit.collider.tag != "Reflectable")
                {
                   
                    break;
                }
 
            }
             
            else
            {        
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }

        }
    }

}


 /*  void ReflectionFor3D()
    {
        ray = new Ray(transform.position, transform.right);

        lineRenderer.positionCount = 1;

        lineRenderer.SetPosition(0, transform.position);

        float remainingLength = maxLength;

        for (int i = 0; i < reflections; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
            {
                lineRenderer.positionCount += 1;

                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                remainingLength -= Vector3.Distance(ray.origin, hit.point);

                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));

                if (hit.collider.tag != "Mirror")
                {
                    break;
                }

            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }

        }
    }
}*/
