using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class CutScenePlayer : MonoBehaviour
{
    private VideoPlayer _videoPlayer;

    [SerializeField] private RawImage _image;
    [SerializeField] private EndTrigger _endTrigger;

    [SerializeField] private VideoClip _startClip;
    [SerializeField] private VideoClip _fakeEndingClip;
    [SerializeField] private VideoClip _trueEndingClip;

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.frame = 0;
    }

    void Start()
    {
        _image.enabled = true;
        _videoPlayer.clip = _startClip;
        _videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(_videoPlayer.frame > 0 && (ulong)_videoPlayer.frame >= _videoPlayer.frameCount-1)
        {
            _image.enabled = false;

            if(_videoPlayer.clip == _fakeEndingClip)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        
        if(_endTrigger.IsCollided && _videoPlayer.isPlaying == false)
        {

            _image.enabled = true;

            if (Radio.TrackListenedNumber == Radio.RadioNumber)
            {
                Debug.Log("Истинный конец!");
            }
            else
            {
                _videoPlayer.clip = _fakeEndingClip;
            }
            _videoPlayer.Play();
        }
    }
}
