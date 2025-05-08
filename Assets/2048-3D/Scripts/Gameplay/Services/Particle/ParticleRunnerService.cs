using G2048_3D.Pool;
using G2048_3D.Services.ConfigsProvider;
using System.Collections.Generic;
using UnityEngine;

namespace G2048_3D.Gameplay.Services.Particle
{
    public class ParticleRunnerService
    {
        private const string ParticleContainer = "Particle Container";
        private readonly IConfigsProvider _configsProvider;

        private Transform _container;

        private Dictionary<ParticleType, ChainPool<ParticleSystem>> _particles = new();

        private ParticleRunnerService(IConfigsProvider staticData)
        {
            _configsProvider = staticData;


            _container = new GameObject(ParticleContainer).transform;
            ParticleData[] data = _configsProvider.ForParticles();

            foreach (ParticleData particle in data)
            {
                _particles[particle.Type] = new(particle.Prefab, particle.Count);

                _particles[particle.Type].Initialize(new ObjectFactory<ParticleSystem>(particle.Prefab), _container, null);
            }


        }
        public ParticleSystem PlayParticlesAt(ParticleType particleID, Vector3 position, Vector3 scale)
        {
            ParticleSystem particles = _particles[particleID].GetItem();

            particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            Transform particlesTransform = particles.transform;
            particlesTransform.position = position; 
            particlesTransform.localScale = scale;

            particles.Play();
            return particles;
        }

    }
}
