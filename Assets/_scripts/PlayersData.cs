using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersData : MonoBehaviour {
    const int LEADER_BOARD_MAX = 10;

    public List<LeaderLine> leaderBoard;
    public bool hasInit = false;

    [System.Serializable]
    public struct LeaderLine
    {
        public string name;
        public int score;
    }

    static public PlayersData _instance;

    static public PlayersData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayersData>();
            }

            return _instance;
        }
    }

    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < LEADER_BOARD_MAX; ++i)
        {
            LeaderLine temp = new LeaderLine();
            temp.name = PlayerPrefs.GetString("name" + i);
            temp.score = PlayerPrefs.GetInt("score" + i);
            if (temp.name.Equals(""))
            {
                temp.score = -1;
            }
            leaderBoard.Add(temp);
        }

        hasInit = true;
    }



    public void UpdateLeaderBoard(string newName,int newScore)
    {
        LeaderLine temp = new LeaderLine();
        temp.name = newName;
        temp.score = newScore;
        leaderBoard.Add(temp);
        leaderBoard.Sort(compare);
        leaderBoard.RemoveAt(leaderBoard.Count - 1);
        for (int i = 0; i < LEADER_BOARD_MAX; ++i)
        {
            PlayerPrefs.SetString("name" + i, leaderBoard[i].name);
            PlayerPrefs.SetInt("score" + i, leaderBoard[i].score);
        }
        PlayerPrefs.Save();
    }

    public int compare(LeaderLine a, LeaderLine b)
    {
        int result = 0;
        if(a.score > b.score)
        {
            result = -1;
        }
        else if(a.score < b.score)
        {
            result = 1;
        }
        return result;
    }
}
