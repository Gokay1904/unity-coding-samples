using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
   
    public GameObject selectedObject;

    public Camera mainCamera;
    public Vector3 mousePos;

    public bool isClicked;
    public bool isClickedDouble;

    public bool isDragging;

    public int mouseClick;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(DataManagement.dataManagement.currentLevelData.sceneName);
    }

    // Update is called once per frame
    void Update()
    {
      
        MouseInteractions();
    }

    void MouseInteractions()
    {
        if (Input.GetMouseButtonDown(0))
        {

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.forward, 1000f, LayerMask.GetMask("Interactable"));

            if (hit && hit.transform.tag == "Interactable")
            {
                if (selectedObject != null)
                {
                    if (selectedObject.transform.GetComponent<DefaultBlock>() != null)
                    {
                        selectedObject.GetComponent<IMoveable>().movingGuide.isGuideOn = false;
                    }
                        if (hit.transform.parent.gameObject == selectedObject)
                    {          
                        mouseClick++;
                    }

                    else
                    {

                        mouseClick = 1;
                        selectedObject = hit.transform.parent.gameObject; 

                        if (selectedObject.transform.GetComponent<DefaultBlock>() != null)
                        {         
                            selectedObject.GetComponent<DefaultBlock>().PlayInteractionSound();
                        }

                    }
                }

                if (selectedObject == null)
                {
                   
                    mouseClick = 1;
                    selectedObject = hit.transform.parent.gameObject; 

                    if (selectedObject.transform.GetComponent<DefaultBlock>() != null)
                    {
                        selectedObject.GetComponent<DefaultBlock>().PlayInteractionSound();
                    }
                }

            }

            else if (hit && hit.transform.tag != "Interactable")
            {
                if (selectedObject != null)
                {
                  
                }
                mouseClick = 0;
            }

            else if (!hit)
            {
                mouseClick = 0;
            }
        }

        if (Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);



            if (selectedObject != null && mouseClick == 1)
            {
                if (selectedObject.transform.GetComponent<IRotatable>() != null)
                {
                    selectedObject.transform.GetComponent<IRotatable>().RotateBlock(mousePos);
                }

             }

                if (selectedObject != null && mouseClick > 1)
                {
                    if (selectedObject.transform.GetComponent<IMoveable>() != null)
                    {
                        selectedObject.transform.GetComponent<IMoveable>().MoveBlock(mousePos);
                    }

                  /*  if (selectedObject.transform.GetComponent<Block>() != null) //MOUSE INTERACTION
                    {
                        selectedObject.transform.GetComponent<Block>().isMouseInteracted = true;
                    }*/
                }

            }

            if (Input.GetMouseButtonUp(0))
            {

                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.forward, 1000f);


                if (hit && hit.transform.tag == "Interactable")
                {
                    if (selectedObject != null)
                    {
                        if (hit.transform.parent == selectedObject.transform)
                        {
                            if (mouseClick > 1)
                            {
                            mouseClick=0;
                            }
                            //MouseClick degismesin
                        }

                        else if (hit.transform.parent != selectedObject)
                        {

                        if (selectedObject != null)
                        {
                            selectedObject.GetComponent<IMoveable>().movingGuide.isGuideOn = false;
                        }
                            mouseClick = 0;
                            selectedObject = null;

                        }
                    }

                    else if (selectedObject == null)
                    {
                        mouseClick = 0;
                    }

                }

                else if(!hit)
                {
                    if (selectedObject != null)
                    {
                   
                    }

                mouseClick = 0;
                selectedObject = null;
                }

            }

        //CHECKING INTERACTION

        if (selectedObject != null)
        {
            if (selectedObject.transform.GetComponent<Block>() != null)
            {
                selectedObject.transform.GetComponent<Block>().isMouseInteracted = isMouseInteracting(mouseClick);
            }

          
            if (mouseClick >= 2) 
            {
                if (selectedObject.transform.GetComponent<IMoveable>() != null)
                {
                    selectedObject.transform.GetComponent<IMoveable>().movingGuide.isGuideOn = true;
                }

            }

            if (mouseClick == 1)
            {
                selectedObject.transform.GetComponent<IMoveable>().movingGuide.isGuideOn = false;
            }

        }

        }

        
        void BlockInteractions()
    {

    }



        void MouseManagement()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.forward * 1000f);

                if (hit && hit.transform.tag == "Interactable")
                {
                    if (selectedObject == null)
                    {
                        selectedObject = hit.transform.parent.gameObject;
                        mouseClick = 1;
                    }

                    else
                    {
                        if (selectedObject == selectedObject.transform.parent.gameObject)
                        {
                            selectedObject = selectedObject.transform.parent.gameObject;
                            mouseClick++;
                        }
                        else
                        {
                            selectedObject = selectedObject.transform.parent.gameObject;
                            mouseClick = 1;
                        }
                    }
                }
                else if (hit && hit.transform.tag != "Interactable")
                {
                    selectedObject = null;
                    mouseClick = 0;
                }

                else if (!hit)
                {
                    selectedObject = null;
                    mouseClick = 0;
                }
            }

            if (Input.GetMouseButton(0))
            {



            }





        }

        bool isMouseInteracting(int _mouseClick)
        {
            if (_mouseClick > 0)
            {
                return true;
            }
            else return false;
            
   
        }

   


    }


    
