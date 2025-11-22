using UnityEngine;
using UnityEngine.UI;

public class SfxController : MonoBehaviour
{
    [Header("Audio resources")]
    [SerializeField] private AudioSource _coin;
    [SerializeField] private AudioSource _explosion;
    [SerializeField] private AudioSource _bonus;
    [SerializeField] private AudioSource _hit;
    [SerializeField] private AudioSource _jump;

    [Header("Slider")]
    [SerializeField] private Slider _sfxSlider;

    private float _volume = 1f;
    private DataSaver _saver;

    public void Init(DataSaver dataSaver)
    {
        ServiceLocator.RegisterService(this);

        _saver = dataSaver;

        float volume = _saver.GetSfxVolume();
        if (volume == -1) _volume = 1f;
        else _volume = volume;

        _coin.volume = _volume;
        _bonus.volume = _volume;
        _hit.volume = _volume;
        _explosion.volume = _volume;
        _jump.volume = _volume;

        _sfxSlider.value = _volume;
    }

    public void Jump() => _jump.Play();

    public void PickUpBonus() => _bonus.Play();

    public void PickUpCoin() => _coin.Play();

    public void Hit() => _hit.Play();

    public void Explosion() => _explosion.Play();

    public void SetVolumeFromSlider()
    {
        _volume = _sfxSlider.value;

        _coin.volume = _volume;
        _bonus.volume = _volume;
        _hit.volume = _volume;
        _explosion.volume = _volume;
        _jump.volume = _volume;

        _saver.SetSfxVolume(_volume);
    }

    public void SetVolume(float volume)
    {
        _volume = volume;

        _coin.volume = _volume;
        _bonus.volume = _volume;
        _hit.volume = _volume;
        _explosion.volume = _volume;
        _jump.volume = _volume;

        _sfxSlider.value = _volume;
        _saver.SetSfxVolume(_volume);
    }
}
