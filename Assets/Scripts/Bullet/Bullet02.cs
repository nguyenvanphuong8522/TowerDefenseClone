using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet02 : Bullet
{
    public Rigidbody rb;

    public override void SetTarget(Vector3 direction)
    {
        if(!isSetted)
        {
            rb.velocity = direction * speed;
            isSetted = true;
        }
        
    }
    private void Update()
    {
        if (Vector3.Distance(pointStart, transform.position) > 2)
        {
            ObjectPool.instance.Return(gameObject);
        }
    }
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(atk, 2);
            HitTarget();
        }
    }
}
