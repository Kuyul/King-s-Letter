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

    public override void OnStartServer()
    {
        base.OnStartServer();
        if (isServer && !GameNetworkManager.instance.mainClient)
        {
            id = System.Guid.NewGuid().ToString();
            var p = adjectives[Random.Range(0, adjectives.Count - 1)];
            playerName = p;
            RpcAddPlayer(id, p);
        }
    }

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
                CmdAddPlayer();
            }
        }
    }

    [Command]
    public void CmdAddPlayer()
    {
        GameNetworkManager.instance.gameController.AddPlayer(this);
    }

    //Called from the controller to invoke a function on the main client
    [Command]
    public void CmdButton(string button)
    {
        RpcButton(button, playerName);
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
