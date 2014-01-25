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
    public GameObject plane;
    int planeSize;

    void Awake ()
    {
        //Debug.Log ("bit set {}".Format(BitIsSet(3, 1)));
        floor = new List<FloorDrop> ();

        if (level) {
            string[] levelSections = level.text.Split (',');
            planeSize = levelSections.Length;

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
        GameObject obj = GameObject.Instantiate (plane, new Vector3 ((3 * planeSize) / 2, -10, 0), Quaternion.identity) as GameObject;
        obj.transform.localScale.Set (3 * planeSize, 1, 1);
    }

    void AddSection (GameObject prefab, int value)
    {
        GameObject go = GameObject.Instantiate (prefab, new Vector3 (floor.Count * 3, 0, 0), Quaternion.identity) as GameObject;
        FloorDrop section = go.GetComponent<FloorDrop> ();
        AddSection (section);

        if (value > 0) {

            int jumpHeight = 0;


            ObsticleTriggerScript obsticleScript = go.GetComponentInChildren<ObsticleTriggerScript> ();

            if( BitIsSet (value, 0)){
                jumpHeight = Random.Range(1,4);
            }
            else{
                jumpHeight = 0;
            }

            obsticleScript.Setup (!BitIsSet (value, 1), BitIsSet (value, 2), BitIsSet (value, 3), BitIsSet (value, 0), jumpHeight);



        }

    }

    bool BitIsSet (int val, int bit)
    {
        return (1 & (val >> bit)) == 1;
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
