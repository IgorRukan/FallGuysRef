using System;
using System.Collections;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public int trapCooldown;
    protected bool onCooldown;
    protected Player playerOnPlatform;

    protected void ActivateTrap()
    {
        if (!onCooldown)
        {
            onCooldown = true;
            Trap();
        }
    }

    protected virtual void Trap()
    {
        StartCoroutine(TrapCooldown());
    }

    protected IEnumerator TrapCooldown()
    {
        yield return new WaitForSeconds(trapCooldown);
        onCooldown = false;
    }

    protected virtual void OnCollisionStay(Collision other)
    {
        if (other.gameObject.GetComponent<Player>() && playerOnPlatform == null)
        {
            playerOnPlatform = other.gameObject.GetComponent<Player>();
        }

        if (other.gameObject.GetComponent<Player>() && onCooldown == false)
        {
            ActivateTrap();
        }
    }

    protected virtual void OnCollisionExit(Collision other)
    {
        if (other.gameObject.GetComponent<Player>() && playerOnPlatform != null)
        {
            playerOnPlatform = null;
        }
    }
}