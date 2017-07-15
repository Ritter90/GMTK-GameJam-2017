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

    public float throwForce = 100f;

    void Update ()
    {
        if (Input.GetButtonDown("Action"))
        {
            GameObject inRangePickup = getPickupInRange();
            if (pickup == null && inRangePickup != null)
            {
                TakePickup(inRangePickup);
            }
            else if (pickup != null)
            {
                ThrowPickup();
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

    private void ThrowPickup()
    {
        ReleasePickupTransform();
        EnablePickupRigidbody();
        pickup.gameObject.tag = "Pickup";
        pickup.transform.position = new Vector2(pickup.transform.position.x + (.5f * Mathf.Sign(gameObject.transform.localScale.x)), pickup.transform.position.y);
        pickup.GetComponent<Rigidbody2D>().AddForce(new Vector2(throwForce * Mathf.Sign(gameObject.transform.localScale.x), 0));
        pickup = null;
    }

    private void TakePickup(GameObject newPickup)
    {
        pickup = newPickup;
        SetPickupTransform(pickup);
        DisablePickupRigidbody(pickup);
        pickup.gameObject.tag = "Held";
    }

    private void EnablePickupRigidbody()
    {
        pickup.GetComponent<BoxCollider2D>().enabled = true;
        pickup.AddComponent<Rigidbody2D>();
    }

    private void DisablePickupRigidbody(GameObject newPickup)
    {
        newPickup.GetComponent<BoxCollider2D>().enabled = false;
        //pickupsRigidBody = newPickup.GetComponent<Rigidbody2D>();
        Destroy(newPickup.GetComponent<Rigidbody2D>());
    }

    private void ReleasePickupTransform()
    {
        pickup.transform.SetParent(null);
    }

    private void SetPickupTransform(GameObject newPickup)
    {
        newPickup.transform.SetParent(pickupHolder.transform);
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
