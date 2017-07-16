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
    private float hitForce = 100f;
    private Player playerInfo;

    public Transform startPunch;
    public Transform endPunch;

    public Animator animator;

    public Vector2 ThrowForceVector
    {
        get { return new Vector2(throwForce * Mathf.Sign(gameObject.transform.localScale.x), 0); }
    }

    public Vector2 DropForceVector
    {
        get { return new Vector2(0, 1) * dropForce; }
    }

    public Vector2 HitForceVector
    {
        get { return new Vector2(hitForce * Mathf.Sign(gameObject.transform.localScale.x), hitForce); }
    }

    void Awake()
    {
        playerInfo = GetComponent<Player>();
        animator = GetComponent<Animator>();
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
                ReleasePickup(true, true);
            }
            else
            {
                Punch();
            }
        }
	}

    public void DropPickup()
    {
        if (pickup != null)
        {
            ReleasePickup(false, false);
        }
    }

    private void Punch()
    {
        animator.SetTrigger("punch");
        RaycastHit2D hitInfo = Physics2D.Linecast(startPunch.position, endPunch.position, 1 << LayerMask.NameToLayer("Player"));
        
        if (hitInfo.collider != null)
        {
            IHittable hittable = hitInfo.collider.gameObject.GetComponent<IHittable>();
            if(hittable != null)
            {
                hittable.Hit(HitForceVector);
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

    private void ReleasePickup(bool isThrown, bool makeDangerous)
    {
        ReleasePickupTransform();
        EnablePickupRigidbody();
        pickup.gameObject.tag = "Pickup";
        if(isThrown)
        {
            pickup.transform.position = new Vector2(pickup.transform.position.x + (.5f * Mathf.Sign(gameObject.transform.localScale.x)), pickup.transform.position.y);
            pickup.GetComponent<Rigidbody2D>().AddForce(ThrowForceVector);
            animator.SetTrigger("punch");
        }
        else
        {
            pickup.transform.position = new Vector2(pickup.transform.position.x, pickup.transform.position.y + 1f);
            pickup.GetComponent<Rigidbody2D>().AddForce(DropForceVector);
        }
        
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
