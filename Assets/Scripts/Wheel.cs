using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField]
    CircleCollider2D pcc;


    Vector3 originalLocalPosition;
    void Awake()
    {
        originalLocalPosition = transform.localPosition;
        Debug.Log(originalLocalPosition);
    }

    void OnEnable()
    {
        transform.localPosition = originalLocalPosition;
    }

    void Update()
    {
        Debug.Log(transform.localPosition);
    }


    void OnDisable()
    {
    }
}
