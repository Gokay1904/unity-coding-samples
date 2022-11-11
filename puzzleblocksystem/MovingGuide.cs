using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingGuide : MonoBehaviour
{

    [SerializeField]
    private LineRenderer verticalGuideLine;
    [SerializeField]
    Material verticalLineMaterial;

    [SerializeField]
    private LineRenderer horizontalGuideLine;

    [SerializeField]
    Material horizontalLineMaterial;

    private bool _isHorizontal;
    public bool isHorizontal{ get { return _isHorizontal; } set { _isHorizontal = value; } }

    private bool _isVertical;
    public bool isVertical { get { return isVertical; } set { _isVertical = value; } }

    [SerializeField]
    private bool _isGuideOn;

    public bool isGuideOn { get { return _isGuideOn;} set { _isGuideOn = value; } }

    private float _horizontalDistanceR;
    public float horizontalDistanceR { get { return _horizontalDistanceR; } set { _horizontalDistanceR = value;} }
    
    private float _horizontalDistanceL;
    public float horizontalDistanceL { get { return _horizontalDistanceL; } set { _horizontalDistanceL = value; } }

    private float _verticalDistanceU;
    public float verticalDistanceU { get { return _verticalDistanceU; } set { _verticalDistanceU = value;}}
    
    private float _verticalDistanceD;
    public float verticalDistanceD { get { return _verticalDistanceD; } set { _verticalDistanceD = value; } }
   
    private Vector3 _startPos;
    public Vector3 startPos { get { return _startPos; } set { _startPos = value; } }

    void Start()
    {
        verticalGuideLine.material = verticalLineMaterial;
        horizontalGuideLine.material = horizontalLineMaterial;
    }
    void Update()
    {
        DrawGuideLines();
    }

    public void DrawGuideLines()
    {
        if (_isGuideOn)
        {
            if (horizontalGuideLine != null)
            {

                if (_isHorizontal)
                {
                    horizontalGuideLine.useWorldSpace = true;
                    horizontalGuideLine.positionCount = 2;
                    horizontalGuideLine.SetPosition(0, _startPos + Vector3.right * _horizontalDistanceR);
                    horizontalGuideLine.SetPosition(1, _startPos + Vector3.left * _horizontalDistanceL);
                }
                else
                {
                    horizontalGuideLine.positionCount = 0;
                }
            }

            if (verticalGuideLine != null)
            {


                if (_isVertical)
                {
                    verticalGuideLine.useWorldSpace = true;
                    verticalGuideLine.positionCount = 2;
                    verticalGuideLine.SetPosition(0, _startPos + Vector3.up * _verticalDistanceU);
                    verticalGuideLine.SetPosition(1, _startPos + Vector3.down * _verticalDistanceD);
                }
                else
                {
                    verticalGuideLine.positionCount = 0;
                }
            }
        }

        else
        {
            horizontalGuideLine.positionCount = 0;
            verticalGuideLine.positionCount = 0;

        }
    }

    void OnDrawGizmos()
    {
        if (_isGuideOn)
        {
            //DEBUGGING
            if (_isHorizontal)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(_startPos + Vector3.right * _horizontalDistanceR, 0.15f);

                Gizmos.color = Color.red;
                Gizmos.DrawSphere(_startPos + Vector3.left * _horizontalDistanceL, 0.15f);
            }
            if (_isVertical)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(_startPos + Vector3.up * _verticalDistanceU, 0.15f);

                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(_startPos + Vector3.down * _verticalDistanceD, 0.15f);
            }

        }
    }
}
