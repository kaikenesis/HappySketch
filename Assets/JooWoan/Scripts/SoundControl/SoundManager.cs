using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    public static SoundManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
        Init();
    }
    #endregion

    [SerializeField] private float sfxVolume = 0.4f;
    [SerializeField] private float bgmVolume = 0.4f;
    [SerializeField] private AudioClip[] bgmArr, sfxArr;
    [SerializeField] private AudioSource bgmPlayer, sfxPlayer;

    IDictionary<string, AudioClip> bgmDict = new Dictionary<string, AudioClip>();
    IDictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();

    private void Init()
    {
        foreach (AudioClip clip in bgmArr)
            bgmDict.Add(clip.name, clip);

        foreach (AudioClip clip in sfxArr)
            sfxDict.Add(clip.name, clip);
    }
    public void PlaySFX(string name)
    {
        if (!sfxDict.ContainsKey(name))
            return;

        sfxPlayer.PlayOneShot(sfxDict[name], sfxVolume);
    }
    public void PlayBGM(string name)
    {
        if (!bgmDict.ContainsKey(name))
            return;

        PauseBGM();
        bgmPlayer.clip = bgmDict[name];
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.Play();
    }

    public void PauseBGM()
    {
        if (bgmPlayer == null)
            return;

        bgmPlayer.Pause();
    }

    public void StopBGM()
    {
        if (bgmPlayer == null)
            return;

        bgmPlayer.Stop();
    }
    public void SetVolumeBGM(float volume)
    {
        bgmVolume = volume;

        if (bgmPlayer != null)
            bgmPlayer.volume = bgmVolume;
    }

    public void SetVolumeSFX(float volume)
    {
        sfxVolume = volume;
    }
}
