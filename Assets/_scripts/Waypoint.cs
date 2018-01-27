using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    public Waypoint[] nextWaypoints;
    public CenterPoint centerPoint;

    public int nextWaypointIndex = 0;

    void Start()
    {
        nextWaypointIndex = 0;    
        if(nextWaypoints.Length == 0 && centerPoint == null)
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
}
