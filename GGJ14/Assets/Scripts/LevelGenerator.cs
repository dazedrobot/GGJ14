using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{

    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public int size = 40;
    public GameObject endTriggerPrefab;
    public TextAsset level;
    List<FloorDrop> floor;

    void Start ()
    {
        floor = new List<FloorDrop> ();

        if (level) {
            string[] levelSections = level.text.Split (',');

            for (int i=0; i < 5; i++)
            {
                AddSection (floorPrefab, 0);
            }

            foreach (string s in levelSections) {

                string[] data = s.Split(':');

                AddSection (floorPrefab, 0);
                if (data[0] == "0") {
                    AddSection (floorPrefab, 0);
                } else {
                    AddSection (wallPrefab, int.Parse (data[0]));
                }
                if (data.Length > 1 && data[1] == "e"){
                    GameObject endTrigger = Instantiate(endTriggerPrefab, floor[floor.Count-1].transform.position, Quaternion.identity) as GameObject;
                    endTrigger.transform.parent = transform;
                }
            }
        } else {
            for (int i =0; i < size; i++) {
                GameObject go = GameObject.Instantiate (floorPrefab, new Vector3 (i * 3, 0, 0), Quaternion.identity) as GameObject;
                FloorDrop section = go.GetComponent<FloorDrop> ();
                AddSection (section);
            }
        }
        floor [0].TriggerDrop ();
    }

    public void ResetLevel()
    {
        BroadcastMessage ("Reset");
//        foreach (FloorDrop section in floor) {
//            section.Reset();
//        }
        floor [0].TriggerDrop ();
    }

    void AddSection (GameObject prefab, int value)
    {
        GameObject go = GameObject.Instantiate (prefab, new Vector3 ((floor.Count - 5) * 3, 0, 0), Quaternion.identity) as GameObject;
        FloorDrop section = go.GetComponent<FloorDrop> ();
        AddSection (section);

        if (value > 0) {

            Obsticle obsticleScript = go.GetComponent<Obsticle> ();

            obsticleScript.Setup (!Utility.BitIsSet (value, 1), Utility.BitIsSet (value, 2), Utility.BitIsSet (value, 3), Utility.BitIsSet (value, 0));

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
}
