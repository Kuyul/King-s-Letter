using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameControl : NetworkBehaviour
{
    //Making this a static varible makes the components of this code visible for every other Unity code
    public static GameControl instance;
    public TextMesh showText;
    public UIController uiController;

    private List<PlayerController> playerList = new List<PlayerController>();

    private List<string> adjectives = new List<string>
    {
        "Groovy",
        "Crazy-legs",
        "Very Tactful",
        "Creepy",
        "Fluffy",
        "Zippy",
        "Fiercely Loyal",
        "Magical",
        "Metal"
    };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void AddPlayer(PlayerController playerController)
    {
        var count = playerList.Count;
        RpcAddPlayer(count);
        playerList.Add(playerController);
    }

    [ClientRpc]
    private void RpcAddPlayer(int count)
    {
        uiController.AddNewPlayer(count, name);
    }
}
