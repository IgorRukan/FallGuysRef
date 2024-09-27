using System.Collections;
using UnityEngine;

public class Bomb : Platforms
{
    public int delay;
    public Renderer rend;
    public float damage;
    private Material def;
    public Material orange;
    public Material red;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        def = rend.material;
    }

    protected override void Trap()
    {
        rend.material = orange;
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        rend.material = red;
        DealDamage();
    }

    private void DealDamage()
    {
        if (playerOnPlatform != null)
        {
            playerOnPlatform.GetDamage(damage);
        }

        StartCoroutine(RedDelay());
    }

    IEnumerator RedDelay()
    {
        yield return new WaitForSeconds(0.5f);
        rend.material = def;
        StartCoroutine(TrapCooldown());
    }
}
