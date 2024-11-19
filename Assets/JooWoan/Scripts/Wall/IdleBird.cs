using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBird : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AnimationClip idleClip;
    [SerializeField] private GameObject mainObject;
    [SerializeField] private float spawnChance = 50.0f;

    void OnEnable()
    {
        float randomChance = Random.Range(0.0f, 100.0f);
        if (randomChance > spawnChance)
        {
            mainObject.SetActive(false);
            return;
        }
        anim.Play(idleClip.name, -1, 0f);
    }

    void OnDisable()
    {
        mainObject.SetActive(true);
    }
}
