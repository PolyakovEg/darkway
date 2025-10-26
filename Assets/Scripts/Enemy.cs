using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    private NavMeshAgent agent;
    private float rayMaxDistance = 15f;
    private Transform[] _wayPoints;
    float redirectTimer = 0;
    float redirectFrequency = 20;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        // TODO: переделать говнокод
        Radio[] radios = FindObjectsByType<Radio>(FindObjectsSortMode.None);
        _wayPoints = new Transform[radios.Length];

        for (int i = 0; i < radios.Length; i++)
        {
            _wayPoints[i] = radios[i].transform;
        }
    }

    private void Start()
    {
        SetRandomDestination();
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, _targetTransform.position - transform.position, out RaycastHit hitInfo))
        {
            if(hitInfo.transform.CompareTag("Player"))
            {
                agent.SetDestination(_targetTransform.position);
            }
            else
            {
                redirectTimer += Time.deltaTime;

                if (redirectTimer > redirectFrequency)
                {
                    redirectTimer = 0;
                    SetRandomDestination();
                }
            }
        }
    }

    private void SetRandomDestination()
    {
        agent.SetDestination(_wayPoints[Random.Range(0, _wayPoints.Length)].position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
