using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Input")]
    public KeyCode fireKey = KeyCode.A;
    public KeyCode changeDirKey = KeyCode.S;

    [Space(5)]
    [Header("Color Ball")]
    public Color ballColor = Color.red;
    public float ballSpeed = 1;
    public GameObject colorBallRef = null;

    [Space(5)]
    [Header("Waypoint")]
    public Waypoint startWaypoint = null;
    

    void Awake()
    {
        init();
    }

    void init()
    {
        //keep and do something
    }

    // Update is called once per frame
    void Update () {
        //catch input information
        if (Input.GetKeyDown(fireKey))
        {
            FireColorBall();
        }

        if (Input.GetKeyDown(changeDirKey))
        {
            startWaypoint.ChangeNextWaypoint();
        }
    }

    void FireColorBall()
    {
        GameObject colorballGO = Instantiate(colorBallRef);
        colorballGO.transform.position = this.transform.position;
        colorballGO.GetComponent<ColorBall>().BallColor = ballColor;
        colorballGO.GetComponent<ColorBall>().nextWaypoint = startWaypoint;
    }


}
