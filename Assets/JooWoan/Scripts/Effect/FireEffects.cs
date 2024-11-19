using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffects : MonoBehaviour
{
    private List<Transform> fireEffectList = new List<Transform>();

    void Start()
    {
        GameController.Instance.InitFireEffectControl(this);

        foreach (Transform effectTransform in transform)
            fireEffectList.Add(effectTransform);
        
        DisableFireEffect();
    }

    public void EnableFireEffect()
    {
        foreach (Transform effectTransform in fireEffectList)
        {
            ParticleSystem[] particles = effectTransform.GetComponentsInChildren<ParticleSystem>(true);

            foreach (ParticleSystem particle in particles)
                particle.Play();
        }
    }

    public void DisableFireEffect()
    {
        foreach (Transform effectTransform in fireEffectList)
        {
            ParticleSystem[] particles = effectTransform.GetComponentsInChildren<ParticleSystem>(true);

            foreach (ParticleSystem particle in particles)
                particle.Stop();
        }
    }
}
