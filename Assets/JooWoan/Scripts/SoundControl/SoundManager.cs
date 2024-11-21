using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    public static SoundManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
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
    public static void PlaySFX(string name)
    {
        if (!Instance.sfxDict.ContainsKey(name))
            return;

        Instance.sfxPlayer.PlayOneShot(Instance.sfxDict[name], Instance.sfxVolume);
    }
    public static void PlayBGM(string name)
    {
        if (!Instance.bgmDict.ContainsKey(name))
            return;

        PauseBGM();
        Instance.bgmPlayer.clip = Instance.bgmDict[name];
        Instance.bgmPlayer.volume = Instance.bgmVolume;
        Instance.bgmPlayer.Play();
    }

    public static void PauseBGM()
    {
        if (Instance.bgmPlayer == null)
            return;

        Instance.bgmPlayer.Pause();
    }

    public static void StopBGM()
    {
        if (Instance.bgmPlayer == null)
            return;

        Instance.bgmPlayer.Stop();
    }
    public static void SetVolumeBGM(float volume)
    {
        Instance.bgmVolume = volume;

        if (Instance.bgmPlayer != null)
            Instance.bgmPlayer.volume = Instance.bgmVolume;
    }

    public static void SetVolumeSFX(float volume)
    {
        Instance.sfxVolume = volume;
    }

    public static void SetBgmSpeed(float speed = 1.0f)
    {
        Instance.bgmPlayer.pitch = speed;
    }
}
