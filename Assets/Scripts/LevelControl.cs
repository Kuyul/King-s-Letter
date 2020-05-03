using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public static LevelControl instance;

    //Make objects public so you can manually assign objects from screen
    public GameObject Letter;
    public GameObject[] ButtonPos;
    public GameObject LetterPos;
    public GameObject LevelStampPos;
    public float stampOffsetZ;
    public List<GameObject> MultiChoiceButtons;
    public List<GameObject> Stamps;

    private GameObject levelStamp;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public GameObject getLevelStamp()
    {
        return levelStamp;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Spawn a letter first
        Instantiate(Letter, LetterPos.transform);

        // Selects random stamp to be this level's levelStamp
        var levelStampIndex = Random.Range(0, Stamps.Count);
        levelStamp = Stamps[levelStampIndex];
        levelStamp.gameObject.name = levelStampIndex.ToString();

        // Gives the levelStamp a random rotation (90, 180, 270)
        var randomRotationAmount = Random.Range(1, 4);
        var levelStampRotationZ = randomRotationAmount * 90;
        LevelStampPos.transform.rotation = Quaternion.Euler(LevelStampPos.transform.rotation.x, LevelStampPos.transform.rotation.y, LevelStampPos.transform.rotation.z + levelStampRotationZ);

        // Spawn level stamp on top of letter
        Instantiate(levelStamp, LevelStampPos.transform);
        Debug.Log("Correct stamp for this level is: " + levelStamp.name);

        // Instatiate MultiChoiceButtons in preset positions
        for (var i = 0; i < MultiChoiceButtons.Count; i++)
        {
            var multiChoiceButtonObj = MultiChoiceButtons[i];
            var multiChoiceButtonPos = ButtonPos[i];
            var multiChoiceButton = Instantiate(multiChoiceButtonObj, multiChoiceButtonPos.transform);
            multiChoiceButton.name = i.ToString();

        }

        //Go through the list of preset spawn positions and instantiate the letter at those positions
        //Letter must be created from a prefab - To create any gameobject from heirarcy as a prefab, just drag them into the Prefab folder
        for (var i = 0; i < ButtonPos.Length; i++)
        {
            //Get Random stamp index
            var ran = Random.Range(0, Stamps.Count);
            var stampObj = Stamps[ran];
            //Remove the stamp at pos because we don't want more than one of the same type to be instantiated
            Stamps.RemoveAt(ran);

            // Do this to get any component out of Gameobjects e.g. switch Transform to Collider if you want to get the collider
            var stampPos = ButtonPos[i].GetComponent<Transform>();
            // Set Z offset to bring stamp in front of button
            stampPos.transform.position.Set(stampPos.position.x, stampPos.position.y, stampPos.position.z + stampOffsetZ);

            Instantiate(stampObj, stampPos);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
