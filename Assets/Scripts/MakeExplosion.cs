using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MakeExplosion : MonoBehaviour
{
    public float force, radius;
    [SerializeField]
    ParticleSystem explosion, smoke;
    public List<Rigidbody> bodiesAffected = new List<Rigidbody>();
    public Rigidbody[] bodiesToNotAffect;

    void Start()
    {
        GetComponent<SphereCollider>().radius = radius;
    }

    public IEnumerator Explode()
    {
        explosion.Play();
        smoke.Play();

        yield return 0;

        foreach (Rigidbody rb in bodiesAffected)
        {
            if (!(bodiesToNotAffect.Contains(GetComponentInParent<Rigidbody>()) || rb == GetComponentInParent<Rigidbody>()))
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        yield return new WaitForSeconds(0.2f);

        explosion.Stop();
        smoke.Stop();
    }

    public void Blow(Vector3 pos)
    {
        StartCoroutine(Explode());
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb && !bodiesAffected.Contains(rb))
        {
            bodiesAffected.Add(rb);
        }
    }
}