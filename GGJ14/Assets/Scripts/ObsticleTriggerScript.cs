using UnityEngine;
using System.Collections;

public class ObsticleTriggerScript : MonoBehaviour
{

    bool armour;
    bool pay;
    bool cctv;
    bool jumpable;
    int jumpHeight;
    public bool rising = false;
    bool smashed;
    bool paid;
    float speed = 4;
    Transform centreTrans;


    // Use this for initialization
    void Start ()
    {
        centreTrans = this.transform.parent.Find ("CentreCube");
        //Setup (false, true, true, false, 0);
        //SetPosition (1.5f);
    }

    void RaiseWall ()
    {
        rising = true;

    }
    // Update is called once per frame
    void Update ()
    {
        if (!smashed) {


            if (rising && !paid) {
                if (centreTrans.localPosition.y < 2) {
                    centreTrans.Translate (0, speed * Time.deltaTime, 0);
                }
            } else {
                if (centreTrans.localPosition.y > -2) {
                    centreTrans.Translate (0, -speed * Time.deltaTime, 0);
                }
    
            }

        }

    }

    void SetPosition (float n)
    {
        Vector3 upVec = new Vector3 (centreTrans.position.x, n, centreTrans.position.z);
        centreTrans.position = upVec;
    }
    
    void OnTriggerStay2D (Collider2D other)
    {
        if (other.tag == "player") {
            PlayerControl pControl = other.gameObject.GetComponent ("PlayerControl") as PlayerControl;



            if (cctv && pControl.state == State.Disappear && !paid) {
                rising = false;
            }

            if (pay && pControl.state == State.Pay) {
                paid = true;
                rising = false;
                        
            }

        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (!paid)
            rising = true;
    }

    public void Setup (bool Armour, bool Pay, bool CCTV, bool Jumpable, int JumpHeight)
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

        if (!Armour) {
            Transform t = transform.parent.FindChild ("CentreCube").FindChild("Armour");
            t.gameObject.SetActive (false);
        }
    }
}
