using UnityEngine;
using System.Collections;

public class CCTVTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D (Collider2D other){
        Transform oTScript = transform.parent.Find ("Trigger");
        oTScript.SendMessage ("RaiseWall");
        Debug.Log ("rasing wall");
    }
}
