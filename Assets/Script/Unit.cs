using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] Vector3 destination;
    [SerializeField] private bool stopped;

    public int Value { get => value; set => this.value = value; }
    public bool Stopped { get => stopped; set => stopped = value; }

    // Start is called before the first frame update
    void Start()
    {
        Value = Random.Range(1, 101);
        gameObject.transform.localScale = new Vector3(1, Value / 100f, 1);
        stopped = true;
    }


    public void SetDestinationUpDown(Vector3 destination)
    {
        stopped = false;
        this.destination = transform.position + destination;
    }
    public void SetDestination(Vector3 destination)
    {
        stopped = false;
        this.destination = destination;
    }




    // Update is called once per frame
    void Update()
    {
        if (!stopped)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, 20 *Time.deltaTime);
            if (transform.position.Equals(destination)) stopped = true;
        }
        
    }
}
