using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
    public bool isRayTouched;
    public Vector2 currentPos;
    public Collider2D interactableArea;

    public Transform block;

    public bool isRotatable;
    public bool isMoveable;

    public bool isRaySignal = false;
    public bool isMouseInteracted = false;
    public bool isRayInteracted = false;

    public virtual void OnMouseInteraction(bool _isMouseInteracted) { }
    public virtual void OnRayInteraction(bool _isRayInteracted) { }

   


}
