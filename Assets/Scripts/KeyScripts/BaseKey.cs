using UnityEngine;

public class BaseKey: MonoBehaviour
{
    public bool dangerous;

    protected void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag != "Player")
        {
            dangerous = false;
        }

        IOpenable openable = coll.gameObject.GetComponent<IOpenable>();
        IKillable killable = coll.gameObject.GetComponent<IKillable>();

        if (openable != null)
        {
            openable.Open();
            Destroy(gameObject);
        }

        if(killable != null && dangerous)
        {
            killable.Kill();
            Destroy(gameObject);
        }
    }
}
