using System.Collections;
using UnityEngine;

public class BonesPile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public ParticleSystem particle;
    void Start()
    {
        ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();
        if (particle != null)
            particle.Play();
        else
            Debug.LogWarning("Particle not found");
        StartCoroutine(DespawnBones());
    }

    IEnumerator DespawnBones()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
