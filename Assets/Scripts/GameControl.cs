using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameControl : NetworkBehaviour
{
    //Making this a static varible makes the components of this code visible for every other Unity code
    public static GameControl instance;
    public UIController uiController;
    public GameObject postLoad;
    public GameObject waiting;
    public LevelControl levelController;

    private List<GamePlayerController> playerList = new List<GamePlayerController>();

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

    //Called when start game button is pressed
    public void StartGame()
    {
        waiting.SetActive(false);
        postLoad.SetActive(true);
    }

    //Called from the player controller class
    public void AddPlayer(GamePlayerController player)
    {
        var count = playerList.Count;
        var name = adjectives[Random.Range(0, adjectives.Count - 1)];
        player.playerName = name;
        playerList.Add(player);
        RpcAddPlayer(player.id, player.playerName);
    }
    [ClientRpc]
    private void RpcAddPlayer(string id, string name)
    {
        uiController.AddPlayer(id, name);
    }

    //Called from the player controller class
    public void RemovePlayer(GamePlayerController player)
    {
        var index = playerList.IndexOf(player);
        playerList.RemoveAt(index);
        RpcRemovePlayer(player.id);
    }
    [ClientRpc]
    private void RpcRemovePlayer(string id)
    {
        uiController.RemovePlayer(id);
    }
    
    //Deeclare input Rpcs
    [ClientRpc]
    public void RpcButtonA()
    {
        var answer = levelController.GetCorrectAnswer();
        if (answer != "A")
        {
            Debug.Log("Incorrect Answer");
        }else
        {
            Debug.Log("Correct Answer");
        }
    }

    [ClientRpc]
    public void RpcButtonB()
    {
        var answer = levelController.GetCorrectAnswer();
        if (answer != "B")
        {
            Debug.Log("Incorrect Answer");
        }
        else
        {
            Debug.Log("Correct Answer");
        }
    }

    [ClientRpc]
    public void RpcButtonC()
    {
        var answer = levelController.GetCorrectAnswer();
        if (answer != "C")
        {
            Debug.Log("Incorrect Answer");
        }
        else
        {
            Debug.Log("Correct Answer");
        }
    }

    [ClientRpc]
    public void RpcButtonD()
    {
        var answer = levelController.GetCorrectAnswer();
        if (answer != "D")
        {
            Debug.Log("Incorrect Answer");
        }
        else
        {
            Debug.Log("Correct Answer");
        }
    }
}
