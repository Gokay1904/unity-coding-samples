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












    /* IEnumerator CalculateCollisions()
     {



             for(int i=0;  i <= lineRenderer.positionCount; i++) //Çakışmalar bitine kadar tarayacak. Eğer son indexe gelindiyse tekrar en baştan tarayacak.
             {

                 //Eğer 0. nokta varsa ve 1. nokta varsa
                 if (laserLineRenderer.GetPosition(i) != null && laserLineRenderer.GetPosition(i+1) != null)
                 {
                     //0.  nokta ile 1. nokta arasında doğru çiz.
                     Vector2 dir = laserLineRenderer.GetPosition(i + 1) - laserLineRenderer.GetPosition(i);
                     //Bu doğru üzerine çakışma belirle.
                     RaycastHit2D hit = Physics2D.Raycast(laserLineRenderer.GetPosition(i),dir.normalized,dir.magnitude);

                     if (hit)
                     {
                         if (laserLineRenderer.GetPosition(i+2) == null) 
                         {
                             //Eğer çakışma varsa ve 3. nokta belirsizse önce yeni bir nokta oluştur.
                             laserLineRenderer.positionCount++;
                             //Yansıma açısını hesapla.
                             Vector2 reflection = Vector2.Reflect(laserLineRenderer.GetPosition(i+1), hit.normal);
                             laserLineRenderer.SetPosition(i+2,reflection); 
                         }

                         else if (laserLineRenderer.GetPosition(i + 2) != null)
                         {  //Eğer çakışma varsa ve 3. nokta belirliyse noktanın yeni konumunu güncelle.
                             Vector2 reflection = Vector2.Reflect(laserLineRenderer.GetPosition(i+1), hit.normal);
                             laserLineRenderer.SetPosition(i + 2, reflection);       
                         }

                         i++; // Bir sonrakine geç.
                     }



                     else if (!hit)
                     {
                     //Eğer çarpışma yoksa son değen noktadan sonraki bütün noktaları sil. 
                     //Yani line renderer büyüklüğünü son noktaya eşitle
                     if (laserLineRenderer.GetPosition(i+2) != null) 
                     { 
                     laserLineRenderer.positionCount = i + 1;
                     }


                     i = 0;

                     }

                     yield return null;

                 }





         }


     }*/




