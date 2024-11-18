using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingControl : MonoBehaviour
{
    private PostProcessVolume processVolume;
    private Animator anim;
    private Vignette vignette;

    void Start()
    {
        anim = GetComponent<Animator>();
        processVolume = GetComponent<PostProcessVolume>();
        processVolume.profile.TryGetSettings<Vignette>(out vignette);

        if (!vignette)
        {
            Debug.LogWarning("Vignette is not properly initialized");
            return;
        }
        vignette.enabled.Override(false);

        if (!anim)
            Debug.LogWarning("Post processing animator is not properly initialized");
    }

    public void PlayFeverEffect()
    {
        vignette.enabled.Override(true);
        anim.Play("FeverEffect", -1, 0f);
    }

    public void StopFeverEffect()
    {
        vignette.enabled.Override(false);
        anim.Play("PostProcessing_default", -1, 0f);
    }
}
