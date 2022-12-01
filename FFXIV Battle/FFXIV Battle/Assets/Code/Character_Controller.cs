using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public bool vulnerable;
    public float defensiveMass;
    public int defense;
    public int health;
    public float walkSpeed;
    public float dashSpeed;
    public Animator animator;
    protected Rigidbody rigidBody;
    public float jumpForceVertical;
    public float jumpForceHorizontal;
    private bool grounded = true;
    protected Vector2 direction;
    private string currentCombo;
    private float comboTimer;
    private bool rootMotion;
    private int initDefense;
    private float savedDir;

    public AudioSource damaged;
    public AudioSource death;

    //Return character to grounded, hitbox off, root motion off


    public virtual void Spawn()
    {
        Debug.Log("Spawn CC");
        //Enemies and players have different spawn functions using this.
    }
    public virtual void Initialize()
    {
        rootMotion = false;
        grounded = true;
        defense = initDefense;
        direction.x = savedDir;
       
    }
    public virtual void ChangeWalkDirection(float x)
    {
        direction.x = x;
    }

   public virtual void TakeDamage(int damage)
    {
        Debug.Log("Took " + damage);
        if (vulnerable) Flinch();
        else Block();
        health -= damage - defense;
        if (health <= 0)
        {
            death.Play();
            Death();
        }
        defense = initDefense;
    }

    public virtual void Block()
    {
        rigidBody.mass = defensiveMass;
        animator.SetTrigger("Block");
    }

    public virtual void Flinch()
    {
        damaged.Play();
        Hitbox hitbox = GetComponentInChildren<Hitbox>();
        if (hitbox != null)
        {
            hitbox.gameObject.SetActive(false);
        }
        animator.SetTrigger("Flinch");
    }

    public virtual void Up()
    {
        Jump();
    }

    public virtual void Down()
    {
        direction.x = 0;
        Duck();
    }

    public virtual void Left()
    {

    }

    public virtual void Right()
    {

    }

    void OnAnimatorMove()
    {
        if (rootMotion)
        {
            animator.ApplyBuiltinRootMotion();
        }
        
    }

    public void DisableRootMotion()
    {
        rootMotion = false;
    }
    public void EnableRootMotionIfGrounded()
    {
        if (IsGrounded())
        {
            rootMotion = true;
        }
    }
    public void EnableRootMotion()
    {
        rootMotion = true;
    }

    public virtual void Combo()
    {

    }

    public virtual void DPad(Vector2 dir)
    {
        if (dir.y > -.5f) { animator.SetBool("Ducking", false); }
        //This input is handled in update, because this input is only run on change to the vector.
        direction = dir;
        savedDir = dir.x;
    }

    public virtual void Walk(float direction)
    {

        if (rootMotion) return;

        animator.SetFloat("Walking", 1);
        Vector3 newPosition = transform.position + (new Vector3(direction, 0, 0) * walkSpeed* Time.deltaTime);
        //rigidBody.MovePosition(newPosition);
        rigidBody.velocity = new Vector3(direction, 0, 0) * walkSpeed;
    }

    public void Flip(float dir)
    {
        
        if (dir == 0 || rootMotion) return;
        ChangeWalkDirection(dir > 0 ? 1 : -1);
        int flipDir = dir > 0 ? 0 : 180;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, flipDir, transform.eulerAngles.z);
       

    }

    private float groundingTimer;

    public virtual void Jump()
    {
        if (grounded)
        {
            rigidBody.velocity = Vector3.zero;
            grounded = false;
            rigidBody.AddForce(new Vector3(jumpForceHorizontal * direction.x, jumpForceVertical, 0), ForceMode.Impulse);
            groundingTimer = .2f;
            animator.SetBool("Grounded", false);
        }
    }

    public bool IsGrounded()
    {
        Collider collider = GetComponent<Collider>();
        if (Physics.Raycast(transform.position - new Vector3(collider.bounds.extents.x, 0, 0), -Vector3.up, collider.bounds.extents.y + 0.1f, LayerMask.GetMask("Default")) ||
            Physics.Raycast(transform.position + new Vector3(collider.bounds.extents.x, 0, 0), -Vector3.up, collider.bounds.extents.y + 0.1f, LayerMask.GetMask("Default")))
        {
            animator.SetBool("Grounded",true);
            return true;
        }
        animator.SetBool("Grounded", false);
        return false;
    }

    public virtual void Death()
    {
        animator.SetTrigger("Death");
    }

    public virtual void Destroy()
    {
        Destroy(this.gameObject);
    }

    public virtual void Duck()
    {
        animator.SetBool("Ducking", true);
    }

    public virtual void FastAttack()
    {
        animator.SetTrigger("FastAttack");
    }

    public virtual void StrongAttack()
    {
        animator.SetTrigger("StrongAttack");
    }

    public virtual void Dash()
    {

        animator.SetTrigger("Dash");
        
    }

    public virtual void Defend()
    {

    }

    public virtual void Special()
    {

    }


    // Start is called before the first frame update
    public virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        //Face the direction you should be facing
        Flip(direction.x);

        //Check grounding, see if you can jump
        comboTimer -= Time.deltaTime;
        groundingTimer -= Time.deltaTime;
        
        if (groundingTimer <= 0 && IsGrounded())
        {
            grounded = true;
            Walk(direction.x);
            animator.SetFloat("Walking", Mathf.Abs(direction.x));
        }
        else
        {
            animator.SetFloat("Walking", 0f);
            grounded = false;
        }
    }
}
