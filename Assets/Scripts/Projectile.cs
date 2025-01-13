using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    List<ParticleSystem> laserParticles;

    [Header ("- Laser Beam Parameters -")]
    [SerializeField] float laserBeamSpeed = 60;
    [SerializeField] float travelDistance = 35;

    void Start()
    {
        laserParticles = new List<ParticleSystem>(GetComponentsInChildren<ParticleSystem>());

        ActivateProjectile();
        SetProjectileSpeed(laserBeamSpeed);
    }

    void ActivateProjectile()
    {
        foreach (var particle in laserParticles)
        {
            particle.Play();
        }
    }

    void DeactivateProjectile()
    {
        foreach (var particle in laserParticles)
        {
            particle.Stop();
        }        
    }

    void SetProjectileSpeed(float projectileSpeed)
    {
        foreach (var particle in laserParticles)
        {
            // In order to modify "Start Speed" field of the -struct- Particle System: 
            ParticleSystem.MainModule mainModule = particle.main;
            mainModule.startSpeed = projectileSpeed;

            // Manage the projectile's distance based on its speed.
            mainModule.startLifetime = travelDistance / projectileSpeed;

            // Don't forget to add a Trigger at the top of the screen to kill particles.
        } 
    }
}
