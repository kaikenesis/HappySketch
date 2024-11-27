using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class FeverText : MonoBehaviour
{
    [SerializeField] private GameObject feverTextObject;
    private RectTransform frontTextTransform;
    private TextMeshProUGUI frontText;
    private float scaleRange = 2.0f;
    private float value;
    [SerializeField] private float speed = 0.02f;
    private bool bPlay = false;
    private Vector3 defaultScale;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        feverTextObject.SetActive(false);
        frontTextTransform = feverTextObject.GetComponent<RectTransform>();
        defaultScale = frontTextTransform.localScale;
        frontText = feverTextObject.GetComponent<TextMeshProUGUI>();
    }

    public void Activate()
    {
        if (bPlay == true) return;

        feverTextObject.SetActive(true);
        StartCoroutine(PlayTextAnimation());
        bPlay = true;
        value = 0;
        frontTextTransform.transform.localScale = defaultScale;
    }

    public void Deactivate()
    {
        feverTextObject.SetActive(false);
        StopCoroutine(PlayTextAnimation());
        bPlay = false;
    }

    private void ResetInfo()
    {
        value = 0;
        frontTextTransform.transform.localScale = defaultScale;
    }

    IEnumerator PlayTextAnimation()
    {
        while(true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            value += speed;
            Mathf.Clamp(value, 0.0f, 1.0f);
            frontText.color = new Color(frontText.color.r, frontText.color.g, frontText.color.b, 1.0f - value);
            frontTextTransform.transform.localScale = new Vector3(defaultScale.x + value/3, defaultScale.y + value/3, frontTextTransform.transform.localScale.z);

            if (value >= 1.0f)
                ResetInfo();
        }
    }
}
