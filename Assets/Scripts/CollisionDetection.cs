using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For the Projectile's Particle System
public class CollisionDetection : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Trigger")
        {
            Debug.Log("Projectile has collided with the Trigger!");
        }
        else
        {
            Debug.Log($"Projectile has collided with: {other.name}");
        }
    }
}
