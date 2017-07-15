using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour {

    [SerializeField]
    private GameObject pickupHolder;
    private float pickupRadius = .75f;

    private GameObject pickup;
    private Rigidbody2D pickupsRigidBody;

    void Update ()
    {
        if (Input.GetButtonDown("Action"))
        {
            GameObject inRangePickup = getPickupInRange();
            if (pickup == null && inRangePickup != null)
            {
                TakePickup(inRangePickup);
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (pickup == null && coll.gameObject.tag == "Pickup")
        {
            TakePickup(coll.gameObject);
        }
    }

    private void TakePickup(GameObject newPickup)
    {
        pickup = newPickup;
        SetPickupTransform(newPickup);
        DisableRigidbody(newPickup);
        newPickup.gameObject.tag = "Held";
    }

    private void DisableRigidbody(GameObject newPickup)
    {
        newPickup.GetComponent<BoxCollider2D>().enabled = false;
        pickupsRigidBody = newPickup.GetComponent<Rigidbody2D>();
        Destroy(newPickup.GetComponent<Rigidbody2D>());
    }

    private void SetPickupTransform(GameObject newPickup)
    {
        newPickup.transform.parent = pickupHolder.transform;
        newPickup.transform.localPosition = Vector3.zero;
        newPickup.transform.localRotation = Quaternion.identity;
    }

    private GameObject getPickupInRange()
    {
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
        foreach (GameObject pickup in pickups)
        {
            if(Vector3.Distance(gameObject.transform.position, pickup.transform.position) < pickupRadius)
            {
                return pickup;
            }
        }
        return null;
    }
}
