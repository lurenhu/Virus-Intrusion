using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyInArea : MonoBehaviour
{
    private Player player;

    private void Awake() {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Enemy")
        {
            Debug.Log("Enemy in");
            player.enemiesInArea.Enqueue(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Enemy")
        {
            Debug.Log("Enemy out");
            player.enemiesInArea.Dequeue();
        }
    }
}
