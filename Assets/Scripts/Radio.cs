using System;
using UnityEngine;

public class Radio : MonoBehaviour
{
    const string PlayerTag = "Player";

    public static int _trackNumber = 0;

    private bool _isPlayed = false;

    private void Start()
    {
        Debug.Log("Радио " + name + " шуршит");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag) && _isPlayed == false)
        {
            Debug.Log("Играет программа номер " + _trackNumber++);
            _isPlayed = true;
        }
    }
}
