using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorAnim : MonoBehaviour
{
    public void SpawnNext()
    {
        LevelManager.Instance.SpawnWave();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
