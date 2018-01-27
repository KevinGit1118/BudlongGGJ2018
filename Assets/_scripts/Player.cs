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
        EnColor();
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
            DeColor();
            startWaypoint.ChangeNextWaypoint();
            EnColor();
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

    public void DeColor()
    {
        colorLine.ResetColor();
        Waypoint current = startWaypoint;
        while(current)
        {
            current.DeColor();
            current = current.GetNextWaypoint();
        }
    }

    public void EnColor()
    {
        colorLine.SetColor(GeneralTable.GetColor(type));
        Waypoint current = startWaypoint;
        while (current)
        {
            current.EnColor(GeneralTable.GetColor(type));
            current = current.GetNextWaypoint();
        }
    }
}
