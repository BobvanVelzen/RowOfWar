using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour {

    private Team[] teams = new Team[2];
    private Player[] players;
    private int leftScore = 0;
    private int rightScore = 0;
    private float ropeValue = 0;
    public float ropeThreshold = 60f;
    private float multiplier = 0.1f;

    private UIManager visualManager;

    // Use this for initialization
    private void Awake () {
        visualManager = GetComponent<UIManager>();

        if (players == null || players.Length == 0)
        {
            List<Player> playerList = new List<Player>();

            GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < objs.Length; i++)
            {
                Player player = objs[i].GetComponent<Player>();
                if (player != null)
                {
                    playerList.Add(player);
                }
            }

            teams[0] = new Team(true);
            teams[1] = new Team(false);
            players = new Player[playerList.Count];
            for (int i = 0; i < playerList.Count; i++)
            {
                players[i] = playerList[i];
                if (teams[0].players.Count <= teams[1].players.Count)
                    teams[0].addPlayer(players[i]);
                else teams[1].addPlayer(players[i]);
            }
        }
    }

    private void Start ()
    {
        if (visualManager != null)
            visualManager.CreateSliders(players);
    }

    // Update is called once per frame
    private void FixedUpdate () {
        MoveRope();
    }

    float RopePositionProgress(RowingMachine player, RowingMachine CPU, float maxValue)
    {
        float value = multiplier * (Mathf.Abs(player.Offset) - Mathf.Abs(CPU.Offset));
        value = Mathf.Clamp(value, -maxValue, maxValue);
        return Mathf.Abs(value) > 0.01f ? value : 0f;
    }

    private void MoveRope()
    {
        // TODO: Get players in a cleaner way
        if (players.Length < 2)
            return;

        float movement = RopePositionProgress(players[0].RowingMachine, players[1].RowingMachine, 1f);
        ropeValue += movement;

        if (ropeValue > ropeThreshold)
        {
            rightScore++;
            Reset();
        } else if (ropeValue < -ropeThreshold)
        {
            leftScore++;
            Reset();
        }

        foreach (Player player in players)
        {
            player.Move(movement);
        }
        visualManager.SetRopeSliderValue(ropeValue);
    }

    private void Reset()
    {
        visualManager.SetScore(leftScore, rightScore);
        ropeValue = 0;

        foreach (Player player in players)
        {
            player.ResetPosition();
        }
    }
}
