using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public override void OnStartServer()
    {
        base.OnStartServer();

        if (isServer)
        {
            if (!GameNetworkManager.instance.mainClient)
            {
                GameControl.instance.AddPlayer(this);
            }
        }
    }
    
    private void Start()
    {
        if (isClient)
        {
            if (isLocalPlayer && !GameNetworkManager.instance.mainClient)
            {
                Debug.Log("Is localplayer:" + isLocalPlayer);
                LocalController.instance.SetLocalPlayer(this);
            }
        }
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
