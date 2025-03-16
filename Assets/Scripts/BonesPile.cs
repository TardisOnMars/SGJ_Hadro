using System.Collections;
using UnityEngine;

public class BonesPile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DespawnBones());
    }

    IEnumerator DespawnBones()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
