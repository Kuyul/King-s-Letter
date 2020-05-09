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
    private string correctAnswer;
    private string[] answerChoices = { "A", "B", "C", "D" };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public string GetCorrectAnswer()
    {
        return correctAnswer;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Spawn a letter first
        Instantiate(Letter, LetterPos.transform);

        // Selects random stamp to be this level's levelStamp
        var levelStampIndex = Random.Range(0, Stamps.Count);
        levelStamp = Stamps[levelStampIndex];
        correctAnswer = answerChoices[levelStampIndex];

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
        }

        //Go through the list of preset spawn positions and instantiate the letter at those positions
        //Letter must be created from a prefab - To create any gameobject from heirarcy as a prefab, just drag them into the Prefab folder
        for (var i = 0; i < ButtonPos.Length; i++)
        {
            var stampObj = Stamps[i];

            // Do this to get any component out of Gameobjects e.g. switch Transform to Collider if you want to get the collider
            var stampPos = ButtonPos[i].GetComponent<Transform>();
            // Set Z offset to bring stamp in front of button
            stampPos.transform.position.Set(stampPos.position.x, stampPos.position.y, stampPos.position.z + stampOffsetZ);

            Instantiate(stampObj, stampPos);
        }
    }
}
