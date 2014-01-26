using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {
   
    public float jumpHeight;

    public float targetHeight;

    public bool downOverride = false;

    float speed = 4;

    void Start()
    {
        SetPosition (targetHeight);
    }

    void Update()
    {
        float diff = targetHeight - transform.localPosition.y;

        int sign = diff > 0 ? 1 : -1;
        if (!downOverride) {
            if (diff < -0.1 || diff > 0.1) {
                transform.Translate (0, sign * speed * Time.deltaTime, 0);
            } else {
                transform.localPosition = new Vector3 (0, targetHeight, 0);
            }
        } else {
            if (transform.localPosition.y > -2) {
                transform.Translate (0, -1 * speed * Time.deltaTime, 0);
            } else {
                transform.localPosition = new Vector3 (0, -2, 0);
            }
        }

    }
    
    void SetPosition (float n)
    {
        Vector3 upVec = new Vector3 (transform.localPosition.x, n, transform.localPosition.z);
        transform.localPosition = upVec;
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Hammer" && !transform.parent.GetComponentInChildren<Obsticle>().armour) {     
            Destroy(this.gameObject);
        }
    }
}
