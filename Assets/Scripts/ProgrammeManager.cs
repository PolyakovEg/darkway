using UnityEngine;

public class ProgrammeManager : MonoBehaviour
{
    //TODO: сделать воспроизведение через отдельную функцию
    public AudioClip Jingle;
    public AudioClip Noise;
    //[SerializeField] AudioClip[] Programmes;
    public AudioClip[] audioClips;
    private void Awake()
    {
        //Programmes = null;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
