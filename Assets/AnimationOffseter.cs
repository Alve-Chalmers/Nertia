using UnityEngine;

public class AnimationOffseter : MonoBehaviour
{

    [SerializeField] [Range(0f, 1f)] float offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Animator>().SetFloat("offset", offset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
