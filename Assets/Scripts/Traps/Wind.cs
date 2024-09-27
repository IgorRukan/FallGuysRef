using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : Platforms
{
    private Vector3 windDirection;
    public Vector3[] directions = {Vector3.back,Vector3.left,Vector3.right};
    public float windStrength = 5f;
    public bool onPlatform;
    private Rigidbody rb;

    protected override void Trap()
    {
        RandomDirection();
        base.Trap();
    }

    void FixedUpdate()
    {
        if (onPlatform)
        {
            rb.AddForce(windDirection * windStrength * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }

    private void RandomDirection()
    {
        var num = Random.Range(0, 3);
        windDirection = directions[num];
    }
    

    protected override void OnCollisionStay(Collision other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            onPlatform = true;
            rb = other.gameObject.GetComponent<Rigidbody>();
        }

        base.OnCollisionStay(other);
    }

    protected override void OnCollisionExit(Collision other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            onPlatform = false;
            rb = null;
        }
    }
}