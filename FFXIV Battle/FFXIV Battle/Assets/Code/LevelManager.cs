using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;
    public int enemiesInWave;
    public Transform[] spawnPoints;
    private int i = 1;
    [SerializeField]
    public GameObject[] enemyWaves;
    public int nextWave;
    public Animator anim;

    private int en;
    private int x;
    private int nW;

    // Start is called before the first frame update

    public void Reset()
    {
        Debug.Log("Resetting." + en + i + nW);
        enemiesInWave = en;
        i = x;
        nextWave = nW;

        Debug.Log("Reset." + enemiesInWave + i + nextWave);
    }

    public void Start()
    {
       en = enemiesInWave;
       x = i;
       nW = nextWave;
    }

    public static void OnDeath()
    {
        Instance.RemoveAnEnemy();
    }

    public void RemoveAnEnemy()
    {
        enemiesInWave -= 1;
    }

    public void NextPhase()
    {
        if (nextWave >= enemyWaves.Length && anim != null)
        {
            if (!anim.GetBool("GameOver")){
                GameController.Instance.GameEnd();
                anim.SetBool("GameOver", true); }
            return;
        }

        enemiesInWave = enemyWaves[nextWave].GetComponentsInChildren<Enemy>().Length;
        
        anim.SetTrigger("Elevate");
    }

    public void SpawnWave()
    {
        if (nextWave >= enemyWaves.Length) return;
        enemyWaves[nextWave].SetActive(true);
        nextWave++;

    }

    public Vector3 NextSpawn()
    {
        Vector3 returnedVector = spawnPoints[i - 1].position;
        if (i == spawnPoints.Length) i = 1;
        else i++;
        return returnedVector;
    }

    private void OnEnable()
    {
        PlayerCharacterController[] players = FindObjectsOfType<PlayerCharacterController>();
        foreach (PlayerCharacterController player in players)
        {
            player.Spawn();
        }
        Instance = this;
    }



    // Update is called once per frame
    void Update()
    {
        if(enemiesInWave == 0)
        {
            NextPhase();
        }
    }
}
