using UnityEngine;

public class HealthChangeParticles : MonoBehaviour
{
    public ParticleSystem DamageParticles;

    public void PlayDamageParticles()
    {
        DamageParticles.Play();
    }
}
