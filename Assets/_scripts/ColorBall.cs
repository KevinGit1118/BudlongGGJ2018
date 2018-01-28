using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBall : MonoBehaviour {

    public MeshRenderer ballMeshrenderer = null;
    public Waypoint nextWaypoint;
    public CenterPoint nextCenterPoint;
    public float startSpeed = 10;
    public float endSpeed = 5;
    public float speedDecreaseRate = 2;
    public float currentSpeed = 5;
    public GeneralTable.Type type;
    public Collider ballCollider;
    private Color _ballcolor = Color.white;

    private Vector3 tempPos = Vector3.zero;
    private Vector3 tempResultPos = Vector3.zero;
    private Vector3 tempNextPos = Vector3.zero;
    private Vector3 tempDirection = Vector3.zero;

    public Color BallColor
    {
        get { return _ballcolor; }
        set { _ballcolor = value;
            SetColor(value);
        }
    }

    private void SetColor(Color color)
    {
        ballMeshrenderer.material.color = color;
    }

    public void Start()
    {
        currentSpeed = startSpeed;
    }

    void Update()
    {
        if(currentSpeed > endSpeed)
        {
            currentSpeed -= speedDecreaseRate * Time.deltaTime;
            if(currentSpeed < endSpeed)
            {
                currentSpeed = endSpeed;
            }
        }

        if (nextWaypoint)
        {
            tempPos = this.transform.position;
            tempNextPos = nextWaypoint.transform.position;
            tempDirection = tempNextPos - tempPos;
            tempDirection.y = 0;
            tempDirection.Normalize();
            // speed
            tempDirection *= currentSpeed;
            this.transform.position = new Vector3(tempPos.x + (tempDirection.x * Time.deltaTime), tempPos.y, tempPos.z + (tempDirection.z * Time.deltaTime));
            tempResultPos = this.transform.position;

            /*Debug.Log("====T " + tempPos.ToString());
            Debug.Log("====N " + tempNextPos.ToString());
            Debug.Log("====R " + tempResultPos.ToString());
            Debug.Log("^^ 1: " + ((tempPos.x - tempNextPos.x) * (tempResultPos.x - tempNextPos.x)));
            Debug.Log("^^ 2: " + ((tempPos.z - tempNextPos.z) * (tempResultPos.z - tempNextPos.z)));*/

            if (((tempPos.x - tempNextPos.x) * (tempResultPos.x - tempNextPos.x)) <= 0 && ((tempPos.z - tempNextPos.z) * (tempResultPos.z - tempNextPos.z)) <= 0)
            {
                this.transform.position = new Vector3(tempNextPos.x, this.transform.position.y, tempNextPos.z);
                if(nextWaypoint.GetNextWaypoint())
                {
                    nextWaypoint = nextWaypoint.GetNextWaypoint();
                }
                else
                {
                    nextCenterPoint = nextWaypoint.GetCenterPoint();
                    nextWaypoint = null;
                }
            }
        }
        else if(nextCenterPoint)
        {
            tempPos = this.transform.position;
            tempNextPos = nextCenterPoint.transform.position;
            tempDirection = tempNextPos - tempPos;
            tempDirection.y = 0;
            tempDirection.Normalize();
            // speed
            tempDirection *= currentSpeed;
            this.transform.position = new Vector3(tempPos.x + (tempDirection.x * Time.deltaTime), this.transform.position.y, tempPos.z + (tempDirection.z * Time.deltaTime));
            tempResultPos = this.transform.position;

            // to the center, just destroy
            if (((tempPos.x - tempNextPos.x) * (tempResultPos.x - tempNextPos.x)) <= 0 && ((tempPos.z - tempNextPos.z) * (tempResultPos.z - tempNextPos.z)) <= 0)
            {
                this.transform.position = tempNextPos;
                Destroy(this.gameObject);
            }
        }
        else
        {
            Debug.LogError("#### No next target, should be destroyed!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ColorBall tempColorBall = other.gameObject.GetComponent<ColorBall>();
        if(tempColorBall)
        {
            // Don't case equal for now lol
            if((int)type > (int)tempColorBall.type)
            {
                type = GeneralTable.Combine(type, tempColorBall.type);
                BallColor = GeneralTable.GetColor(type);

                GamePlayManager.Instance.centerPoint.RemoveEstimateColorBallNum();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        CenterPoint tempCenterPoint = other.gameObject.GetComponent<CenterPoint>();
        if(tempCenterPoint)
        {
            nextCenterPoint.match(type);
            ballCollider.enabled = false;
        }
    }
}
