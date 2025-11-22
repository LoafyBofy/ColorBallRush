using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicController : PausedMonoBehaviour
{
    [SerializeField] private List<AudioResource> _musics = new();
    [SerializeField] private float _risingTime = 2.5f;
    [SerializeField] private float _fadeTime = 3f;

    [Header("Slider")]
    [SerializeField] private Slider _musicSlider;

    private AudioSource _currentTrack;
    private float _risingStep = 0f;
    private float _fadeStep = 0f;
    private float _volume = 1f;
    private DataSaver _saver;

    public void Init(DataSaver saver)
    {
        _saver = saver;
        _currentTrack = GetComponent<AudioSource>();

        float volume = _saver.GetMusicVolume();
        if (volume == -1) _volume = 1f;
        else _volume = volume;

        _musicSlider.value = _volume;

        _risingStep = CalculateStep(_risingTime);
        _fadeStep = CalculateStep(_fadeTime);

        _currentTrack.resource = ChangeTrack();
        _currentTrack.Play();
        _currentTrack.volume = 0;
    }

    private void Update()
    {
        if (_currentTrack == null) return;

        if (_currentTrack.isPlaying == false)
        {
            _currentTrack.resource = ChangeTrack();
            _currentTrack.Play();
            _currentTrack.volume = 0;
        }

        if (_currentTrack.isPlaying)
        {
            if (_currentTrack.time < _risingTime)
            {
                _currentTrack.volume += _risingStep * Time.deltaTime;
            }
            else if (_currentTrack.time > _currentTrack.clip.length - _fadeTime)
            {
                _currentTrack.volume -= _fadeStep * Time.deltaTime;
            }
        }
    }

    public void PauseTack(bool isPaused)
    {
        if (isPaused) 
            _currentTrack.Pause();
        else
            _currentTrack.UnPause();
    }

    public void SetVolumeFromSlider()
    {
        if (_musicSlider != null)
        {
            _volume = _musicSlider.value;
            _currentTrack.volume = _volume;
            _saver.SetMusicVolume(_volume);
        }
    }

    public void SetVolume(float volume)
    {
        _volume = volume;
        _currentTrack.volume = _volume;
    }

    private float CalculateStep(float time)
    {
        if (_volume > 0 && time > 0)
            return _volume / time;
        else
            return 0;
    }

    private AudioResource ChangeTrack()
    {
        var newTrack = _musics.GetRandom();

        while(newTrack == _currentTrack)
        {
            newTrack = _musics.GetRandom();
        }

        return newTrack;
    }
}
