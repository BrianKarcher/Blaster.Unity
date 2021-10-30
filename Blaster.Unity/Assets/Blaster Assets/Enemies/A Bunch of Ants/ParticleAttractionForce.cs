using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ParticleSystem))]
public class ParticleAttractionForce : MonoBehaviour
{

    public Transform target;
    public float speed = 5;

    public float rangeToExplode = 5;
    public GameObject ExplosionPrefab;

    private ParticleSystem pS;

    private static ParticleSystem.Particle[] particles; 

    // Update is called once per frame
    void Update()
    {

        particles = new ParticleSystem.Particle[1000];
        if (pS == null) pS = GetComponent<ParticleSystem>();

        var count = pS.GetParticles(particles);

        for (int i = 0; i < count; i++) {
            var particle = particles[i];

            float distance = Vector3.Distance(target.position, particle.position);

            if (distance > rangeToExplode)
            {
                particle.position = Vector3.Lerp(particle.position, target.position, Time.deltaTime / speed);
                particles[i] = particle;
            }
            else if (distance < rangeToExplode) {
                SelfDestruction(particles[i].position);
                particles[i].remainingLifetime = 0;
            }

            pS.SetParticles(particles, count);
        
        }
    }


    private void SelfDestruction(Vector3 pos) {
        Instantiate(ExplosionPrefab, pos, new Quaternion(0,0,0,0));
    }
}
