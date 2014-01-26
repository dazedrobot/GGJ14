using UnityEngine;
using System.Collections;

public class FloorDrop : MonoBehaviour {

    public FloorDrop next;

    private float delay = 1f;

    public void TriggerDrop()
    {
        StartCoroutine (Drop ());
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
