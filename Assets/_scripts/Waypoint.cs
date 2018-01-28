using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    public Waypoint[] nextWaypoints;
    public ColorLine[] colorLines;
    public CenterPoint centerPoint;

    public GeneralTable.Type myType = GeneralTable.Type.White;
    public MeshRenderer waypointMeshRenderer;

    public int nextWaypointIndex = 0;

    public ColorLine colorLine;

    void Start()
    {
        nextWaypointIndex = 0;
        if (nextWaypoints.Length == 0 && centerPoint == null)
        {
            Debug.LogError(this.name + " have no next target!");
        }
    }

    // change index to next waypoint
    public void ChangeNextWaypoint()
    {
        nextWaypointIndex++;
        if(nextWaypointIndex >= nextWaypoints.Length)
        {
            nextWaypointIndex = 0;
        }
    }

    // get next waypoint
    public Waypoint GetNextWaypoint()
    {
        if(nextWaypoints.Length == 0)
        {
            return null;
        }
        else
        {
            return nextWaypoints[nextWaypointIndex];
        }
    }

    // get center point
    public CenterPoint GetCenterPoint()
    {
        return centerPoint;
    }

    public void EnType(GeneralTable.Type type)
    {
        myType = GeneralTable.Combine(myType, type);
        waypointMeshRenderer.material.color = GeneralTable.GetColor(myType);
        if (colorLine)
        {
            colorLine.AddType(type);
        }
        if (colorLines.Length > 0)
        {
            colorLines[nextWaypointIndex].AddType(type);
        }
    }

    public void DeType(GeneralTable.Type type)
    {
        myType = GeneralTable.Remove(myType, type);
        waypointMeshRenderer.material.color = GeneralTable.GetColor(myType);
        if (colorLine)
        {
            colorLine.RemoveType(type);
        }
        if(colorLines.Length > 0)
        {
            colorLines[nextWaypointIndex].RemoveType(type);
        }
    }
}
