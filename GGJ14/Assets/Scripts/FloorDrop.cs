using UnityEngine;
using System.Collections;

public class FloorDrop : MonoBehaviour {

    public FloorDrop next;

    public void TriggerDrop()
    {
        StartCoroutine (Drop ());
    }

    IEnumerator Drop()
    {
        yield return new WaitForSeconds (1);
        rigidbody2D.isKinematic = false;
        rigidbody2D.fixedAngle = false;

        if (next)
            next.TriggerDrop();
    }
}
