using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    private NavMeshAgent agent;
    private float rayMaxDistance = 15f;
    private Transform[] _wayPoints;
    float _redirectTimer = 0;
    float _redirectFrequency = 30;
    private int _stopFrequencyMin = 10;
    private int _stopFrequencyMax = 40;

    [SerializeField] private float _walkSpeed = 3;
    [SerializeField] private float _runSpeed = 0.2f;


    private float _idleTime = 0;
    private float _idleTimeMax = 5;
    private float _idleTimeReload = 8;

    private float _runTime = 0;
    private float _runTimeMax = 5f;
    private float _runTimeReload = 8;

    private States _state = new States();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        /*// TODO: переделать говнокод
        Radio[] radios = FindObjectsByType<Radio>(FindObjectsSortMode.None);
        _wayPoints = new Transform[radios.Length];

        for (int i = 0; i < radios.Length; i++)
        {
            _wayPoints[i] = radios[i].transform;
        }*/
    }

    private void Start()
    {
        Invoke("Stop", Random.Range(_stopFrequencyMin, _stopFrequencyMax));
        agent.SetDestination(_targetTransform.position);
    }

    void Update()
    {
        if(_state == States.Idle)
        {
            _idleTime += Time.deltaTime;

            if(_idleTime >= _idleTimeMax)
            {
                _state = States.Walk;
            }
        }

        switch (_state)
        {
            case States.Idle:
                agent.speed = 0;
                _runTime = 0;
                break;

            case States.Walk:
                agent.speed = _walkSpeed;
                _runTime = 0;
                break;

            case States.Run:
                if (_runTime < _runTimeMax)
                {
                    agent.speed = _runSpeed;
                }
                else
                {
                    agent.speed = _walkSpeed;
                }
                break;
        }

        if (Physics.Raycast(transform.position, _targetTransform.position - transform.position, out RaycastHit hitInfo))
        {
            if(hitInfo.transform.CompareTag("Player") )
            {
                agent.SetDestination(_targetTransform.position);
                _state = States.Run;
            }
            else
            {
                _state = States.Walk;
                agent.speed = _walkSpeed;

                _redirectTimer += Time.deltaTime;

                if (_redirectTimer > _redirectFrequency)
                {
                    _redirectTimer = 0;
                    //SetRandomDestination();
                    agent.SetDestination(_targetTransform.position);
                }

                _runTime = 0;
            }
        }

        _runTime += Time.deltaTime;
        if (_runTime > _runTimeMax + _runTimeReload)
            _runTime = 0;
    }

    private void SetRandomDestination()
    {
        //agent.SetDestination(_wayPoints[Random.Range(0, _wayPoints.Length)].position);
    }

    private void Stop()
    {
        //agent.SetDestination(transform.position);
        _state = States.Idle;
        _idleTime = 0;
        Invoke("Stop", Random.Range(_stopFrequencyMin, _stopFrequencyMax));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    enum States
    {
        Idle,
        Walk,
        Run
    }
}
