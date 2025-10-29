using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    public string FakeEndScene;
    public string TrueEndScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Radio.TrackListenedNumber == Radio.RadioNumber)
            {
                SceneManager.LoadScene(TrueEndScene);
            }
            else
            {
                SceneManager.LoadScene(FakeEndScene);
            }
        }
    }
}
