using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Hitbox that is given data about the effects of the current attack being performed. Runs damage on the attacked and then an explosive force.
public class Hitbox : MonoBehaviour
{
    public AudioSource wiff;
    //Values defaults here can be changed by animations.
    public bool friendly;
    private string tagToAttack;
    public int damage = 1;
    //public float impulseForce = 3;
    //public float upwardForce = 1;
    public Transform impactPoint;

    private void SetDamage(int dmg)
    {
        damage = dmg;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagToAttack)
        {
            Character_Controller cc = other.GetComponentInParent<Character_Controller>();
            cc.DisableRootMotion();
            cc.Flip(impactPoint.position.x - cc.transform.position.x);
            cc.TakeDamage(damage);
            
            //other.GetComponentInParent<Rigidbody>().AddExplosionForce(impulseForce, impactPoint.position, 0, upwardForce, ForceMode.Impulse);
            //ForceMode.Impulse is used so that heavy (high mass) enemies are not easily pushed away.
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        tagToAttack = friendly ? "Enemy" : "Player";
    }

    private void OnEnable()
    {
        wiff.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
