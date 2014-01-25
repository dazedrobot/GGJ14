using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {

    public GameObject floorPrefab;
    public GameObject wallPrefab;

    public int size = 40;

    List<FloorDrop> floor;

	void Awake () {
        floor = new List<FloorDrop> ();

        FloorDrop previous = null;
        for (int i =0; i < size; i++)
        {
            GameObject go = GameObject.Instantiate(floorPrefab, new Vector3(i*3,0,0), Quaternion.identity) as GameObject;
            FloorDrop current = go.GetComponent<FloorDrop>();
            if (previous)
                previous.next = current;
            previous = current;
            floor.Add(current);
        }
        StartCoroutine (StartDropDelayed ());
	}
	
	void Update () {
	
	}

    IEnumerator StartDropDelayed()
    {
        yield return new WaitForSeconds (4);
        floor [0].TriggerDrop ();
    }
}
