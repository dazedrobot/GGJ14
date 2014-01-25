using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 position = transform.localPosition;
		transform.localPosition = new Vector3 (target.transform.localPosition.x, position.y, position.z); 
	}
}
