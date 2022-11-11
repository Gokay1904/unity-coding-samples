using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public int rayCount;

    private Transform goTransform;

    private Ray ray;

    private LineRenderer lineRenderer;

    private Vector3 inDirection;

    //the number of reflections  
    public int nReflections = 2;

    //the number of points at the line renderer  
    private int nPoints;


    // Start is called before the first frame update
    void Start()
    {
        //get the attached Transform component  
        goTransform = this.GetComponent<Transform>();
        //get the attached LineRenderer component  
        lineRenderer = this.GetComponent<LineRenderer>();

      
    
    }

    void Update()
    {
        Debug.Log(DataManagement.playerData);
        if (Input.GetKeyDown(KeyCode.Space))
        {
          
            EnableLaser();
        }

        CheckRays();

        if (Input.GetKeyDown(KeyCode.X))
        {
            DisableLaser();
        }
    }
    void EnableLaser()
    {
        lineRenderer.enabled = true;
    }

    // Update is called once per frame   
     void CheckRays()
     {
     
         nReflections = Mathf.Clamp(nReflections, 1, nReflections);
  
         ray = new Ray(goTransform.position, goTransform.right);

         Debug.DrawRay(goTransform.position, goTransform.right * 100, Color.magenta);

         nPoints = nReflections;

         lineRenderer.SetVertexCount(nPoints);

         lineRenderer.SetPosition(0, goTransform.position);

         for (int i = 0; i <= nReflections; i++)
         {
             if (i == 0)
             {

                if (Physics2D.Raycast(ray.origin, ray.direction, 100))
                {
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100);

                    inDirection = Vector3.Reflect(ray.direction, hit.normal);

                    ray = new Ray(hit.point, inDirection);

                    Debug.DrawRay(hit.point, hit.normal * 3, Color.blue);

                    Debug.DrawRay(hit.point, inDirection * 100, Color.magenta);


                    Debug.Log("Object name: " + hit.transform.name);
               
 
                     if (nReflections == 1)
                     {  
                         lineRenderer.SetVertexCount(++nPoints);
                     }

                     lineRenderer.SetPosition(i + 1, hit.point);
                 }

             }

             else 
             {
            
                 if (Physics2D.Raycast(ray.origin, ray.direction, 100))
                 {

                     RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100);

                     inDirection = Vector3.Reflect(inDirection, hit.normal);
                   
                     ray = new Ray(hit.point, inDirection);

                     Debug.DrawRay(hit.point, hit.normal * 3, Color.blue);

                     Debug.DrawRay(hit.point, inDirection * 100, Color.magenta);

                     Debug.Log("Object name: " + hit.transform.name);

                     lineRenderer.SetVertexCount(++nPoints);

                     lineRenderer.SetPosition(i + 1, hit.point);

                 }

             }
         }
     }


  
    void DisableLaser()
    {
        lineRenderer.enabled = false;
    }

    void CastRay()
    {
        nReflections = Mathf.Clamp(nReflections, 1, nReflections);

        ray = new Ray(goTransform.position, goTransform.forward);

        Debug.DrawRay(goTransform.position, goTransform.forward * 100, Color.magenta);

        nPoints = nReflections;

        lineRenderer.SetVertexCount(nPoints);

        lineRenderer.SetPosition(0, goTransform.position);

        RaycastHit2D hit;

        for (int i = 0; i <= nReflections; i++)
        {
            if (i == 0) //Eğer hiç yansımadıysa 
            {
            //Bir yere çarptıysa
            if (Physics2D.Raycast(ray.origin,ray.direction,100f))
            {
                    //Çarpma noktasını belirt.
                    hit = Physics2D.Raycast(ray.origin, ray.direction, 100f);
                    //Yansıma yönünü belirt.
                    inDirection = Vector3.Reflect(ray.direction,hit.normal);

                    //Yansımanın olduğu ışını çiz.
                    ray = new Ray(hit.point,inDirection);

                    Debug.DrawRay(hit.point, hit.normal*3f, Color.blue);
                    Debug.DrawRay(hit.point, inDirection*100f, Color.magenta);

                    if (nReflections == 1)
                    {
                        lineRenderer.positionCount += 1;
                    }

                    lineRenderer.SetPosition(i+1,hit.point);
                }
    
                else
                {
                    if (Physics2D.Raycast(ray.origin,ray.direction,100f))
                    {
                        hit = Physics2D.Raycast(ray.origin, ray.direction, 100f);

                        inDirection = Vector3.Reflect(ray.direction, hit.normal);

            
                    }

                }


            }



        }
       



    }


}
