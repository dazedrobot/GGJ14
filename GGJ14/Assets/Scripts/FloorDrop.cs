using UnityEngine;
using System.Collections;

public class FloorDrop : MonoBehaviour {

    public FloorDrop next;

    private float delay = 0.5f;

    Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    public void TriggerDrop()
    {
        StartCoroutine ("Drop");
    }

    public void Reset ()
    {
        StopCoroutine ("Drop");
        rigidbody2D.isKinematic = true;
        rigidbody2D.fixedAngle = true;
        transform.localPosition = initialPosition;
        transform.localRotation = Quaternion.identity;
    }

    IEnumerator Drop()
    {
        yield return new WaitForSeconds (delay);
        rigidbody2D.isKinematic = false;
        rigidbody2D.fixedAngle = false;
        rigidbody2D.AddTorque (1000);

        if (next)
            next.TriggerDrop();
    }
}
