using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character_Controller
{
    public override void Death()
    {
        base.Death();
        LevelManager.OnDeath();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void Initialize()
    {
        base.Initialize();
        GetComponent<EnemyAI>().state = EnemyAI.State.idle;
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
