using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBall : MonoBehaviour {

    public MeshRenderer ballMeshrenderer = null;
    public Waypoint nextWaypoint;
    public CenterPoint nextCenterPoint;
    public float speed = 5;
    public GeneralTable.Type type;
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


    void Update()
    {
        if(nextWaypoint)
        {
            tempPos = this.transform.position;
            tempNextPos = nextWaypoint.transform.position;
            tempDirection = tempNextPos - tempPos;
            tempDirection.Normalize();
            // speed
            tempDirection *= speed;
            this.transform.position = new Vector3(tempPos.x + (tempDirection.x * Time.deltaTime), 0, tempPos.z + (tempDirection.z * Time.deltaTime));
            tempResultPos = this.transform.position;

            if(((tempPos.x - tempNextPos.x) * (tempResultPos.x - tempNextPos.x)) <= 0 && ((tempPos.z - tempNextPos.z) * (tempResultPos.z - tempNextPos.z)) <= 0)
            {
                this.transform.position = tempNextPos;
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
            tempDirection.Normalize();
            // speed
            tempDirection *= speed;
            this.transform.position = new Vector3(tempPos.x + (tempDirection.x * Time.deltaTime), 0, tempPos.z + (tempDirection.z * Time.deltaTime));
            tempResultPos = this.transform.position;

            if (((tempPos.x - tempNextPos.x) * (tempResultPos.x - tempNextPos.x)) <= 0 && ((tempPos.z - tempNextPos.z) * (tempResultPos.z - tempNextPos.z)) <= 0)
            {
                this.transform.position = tempNextPos;
                nextCenterPoint.match(type);
                Destroy(this.gameObject);
            }
        }
        else
        {
            Debug.LogError("#### No next target, should be destroyed!");
        }
    }


}
