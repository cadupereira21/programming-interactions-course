using System.Linq;
using UnityEngine;

public class ExplosiveShot : Shot {

    [SerializeField] private GameObject explosionVfx;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private float upwardModifier = 1f;
    
    private void OnCollisionEnter(Collision other) {
        Vector3 contactPoint = other.contacts[0].point;

        GameObject explosionInstance = Instantiate(explosionVfx, this.transform.position, this.transform.rotation);
        
        Collider[] objectsInRange = Physics.OverlapSphere(contactPoint, explosionRadius);

        foreach (Collider col in objectsInRange) {
            if (col.CompareTag("Target") && col.TryGetComponent(out Rigidbody rb)) {
                rb.AddExplosionForce(explosionForce, contactPoint, explosionRadius, upwardModifier);
            }
        }
        
        Destroy(this.gameObject);
    }
}