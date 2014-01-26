using UnityEngine;
using System.Collections;

public class Payment : MonoBehaviour {

    public WallScript wall;
    
    void OnTriggerStay2D (Collider2D other)
    {
        if (other.tag == "player") {
            PlayerControl pControl = other.gameObject.GetComponent<PlayerControl>();

            
            if (pControl.state == State.Pay) {
                wall.downOverride = true;
            }
            
        }
    }

}
