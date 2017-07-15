using UnityEngine;

/// <summary>
/// MockKey specifically for testing the IOpenable Interface in the scene
/// </summary>
public class MockKey : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        IOpenable openable = coll.gameObject.GetComponent<IOpenable>();

        if(openable != null)
        {
            openable.Open();
        }
    }
}
