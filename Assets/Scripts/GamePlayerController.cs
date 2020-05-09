using System;
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

    public override void OnStartServer()
    {
        id = Guid.NewGuid().ToString();
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
        GameControl.instance.AddPlayer(this);
    }

    [Command]
    public void CmdButtonA()
    {
        GameControl.instance.RpcButtonA();
    }

    [Command]
    public void CmdButtonB()
    {
        GameControl.instance.RpcButtonB();
    }

    [Command]
    public void CmdButtonC()
    {
        GameControl.instance.RpcButtonC();
    }

    [Command]
    public void CmdButtonD()
    {
        GameControl.instance.RpcButtonD();
    }
}
