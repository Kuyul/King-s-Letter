using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GamePlayerController : NetworkBehaviour
{
    [SyncVar]
    public string id;
    [SyncVar]
    public string playerName;

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

    //Called from the server whenever a new player joins the game
    [ClientRpc]
    public void RpcAddPlayer(string pId, string pName)
    {
        if (GameNetworkManager.instance.mainClient)
        {
            GameNetworkManager.instance.uiController.AddPlayer(pId, pName);
        }
    }


    [ClientRpc]
    public void RpcRemovePlayer()
    {
        if (GameNetworkManager.instance.mainClient)
        {
            GameNetworkManager.instance.uiController.RemovePlayer(id);
        }
    }

    private void Start()
    {
        if (isClient)
        {
            if (isLocalPlayer && !GameNetworkManager.instance.mainClient)
            {
                LocalController.instance.SetLocalPlayer(this);
                id = System.Guid.NewGuid().ToString();
                playerName = adjectives[Random.Range(0, adjectives.Count - 1)];
                CmdAddPlayer(id, playerName);
            }
        }
    }

    [Command]
    public void CmdAddPlayer(string pid, string pName)
    {
        GameNetworkManager.instance.gameController.AddPlayer(this);
        RpcAddPlayer(pid, pName);
    }

    //Called from the controller to invoke a function on the main client
    [Command]
    public void CmdButton(string button, string pName)
    {
        RpcButton(button, pName);
    }

    [ClientRpc]
    public void RpcButton(string button, string buttonPlayerName)
    {
        if (GameNetworkManager.instance.mainClient)
        {
            var answer = GameNetworkManager.instance.levelController.GetCorrectAnswer();
            if (answer != button)
            {
                Debug.Log("Incorrect Answer by " + buttonPlayerName);
            }
            else
            {
                Debug.Log("Correct Answer by " + buttonPlayerName);
            }
        }
    }

}
