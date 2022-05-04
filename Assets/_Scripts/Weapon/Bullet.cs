using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rigidbody;
    private float projectileSpeed = 1f;
    public int bulletDamage = 1;

    private void Awake()
    {
        Physics.IgnoreLayerCollision(6, 6, true);
    }

    private void OnEnable()
    {
        if (rigidbody != null)
        {
            rigidbody.velocity = Vector3.zero;
        }
        Invoke("Disable", 0.5f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<InteractableObject>(out InteractableObject obj))
        {
            obj.Interaction(bulletDamage);
        }
        Disable();
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
