using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UIController : MonoBehaviour
{
    public Text connection;
    public GameObject playerUI;
    public float playerSlotOffset = 20f;
    public RectTransform playerInfo;
    public GameNetworkManager gameNetworkManager;

    private List<GameObject> playerList = new List<GameObject>();
    private Dictionary<string, GameObject> playerUiList = new Dictionary<string, GameObject>();

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

    public void AddPlayer(string playerId, string playerName)
    {
        var newUiObj = Instantiate(playerUI, playerInfo);
        newUiObj.GetComponent<Text>().text = playerName;
        playerUiList[playerId] = newUiObj;
        UpdatePlayer();
    }

    public void RemovePlayer(string playerId)
    {
        var obj = playerUiList[playerId];
        playerUiList.Remove(playerId);
        Destroy(obj);
        UpdatePlayer();
    }

    public void UpdatePlayer()
    {
        var offset = 0f;
        foreach (string key in playerUiList.Keys)
        {
            var rect =playerUiList[key].GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector3(0, offset);
            offset -= playerSlotOffset;
        }
    }
}
