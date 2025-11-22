using UnityEngine;

public class DataSaver 
{
    private const string KEY_SFX_VOLUME = "SFX_VOLUME";
    private const string KEY_MUSIC_VOLUME = "MUSIC_VOLUME";

    public float GetSfxVolume()
    {
        return PlayerPrefs.GetFloat(KEY_SFX_VOLUME, -1);
    }

    public void SetSfxVolume(float value)
    {
        PlayerPrefs.SetFloat(KEY_SFX_VOLUME, value);
    }

    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(KEY_MUSIC_VOLUME, -1);
    }

    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat(KEY_MUSIC_VOLUME, value);
    }


}
