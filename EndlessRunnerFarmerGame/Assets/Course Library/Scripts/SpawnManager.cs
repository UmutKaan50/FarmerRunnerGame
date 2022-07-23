using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2;
    private float repeatRate;

    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        repeatRate = Random.Range(1.8f, 3f);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            repeatRate -= 1.5f;
                
        }
    }
    void SpawnObstacle()
    {
        int randomValue = Random.Range(0, obstaclePrefab.Length);
        if(playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab[randomValue], obstaclePrefab[randomValue].transform.position, obstaclePrefab[randomValue].transform.rotation);
        }
        
        
    }
}

