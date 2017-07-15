using UnityEngine;

public class BaseScreenController : MonoBehaviour
{
    protected virtual void Awake()
    {
        RectTransform rect = gameObject.GetComponent<RectTransform>();
        rect.localPosition = Vector2.zero;
    }
}
