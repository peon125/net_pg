using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour 
{
    [SerializeField]
    MakeExplosion explosion;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.impulse.magnitude > 45)
        {
            explosion.gameObject.SetActive(true);
            explosion.Blow(transform.position);

            explosion.transform.parent = null;
            Destroy(gameObject);
        }
    }
}