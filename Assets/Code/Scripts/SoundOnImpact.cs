using UnityEngine;
using UnityEngine.Rendering;

public class SoundOnImpact : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    void OnCollisionEnter2D(Collision2D col)
    {
        float maxImpact = 0;
        int contactCount = col.contactCount;
        for (int i = 0; i < contactCount; i++)
        {
            ContactPoint2D contact = col.GetContact(i);
            float v = Vector2.Dot(contact.normal, col.relativeVelocity.normalized);
            if (v >= maxImpact)
                maxImpact = v;
        }

        if (maxImpact < 0.2f) {
            return;
        }

        Debug.Log(maxImpact * col.relativeVelocity.magnitude);
        audioSource.volume = Mathf.SmoothStep(0.0f, 1.3f, maxImpact * col.relativeVelocity.magnitude / 30f);
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
    }
}
