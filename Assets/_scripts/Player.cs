using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Input")]
    public KeyCode fireKey = KeyCode.A;
    public KeyCode changeDirKey = KeyCode.S;

    [Space(5)]
    [Header("Color Ball")]
    public GeneralTable.Type type;
    public float ballSpeed = 1;
    public GameObject colorBallRef = null;

    [Space(5)]
    [Header("Waypoint")]
    public Waypoint startWaypoint = null;

    public MeshRenderer playerMeshRenderer;

    public ColorLine colorLine;

    void Awake()
    {
        init();
    }

    void init()
    {
        //keep and do something
        playerMeshRenderer.material.color = GeneralTable.GetColor(type);
    }

    private void Start()
    {
        EnType();
    }

    // Update is called once per frame
    void Update () {

        if (GamePlayManager.CurGameState != GamePlayManager.GameState.InGame)
            return;
        //catch input information
        if (Input.GetKeyDown(fireKey))
        {
            FireColorBall();
        }

        if (Input.GetKeyDown(changeDirKey))
        {
            DeType();
            startWaypoint.ChangeNextWaypoint();
            EnType();
        }
    }

    void FireColorBall()
    {
        GameObject colorballGO = Instantiate(colorBallRef);
        colorballGO.transform.position = this.transform.position;
        colorballGO.GetComponent<ColorBall>().type = type;
        colorballGO.GetComponent<ColorBall>().BallColor = GeneralTable.GetColor(type);
        colorballGO.GetComponent<ColorBall>().nextWaypoint = startWaypoint;
    }

    public void DeType()
    {
        colorLine.RemoveType(type);
        Waypoint current = startWaypoint;
        while(current)
        {
            current.DeType(type);
            current = current.GetNextWaypoint();
        }
    }

    public void EnType()
    {
        Debug.Log("## Player: " + type.ToString());
        colorLine.AddType(type);
        Waypoint current = startWaypoint;
        while (current)
        {
            current.EnType(type);
            current = current.GetNextWaypoint();
        }
    }
}
