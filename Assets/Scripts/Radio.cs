using System;
using UnityEngine;

public class Radio : MonoBehaviour
{
    const string PlayerTag = "Player";

    public static int TrackNumber = 0;
    public static int TrackListenedNumber = 0;
    public static int RadioNumber = 0;

    private bool _isPlayed = false;

    private void Awake()
    {
        TrackNumber = 0;
        TrackListenedNumber = 0;
        RadioNumber = 0;
    }

    private void Start()
    {
        Debug.Log("Радио " + name + " шуршит");
        RadioNumber++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag) && _isPlayed == false)
        {
            Debug.Log("Играет программа номер " + TrackNumber++);
            TrackListenedNumber++;
            _isPlayed = true;
        }
    }
}
