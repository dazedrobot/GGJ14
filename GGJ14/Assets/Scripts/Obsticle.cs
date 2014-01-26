using UnityEngine;
using System.Collections;

public class Obsticle : MonoBehaviour
{
    public WallScript wall;
    public BoxCollider2D cameraZone;
    public BoxCollider2D payZone;

    public bool armour;
    public bool pay;
    public bool cctv;
    public bool jumpable;

    int jumpHeight;
    public bool rising = false;
    bool paid;


    void Awake ()
    {
        Setup (armour, pay, cctv, jumpable);
    }

    void RaiseWall ()
    {
        Debug.Log ("RW");
        rising = true;

    }

    void Reset()
    {
        Setup (armour, pay, cctv, jumpable);
    }

    public void Setup (bool Armour, bool Pay, bool CCTV, bool Jumpable)
    {
        cctv = CCTV;
        armour = Armour;
        pay = Pay;
        jumpable = Jumpable;
        wall.jumpHeight = Jumpable ? 0 : 2;
        if (CCTV)
            wall.targetHeight = -2;
        else
            wall.targetHeight = Jumpable ? 0 : 2;


        transform.Find("CCTVCamera").gameObject.SetActive (CCTV);
        cameraZone.enabled = CCTV;

        transform.FindChild ("PaySlot").gameObject.SetActive (Pay);
        payZone.enabled = Pay;

        wall.transform.FindChild("Armour").gameObject.SetActive (Armour);

    }
}
