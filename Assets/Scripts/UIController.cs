using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Text connection;
    public List<Text> playerSlots;

    public GameNetworkManager gameNetworkManager;

    // Start is called before the first frame update
    void Start()
    {
        var netHUD = gameNetworkManager.GetComponent<NetworkManagerHUD>();
        // default to 1 so if the scene is started in editor, debug HUD shows
        netHUD.showGUI = PlayerPrefs.GetInt("ShowUnityHUD", 1) == 1;
    }
    
    private void OnDisable()
    {
        var netHUD = gameNetworkManager.GetComponent<NetworkManagerHUD>();
        netHUD.showGUI = false;
    }

    public void StartGame()
    {
        GameControl.instance.StartGame();
    }

    public void AddNewPlayer(int count, string name)
    {
        playerSlots[count].text = name;
    }
}
