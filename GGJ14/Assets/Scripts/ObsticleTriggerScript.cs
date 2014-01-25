using UnityEngine;
using System.Collections;

public class ObsticleTriggerScript : MonoBehaviour
{

    bool armour;
    bool pay;
    bool cctv;
    bool jumpable;
    int jumpHeight;
    public GameObject cctvCamera;
    bool rising = true;
    bool smashed;

    // Use this for initialization
    void Start ()
    {
        Setup (false, true, true, false, 0);
        //SetPosition (1.5f);
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (!smashed) {
            Transform centreTrans = this.transform.parent.Find ("CentreCube");
            if (rising) {
                if (centreTrans.localPosition.y < 1.5) {
                    centreTrans.Translate (0, 1 * Time.deltaTime, 0);
                }
            } else {
                if (centreTrans.localPosition.y > -1.5) {
                    centreTrans.Translate (0, -1 * Time.deltaTime, 0);
                }
    
            }
        }
    }

    void SetPosition (float n)
    {
        Transform centreTrans = this.transform.parent.Find ("CentreCube");
        Vector3 upVec = new Vector3 (centreTrans.position.x, n, centreTrans.position.z);
        centreTrans.position = upVec;
    }
    
    void OnTriggerStay2D (Collider2D other)
    {
        if (other.tag == "player") {
            PlayerControl pControl = other.gameObject.GetComponent ("PlayerControl") as PlayerControl;

            if (cctv && pControl.state == State.Disappear) {
                rising = false;
            }

            if (!armour && pControl.state == State.Smashing) {
                Transform centreTrans = this.transform.parent.Find ("CentreCube");
                Destroy (centreTrans.gameObject);
                smashed = true;
            }

            if (pay && pControl.state == State.Pay) {
                rising = false;
                        
            }

            if (cctv && pControl.state == State.Idle) {
                rising = true;
            }
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        rising = true;
    }

    void Setup (bool Armour, bool Pay, bool CCTV, bool Jumpable, int JumpHeight)
    {
        cctv = CCTV;
        armour = Armour;
        pay = Pay;
        jumpable = Jumpable;
        jumpHeight = JumpHeight;

        if (!CCTV) {
            Transform t = transform.parent.FindChild ("CCTVCamera");
            t.gameObject.SetActive (false);
        }
        if (!Pay) {
            Transform t = transform.parent.FindChild ("PaySlot");
            t.gameObject.SetActive (false);
        }
    }
}
