using System.Collections;
using UnityEngine;

public class SnowBehavior : MonoBehaviour
{
    [SerializeField]
    public GameObject snowExplode; // The particle effect for snow impact


    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is NOT tagged as "Controller"
        if (!collision.gameObject.CompareTag("Controller"))
        {
            // Handle the snow impact
            HandleSnowImpact();
        }
    }

    private void HandleSnowImpact()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(snowExplode,transform.position,transform.rotation);
        Destroy(explosion,1f);

    }
      
}
