using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{

    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public int size = 40;
    public TextAsset level;
    List<FloorDrop> floor;

    void Start ()
    {
        floor = new List<FloorDrop> ();

        if (level) {
            string[] levelSections = level.text.Split (',');

            bool first = true;
            foreach (string s in levelSections) {
                if (!first)
                    AddSection (floorPrefab, 0);
                else
                    first = false;
                if (s == "0") {
                    AddSection (floorPrefab, 0);
                } else {
                    AddSection (wallPrefab, int.Parse (s));
                }
            }
        } else {
            for (int i =0; i < size; i++) {
                GameObject go = GameObject.Instantiate (floorPrefab, new Vector3 (i * 3, 0, 0), Quaternion.identity) as GameObject;
                FloorDrop section = go.GetComponent<FloorDrop> ();
                AddSection (section);
            }
        }
        StartCoroutine (StartDropDelayed ());
    }

    void AddSection (GameObject prefab, int value)
    {
        GameObject go = GameObject.Instantiate (prefab, new Vector3 (floor.Count * 3, 0, 0), Quaternion.identity) as GameObject;
        FloorDrop section = go.GetComponent<FloorDrop> ();
        AddSection (section);

        if (value > 0) {

            int jumpHeight = 0;


            ObsticleTriggerScript obsticleScript = go.GetComponentInChildren<ObsticleTriggerScript> ();

            if( Utility.BitIsSet (value, 0)){
                jumpHeight = Random.Range(1,4);
            }
            else{
                jumpHeight = 0;
            }

            obsticleScript.Setup (!Utility.BitIsSet (value, 1), Utility.BitIsSet (value, 2), Utility.BitIsSet (value, 3), Utility.BitIsSet (value, 0), jumpHeight);



        }

    }

    void AddSection (FloorDrop section)
    {
        if (floor.Count > 0)
            floor [floor.Count - 1].next = section;
        floor.Add (section);
        section.transform.parent = transform;
    }

    public void CleanUp()
    {
        transform.localPosition = Vector3.up * -200;
    }

    IEnumerator StartDropDelayed ()
    {
        yield return new WaitForSeconds (4);
        floor [0].TriggerDrop ();
    }
}
