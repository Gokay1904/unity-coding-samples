using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBlock : Block, IMoveable, IRotatable, ITimer
{

    [Header("Timer")]
    [Space(5)]
    [SerializeField]
    private TimerManagement _timerManagement;
    public TimerManagement timerManagement { get { return _timerManagement; } set { timerManagement = _timerManagement; } }
    public TimerUI timerUI { get { return timerManagement.TimerUI; } set{ timerUI = timerManagement.TimerUI; }}
    public bool isTimeUp { get { return !timerManagement.isCountable; } set { isTimeUp = !timerManagement.isCountable; } }
 

    [Header("Movement")]
    [Space(5)]

    [SerializeField]
    private bool _isHorizontal;
    [SerializeField]
    private bool _isVertical;
    public bool isHorizontal { get { return _isHorizontal; } set {_isHorizontal = value;  } }
    public bool isVertical { get  { return _isVertical; } set { _isVertical = value; } }

    public Vector2 centerPos { get; set; }
    public float movingLerpSpeed{ get {return 5f; } set { movingLerpSpeed = 1000f; } }

    [SerializeField]
    private float _leftDistance;
    [SerializeField]
    private float _rightDistance;
    [SerializeField]
    private float _upDistance;
    [SerializeField]
    private float _downDistance;
  
    public float leftDistance { get {return  _leftDistance; } set { _leftDistance = value; } }
    public float rightDistance { get {return _rightDistance;  } set { _rightDistance = value; } }
    public float upDistance { get {return _upDistance; } set { _upDistance = value;} }
    public float downDistance { get {return _downDistance; } set { _downDistance = value; } }

    [SerializeField]
    private MovingGuide _movingGuide;

    public MovingGuide movingGuide { get { return _movingGuide; } set { _movingGuide = value;} }

 

    // Start is called before the first frame update
    void Awake()
    {

        block = transform;
        block.position = transform.position;
        
        centerPos = transform.position;

        currentPos = block.transform.position;

        interactableArea = gameObject.GetComponent<CircleCollider2D>();

        InitializeTimer();

    }
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnMouseInteraction(isMouseInteracted);
        OnTimeIsUp(isTimeUp);
        HandleMovingGuide();

        if (isRaySignal == true)
        {

        }

    }

    public void RotateBlock(Vector3 targetPos)
    {
        if (isRotatable) { 
        currentPos = block.transform.position;

        float degree = Mathf.Atan2(targetPos.y - currentPos.y, targetPos.x - currentPos.x) * Mathf.Rad2Deg;

        block.transform.rotation = Quaternion.Euler(0, 0, degree);
        }
    }

    public void MoveBlock(Vector2 targetPos)
    {
        if (isMoveable) {

           currentPos = block.transform.position;

            if (isHorizontal)
            {
                if (targetPos.x >= centerPos.x)
                {
                    if (rightDistance>= (targetPos.x - centerPos.x))
                    {          
                        currentPos.x = Mathf.Lerp(currentPos.x,targetPos.x,movingLerpSpeed * Time.deltaTime);
                        block.transform.position = currentPos;
                    }
                }

                else if (targetPos.x < centerPos.x)
                {
                    if (targetPos.x - centerPos.x >=  -leftDistance)
                    {
                        currentPos.x = Mathf.Lerp(currentPos.x, targetPos.x, movingLerpSpeed * Time.deltaTime);
                        block.transform.position = currentPos;
                    }
                }
            }

            if (isVertical)
            {

                if (targetPos.y > centerPos.y)
                {

                    if (targetPos.y - centerPos.y <= upDistance)
                    {

                        currentPos.y = Mathf.Lerp(currentPos.y, targetPos.y, movingLerpSpeed * Time.deltaTime);
                        block.transform.position = currentPos;
                    }

                }

                else if (targetPos.y <= centerPos.y)
                {
                    if (targetPos.y - centerPos.y >= -downDistance)
                    {
                        currentPos.y = targetPos.y; //Mathf.Lerp(currentPos.y, targetPos.y, movingLerpSpeed * Time.deltaTime);
                        block.transform.position = currentPos;
                    }
                }
            }


        }

    }

    public override void OnMouseInteraction(bool _isMouseInteracted)
     {
        if (_isMouseInteracted == true)
        {
            timerManagement.decreaseTime();
        }
        else if (_isMouseInteracted == false)
        {
            movingGuide.isGuideOn = false;
        }
    }

    public void InitializeTimer()
    {
        timerManagement.Initialize();
    }

    public void OnTimeIsUp(bool _isTimeUp)
    {

        if (timerManagement.isTimeUp == true)
        {
            isMoveable = false;
            isRotatable = false;
            isMouseInteracted = false;
        }
       
    }

    public void HandleMovingGuide()
    {
        if (movingGuide != null)
        {
            movingGuide.isHorizontal = this._isHorizontal;
            movingGuide.isVertical = this._isVertical;
            movingGuide.startPos = this.centerPos;
            movingGuide.horizontalDistanceL = _leftDistance;
            movingGuide.horizontalDistanceR = _rightDistance;
            movingGuide.verticalDistanceD = _downDistance;
            movingGuide.verticalDistanceU = _upDistance;
        }
    }
    public void PlayInteractionSound()
    {
      

        if(timerManagement.isTimeUp)
        { 
            SoundManagement.soundManagement.GetSound("StoneClick").musicSource.pitch = 1.5f;
        }
        else
        {
            SoundManagement.soundManagement.GetSound("StoneClick").musicSource.pitch = 1f;
        }
       
   
        SoundManagement.soundManagement.PlaySound("StoneClick");
    }


}



