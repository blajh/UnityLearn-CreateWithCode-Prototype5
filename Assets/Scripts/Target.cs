using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    private Rigidbody rb;
    private GameManager gameManager;

    private float forceMin = 12.0f;
    private float forceMax = 15.0f;

    private float torqueRange = 10.0f;

    private float positionRangeX = 4.0f;
    private float positionY = -1.0f;

    public int scoreValue;
    public ParticleSystem particles;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        RandomForce(rb);
        RandomTorque(rb);
        RandomSpawnPos();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomForce(Rigidbody rigidbody)
    {
        rigidbody.AddForce(
            Vector3.up * Random.Range(forceMin, forceMax),
            ForceMode.Impulse);
    }

    void RandomTorque(Rigidbody rigidbody)
    {
        rigidbody.AddTorque(
            Random.Range(-torqueRange, torqueRange),
            Random.Range(-torqueRange, torqueRange),
            Random.Range(-torqueRange, torqueRange),
            ForceMode.Impulse);
    }

    void RandomSpawnPos()
    {
        transform.position = new Vector3(
            Random.Range(-positionRangeX, positionRangeX),
            positionY,
            0);
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Instantiate(particles, transform.position, particles.transform.rotation);
            particles.Play();
            Destroy(gameObject);
            gameManager.UpdateScore(scoreValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }

}
 