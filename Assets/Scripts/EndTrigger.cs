using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public bool IsCollided { get; private set; }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            IsCollided = true;
        }
    }
}
