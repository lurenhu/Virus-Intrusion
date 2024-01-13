using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private AmmoSO ammoSO;
    private Transform targetPosition;
    [SerializeField] private CircleCollider2D circleCollider2D;

    public void InitializeAmmo(AmmoSO ammoSO,Transform targetPosition)
    {
        this.ammoSO = ammoSO;
        this.targetPosition = targetPosition;
    }

    private void Update() {
        Vector2 direction = (targetPosition.position - transform.position).normalized;

        transform.position += (Vector3)(direction * ammoSO.ammoSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Enemy")
        {
            Health health = collision.GetComponent<Health>();
            health.TakeDamage(3);

            Destroy(gameObject);
        }
    }
}
