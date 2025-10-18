using System;
using UnityEngine;
using UnityEngine.Audio;

public class Radio : MonoBehaviour
{
    const string PlayerTag = "Player";

    public static int TrackNumber = 0;
    public static int TrackListenedNumber = 0;
    public static int RadioNumber = 0;

    private bool _isPlayed = false;
    private ProgrammeManager _programmeManager;
    private AudioSource _audioSource;

    private void Awake()
    {
        TrackNumber = 0;
        TrackListenedNumber = 0;
        RadioNumber = 0;
        _programmeManager = FindFirstObjectByType<ProgrammeManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Debug.Log("Радио " + name + " шуршит");
        RadioNumber++;

        _audioSource.loop = true;
        _audioSource.resource = _programmeManager.Noise;
        _audioSource.Play();
    }

    private void Update()
    {
        if(_audioSource.time >= _audioSource.clip.length)
        {
            if(_audioSource.resource != _programmeManager.Noise && _audioSource.resource != _programmeManager.Jingle)
            {
                _audioSource.resource = _programmeManager.Jingle;
                _audioSource.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag) && _isPlayed == false)
        {
            _audioSource.resource = _programmeManager.audioClips[TrackNumber];
            _audioSource.loop = false;
            _audioSource.Play();
            Debug.Log("Играет программа номер " + TrackNumber);
            TrackNumber++;
            TrackListenedNumber++;
            _isPlayed = true;
        }
    }
}
