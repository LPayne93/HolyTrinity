using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State { idle, wander, rush, attack};
    public State state;
    private Enemy enemyController;
    public Character_Controller target;
    private float walkingDirection;
    private float stateTimer;
    public float fastAttackRange;
    public float strongAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponent<Enemy>();
    }

    public void OnEnable()
    {
        ChooseTarget();
    }

    private void ChooseTarget()
    {
        PlayerCharacterController[] players = FindObjectsOfType<PlayerCharacterController>();
        if (players.Length < 1) return;
        target = players[Random.Range(0, players.Length)];
    }

    public void CheckForAttack()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < fastAttackRange)
        {
            state = State.attack;
            enemyController.FastAttack();
        } else if (Vector3.Distance(transform.position, target.transform.position) < strongAttackRange)
        {
            state = State.attack;
            FaceTarget();
            enemyController.StrongAttack();
        }
    }

    //Usually works, TODO: Find out why it doesn't *always* work. 
    public void FaceTarget()
    {
        float playerDir = target.transform.position.x > transform.position.x ? 1 : -1;
        enemyController.ChangeWalkDirection(playerDir);
        enemyController.Flip(playerDir);
    }


    public void Wander(float direction)
    {
        enemyController.Flip(direction);
        enemyController.ChangeWalkDirection(direction);
    }

    public void ChangeState(State passedState)
    {
        stateTimer = Random.Range(.5f, 3);
        state = passedState;
        walkingDirection = Random.Range(-1, 2) > 0 ? 1f : -1f;
    }

    public void RandomState()
    {
        int s = Random.Range(0, 4);
        switch (s)
        {
            case 0:
                state = State.idle;
                break;
            case 1:
                state = State.wander;
                break;
            case 2:
                state = State.rush;
                break;
            case 3:
                state = State.attack;
                break;

        }
    }

    public void RandomAttack()
    {
        int attack = Random.Range(0,2);
        switch (attack)
        {
            case 0:
                Debug.Log("Weak");
                enemyController.FastAttack();
                break;
            case 1:
                Debug.Log("Strong");
                enemyController.StrongAttack();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        stateTimer -= Time.deltaTime;
        if(target == null)
        {
            ChooseTarget();
            return;
        }

        //All actions are performed in functions named outside of the switch state.
        //All variables relating to states are initialized within "ChangeState."
        
        switch (state)
        {
            case State.idle:
                CheckForAttack();
                if (stateTimer <= 0) { ChangeState(State.wander); }
                break;
            case State.wander:
                CheckForAttack();
                if (stateTimer <= 0) { ChangeState(State.idle); }
                Wander(walkingDirection);
                break;
            case State.attack:
                break;
        }
    }
}
