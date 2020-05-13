using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameControl : NetworkBehaviour
{
    private List<GamePlayerController> playerList = new List<GamePlayerController>();

    
    //Called from the player controller class
    public void AddPlayer(GamePlayerController player)
    {
        playerList.Add(player);
    }

    //Called from the player controller class
    public void RemovePlayer(GamePlayerController player)
    {
        playerList.Remove(player);
    }
}
