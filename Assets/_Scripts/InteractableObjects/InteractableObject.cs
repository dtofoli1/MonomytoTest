using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public Rigidbody rigidbody;
    public abstract void Disable();

    public abstract void Interaction(int value = 0);

    public virtual void OnEnable()
    {
        if (rigidbody != null)
        {
            rigidbody.velocity = Vector2.up * 1;
        }
    }

    public virtual void OnDisable()
    {
        Disable();
    }
}
