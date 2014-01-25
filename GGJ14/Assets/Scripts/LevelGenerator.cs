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

    void Awake ()
    {
        floor = new List<FloorDrop> ();

        if (level) {
            string[] levelSections = level.text.Split (',');

            bool first = true;
            foreach (string s in levelSections)
            {
                if (!first)
                    AddSection(floorPrefab);
                else
                    first = false;
                if (s == "0")
                {
                    AddSection(floorPrefab);
                }
                else
                {
                    AddSection (wallPrefab);
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

    void AddSection (GameObject prefab)
    {
        GameObject go = GameObject.Instantiate (prefab, new Vector3 (floor.Count * 3, 0, 0), Quaternion.identity) as GameObject;
        FloorDrop section = go.GetComponent<FloorDrop> ();
        AddSection (section);
    }

    void AddSection (FloorDrop section)
    {
        if (floor.Count > 0)
            floor [floor.Count - 1].next = section;
        floor.Add (section);
    }

    IEnumerator StartDropDelayed ()
    {
        yield return new WaitForSeconds (4);
        floor [0].TriggerDrop ();
    }
}
