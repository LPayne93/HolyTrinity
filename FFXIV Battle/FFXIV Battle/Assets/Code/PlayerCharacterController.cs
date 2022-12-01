using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacterController : Character_Controller
{

    private GameObject prefabMe;
    public string prefabCharacterToLoad;

    public override void Start()
    {
        base.Start();
        prefabMe = Resources.Load("Prefabs/" + prefabCharacterToLoad) as GameObject;
    }

    public override void Spawn()
    {
        transform.position = GameController.Instance.GetPlayerSpawn();
        if (GetComponentInParent<PlayerController>().playerNumber == 2)
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        }
    }
    public override void FastAttack()
    {
        if (direction.x == 0 && IsGrounded()) animator.SetTrigger("FastAttack");
        else if (direction.x != 0 && IsGrounded()) animator.SetTrigger("FastAttack");
        else if (!IsGrounded()) animator.SetTrigger("FastAttack");
    }

    public void OnDestroy()
    {
        NewLife();
    }

    public virtual void NewLife()
    {
        GameController.Instance.lives -= 1;
        if(GameController.Instance.lives > 0)
        { 
            GameController.Instance.GiveCharacterToPlayer(GetComponentInParent<PlayerController>(), prefabMe);
            
        } else
        {
            Debug.Log("GameOver");
            GameController.Instance.lives = 3;
            GameController.Instance.GameOver();
            
        }
    }
}
