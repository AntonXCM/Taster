using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] BaseSounds;
    private Dictionary<string, AudioClip> BaseSoundsDictionary = new Dictionary<string, AudioClip>();

    private AudioSource source;

    void Awake() => ServiceLocator.Register<SoundManager>(this);

    private void Start()
    {
        source = GetComponent<AudioSource>();
        foreach (AudioClip i in BaseSounds) BaseSoundsDictionary.Add(i.name, i);
    }

    public void PlaySound(AudioClip sound) => source.PlayOneShot(sound);

    public void PlayBaseSound(string soundName)
    {
        if (BaseSoundsDictionary.ContainsKey(soundName))
            source.PlayOneShot(BaseSoundsDictionary[soundName]);
        else
            Debug.LogError("There is no sound named \""+ soundName + '"');
    }
}
