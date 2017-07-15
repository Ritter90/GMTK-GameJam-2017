using UnityEngine;

public class BaseKey: MonoBehaviour
{
    protected void OnCollisionEnter2D(Collision2D coll)
    {
        IOpenable openable = coll.gameObject.GetComponent<IOpenable>();

        if (openable != null)
        {
            openable.Open();
        }
    }
}
