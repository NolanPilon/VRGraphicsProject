using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.AI;

public class NotPokemonBrain : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;
    public float safety;

    private float timer = 0;
    private Vector3 startScale;
    private bool caught = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        startScale = transform.localScale;

    }

    void Run(Vector3 location)
    {
        Vector3 fleeVector = location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeVector);
    }
    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Evade()
    {
        Vector3 targetDir = target.transform.position - this.transform.position;
        float lookAhead = targetDir.magnitude / (agent.speed + 10);
        Run(target.transform.position + target.transform.forward * lookAhead);
    }

    Vector3 wanderTarget = Vector3.zero;
    void Wander()
    {
        float wanderRadius = 10;
        float wanderDistance = 10;
        float wanderJitter = 1;

        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter,
                                        0,
                                        Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        Seek(targetWorld);
    }

    float calculateDistance()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        return distance;
    }

    void shrink()
    {
        //how long it takes to shrink
        timer += Time.deltaTime / 2f;

        //what does the shrinking
        Vector3 newScale = Vector3.Lerp(startScale, startScale * 0.2f, timer);
        transform.localScale = newScale;

        //end of timer
        if(timer > 1)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!caught)
        {
            if(calculateDistance() <= safety)
             {
                 Evade();
             }
             else
             {
                 Wander();
             }
        }
        else
        {
            shrink();
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
          
            caught = true;
        }
    }
}