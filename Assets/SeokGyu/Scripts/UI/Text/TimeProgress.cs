using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeProgress : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Image BarImg;
    [SerializeField] private Image feverImg;

    private void Start()
    {
        SetInfo();
    }

    private void SetInfo()
    {
        timeText.text = NoteManager.Instance.noteTimeInfo.PlayTime.ToString("F0") + "초";
        BarImg.material = defaultMaterial;
        feverImg.enabled = false;
    }

    private void SetText()
    {
        float time = NoteManager.Instance.noteTimeInfo.PlayTime - NoteManager.instance.CurTime;
        if (UIManager.Instance.bPlayGame == false) return;
        if (time < 0) return;

        timeText.text = time.ToString("F0") + "초";
    }

    private void SetSlider()
    {
        progressSlider.value = 1.0f - (NoteManager.Instance.CurTime / NoteManager.Instance.noteTimeInfo.PlayTime);
        if (progressSlider.value <= 0.0f)
        {
            BarImg.enabled = false;
            feverImg.enabled = false;
        }
    }
    
    private void SetFeverColor()
    {
        // Mask처리를 위해서 본래 progress바의 Material을 null로 초기화하고, mask를 씌울 rainbowImage를 활성화시킴
        BarImg.material = null;
        feverImg.enabled = true;
    }

    private void SetDefaultColor()
    {
        BarImg.material = defaultMaterial;
        BarImg.enabled = true;
        feverImg.enabled = false;
    }

    public void UpdateProgress()
    {
        SetText();
        SetSlider();
    }

    public void SetProgressColor(bool isFever)
    {
        if (isFever == true) SetFeverColor();
        else SetDefaultColor();
    }

    public void ResetProgress()
    {
        timeText.text = NoteManager.Instance.noteTimeInfo.PlayTime.ToString("F0") + "초";
        progressSlider.value = 1.0f;
        SetDefaultColor();
    }
}
