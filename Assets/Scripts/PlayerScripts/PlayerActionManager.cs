using System;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour {

    [SerializeField]
    private GameObject pickupHolder;
    private float pickupRadius = .75f;

    public GameObject pickup;
    private Rigidbody2D pickupsRigidBody;

    private float throwForce = 500f;
    private float dropForce = 250f;
    private Player playerInfo;

    public Vector2 ThrowForceVector
    {
        get { return new Vector2(throwForce * Mathf.Sign(gameObject.transform.localScale.x), 0); }
    }

    public Vector2 DropForceVector
    {
        get { return new Vector2(0, 1) * dropForce; }
    }

    void Awake()
    {
        playerInfo = GetComponent<Player>();
    }

    void Update ()
    {
        if (InputManager.GetActionInput(playerInfo.playerNumber))
        {
            GameObject inRangePickup = getPickupInRange();
            if (pickup == null && inRangePickup != null)
            {
                TakePickup(inRangePickup);
            }
            else if (pickup != null)
            {
                ReleasePickup(ThrowForceVector, true);
            }
        }
	}

    public void DropPickup()
    {
        if (pickup != null)
        {
            ReleasePickup(DropForceVector, false);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (pickup == null && coll.gameObject.tag == "Pickup")
        {
            TakePickup(coll.gameObject);
        }
    }

    private void ReleasePickup(Vector2 force, bool makeDangerous)
    {
        ReleasePickupTransform();
        EnablePickupRigidbody();
        pickup.gameObject.tag = "Pickup";
        pickup.transform.position = new Vector2(pickup.transform.position.x + (.5f * Mathf.Sign(gameObject.transform.localScale.x)), pickup.transform.position.y);
        pickup.GetComponent<Rigidbody2D>().AddForce(force);
        if(makeDangerous)
        {
            SetPickupDangerousIfIDangerous();
        }
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

    private void SetPickupDangerousIfIDangerous()
    {
        IDangerous dangerousPickup = pickup.GetComponent<IDangerous>();

        if (dangerousPickup != null)
        {
            dangerousPickup.SetDangerous();
        }
    }
}
