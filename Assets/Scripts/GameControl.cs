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
        var name = adjectives[Random.Range(0, adjectives.Count - 1)];
        RpcAddPlayer(count, name);
        playerList.Add(playerController);
    }

    [ClientRpc]
    private void RpcAddPlayer(int count, string name)
    {
        uiController.AddNewPlayer(count, name);
    }

    //Deeclare input Rpcs
    [ClientRpc]
    public void RpcButtonA()
    {
        Debug.Log("Input A Pressed!");
    }

    [ClientRpc]
    public void RpcButtonB()
    {
        Debug.Log("Input B Pressed!");
    }

    [ClientRpc]
    public void RpcButtonC()
    {
        Debug.Log("Input C Pressed!");
    }

    [ClientRpc]
    public void RpcButtonD()
    {
        Debug.Log("Input D Pressed!");
    }
}
