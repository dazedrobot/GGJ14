using UnityEngine;
using System.Collections;

public class CCTVTrigger : MonoBehaviour {

    public WallScript wall;

    void OnTriggerStay2D (Collider2D other){
        if (other.tag == "player") {
            PlayerControl player = other.GetComponent<PlayerControl>();
            if (player.state == State.Disappear)
            {
                wall.targetHeight = -2;
            }
            else
                wall.targetHeight = wall.jumpHeight;
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        wall.targetHeight = -2;
    }
}
