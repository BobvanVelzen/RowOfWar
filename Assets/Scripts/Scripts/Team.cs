using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team {

    private bool isLeft;
    public bool IsLeft
    {
        get { return isLeft; }
    }
    public List<Player> players = new List<Player>();

    public Team(bool isLeft)
    {
        this.isLeft = isLeft;
    }

    public void addPlayer(Player player)
    {
        player.Team = this;
        players.Add(player);
    }
}
