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

    [SerializeField] private float sfxVolume;
    [SerializeField] private float bgmVolume;

    [SerializeField] private AudioClip[] bgmArr;
    [SerializeField] private SoundConfig[] sfxArr;

    [SerializeField] private AudioSource bgmPlayer, audioSourceTemplate;
    private AudioSource[] sfxSources = new AudioSource[32];

    private IDictionary<string, AudioClip> bgmDict = new Dictionary<string, AudioClip>();
    private IDictionary<string, SoundConfig> sfxDict = new Dictionary<string, SoundConfig>();

    private void Init()
    {
        foreach (AudioClip clip in bgmArr)
            bgmDict.Add(clip.name, clip);

        foreach (SoundConfig config in sfxArr)
            sfxDict.Add(config.Clip.name, config);

        for (int i = 0; i < sfxSources.Length; i++)
            sfxSources[i] = Instantiate(audioSourceTemplate, transform);
    }

    public static void PlaySFX(string name)
    {
        AudioSource audioSource = null;

        foreach (AudioSource source in Instance.sfxSources)
        {
            if (source.isPlaying &&
                source.clip.name == name &&
                source.time <= Instance.sfxDict[name].MinPlaybackInterval)
            {
                return;
            }
            if (audioSource == null && !source.isPlaying)
                audioSource = source;
        }

        if (audioSource == null)
            return;

        if (!Instance.sfxDict.ContainsKey(name))
        {
            Debug.LogWarning($"Failed to find clip named : {name}");
            return;
        }

        audioSource.clip    = Instance.sfxDict[name].Clip;
        audioSource.volume  = Instance.sfxVolume;
        audioSource.Play();
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

[System.Serializable]
public class SoundConfig
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float minPlaybackInterval;

    public AudioClip Clip => audioClip;
    public float MinPlaybackInterval => minPlaybackInterval;
}