using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : Platforms
{
    public float pushForce = 10f;
    
    public int damage;

    public float delay = 1;

    protected override void Trap()
    {
        Jump();
        base.Trap();
    }

    private void Jump()
    {
        if (playerOnPlatform != null)
        {
            Rigidbody otherRb = playerOnPlatform.gameObject.GetComponent<Rigidbody>();

            if (otherRb != null)
            {
                Vector3 pushDir = (playerOnPlatform.transform.position - transform.position).normalized;
                otherRb.AddForce(pushDir * pushForce, ForceMode.Impulse);
            }
            
            playerOnPlatform.GetDamage(damage);
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        ActivateTrap();
    }

    protected override void OnCollisionStay(Collision other)
    {
        if (other.gameObject.GetComponent<Player>() && playerOnPlatform == null)
        {
            playerOnPlatform = other.gameObject.GetComponent<Player>();
        }

        if (other.gameObject.GetComponent<Player>() && onCooldown == false)
        {
            StartCoroutine(Delay());
        }
    }
    
    
}