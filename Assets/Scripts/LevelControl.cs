using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public static LevelControl instance;

    //Make objects public so you can manually assign objects from screen
    public GameObject Letter;
    public GameObject[] StampPos;
    public GameObject LetterPos;

    public List<GameObject> Stamps;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Spawn a letter first
        Instantiate(Letter, LetterPos.transform);

        //Go through the list of preset spawn positions and instantiate the letter at those positions
        //Letter must be created from a prefab - To create any gameobject from heirarcy as a prefab, just drag them into the Prefab folder
        for(var i = 0; i < StampPos.Length; i++)
        {
            //Get Random stamp index
            var ran = Random.Range(0, Stamps.Count);
            var stampObj = Stamps[ran];
            //Remove the stamp at pos because we don't want more than one of the same type to be instantiated
            Stamps.RemoveAt(ran);

            //Do this to get any component out of Gameobjects e.g. switch Transform to Collider if you want to get the collider
            var summonPos = StampPos[i].GetComponent<Transform>();
            var stamp = Instantiate(stampObj, summonPos);
            //Set Stamp name
            stamp.gameObject.name = "Stamp " + i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
