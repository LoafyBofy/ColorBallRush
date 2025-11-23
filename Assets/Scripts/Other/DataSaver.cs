using UnityEngine;

public class DataSaver 
{
    private const string KEY_SFX_VOLUME = "SFX_VOLUME";
    private const string KEY_MUSIC_VOLUME = "MUSIC_VOLUME";
    private const string KEY_COINS_AMOUNT = "COINS_AMOUNT";
    private const string KEY_SCORE_AMOUNT = "SCORE_AMOUNT";
    private const string KEY_ACTIVE_SCENE = "ACTIVE_SCENE";

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

    public int GetSceneAccess(string sceneName)
    {
        return PlayerPrefs.GetInt(sceneName, -1);
    }

    public void SetSceneAccess(string sceneName, bool isOpen)
    {
        PlayerPrefs.SetInt(sceneName, isOpen ? 1 : -1);
    }

    public void SetActiveScene(string sceneName)
    {
        PlayerPrefs.SetString(KEY_ACTIVE_SCENE, sceneName);
    }

    public string GetActiveScene()
    {
        return PlayerPrefs.GetString(KEY_ACTIVE_SCENE);
    }

    public int GetCoins()
    {
        return PlayerPrefs.GetInt(KEY_COINS_AMOUNT, -1);
    }

    public void SetCoins(int amount)
    {
        PlayerPrefs.SetInt(KEY_COINS_AMOUNT, amount);
    }

    public int GetScore()
    {
        return PlayerPrefs.GetInt(KEY_SCORE_AMOUNT, -1);
    }

    public void SetScore(int amount)
    {
        PlayerPrefs.SetInt(KEY_SCORE_AMOUNT, amount);
    }
}
