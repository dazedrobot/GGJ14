using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "player")
            LevelController.instance.reachedEnd = true;
    }
}
