using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    PlayerController playerController;

    List<ParticleSystem> laserParticles;

    [Header ("- Laser Beam Parameters -")]
    [SerializeField] float laserBeamSpeed = 60;
    [SerializeField] float travelDistance = 35;
    

    void Start()
    {
        playerController = GetComponent<PlayerController>(); // The script.
        playerController.OnShooting += ActivateProjectile;
        playerController.OnStopShooting += DeactivateProjectile;

        laserParticles = new List<ParticleSystem>(GetComponentsInChildren<ParticleSystem>());
        DeactivateProjectile();
        SetProjectileSpeed(laserBeamSpeed);
    }

    void OnDestroy()
    {
        if (playerController != null)
        {   
            playerController.OnShooting -= ActivateProjectile;
            playerController.OnStopShooting -= DeactivateProjectile;
        }
    }

    public void ActivateProjectile()
    {
        foreach (var particle in laserParticles)
        {
            if (!particle.isPlaying)
            {
                particle.Play();
            }
            else
            {
                continue;
            }
        }
    }

    public void DeactivateProjectile()
    {
        foreach (var particle in laserParticles)
        {
            if (particle.isPlaying)
            {
                particle.Stop();
            }
            else
            {
                continue;
            }
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
        } 
    }
}
