using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    Vector2 attackOffset;
    Collider2D swordCollider;

    private void Start() {
        swordCollider = GetComponent<Collider2D>();
        attackOffset = transform.position;
    }

    public void AttackRight() {
        swordCollider.enabled = true;
        transform.position = attackOffset;
    }

    public void AttackLeft() {
        swordCollider.enabled = true;
        transform.position = new Vector3(attackOffset.x * -1, attackOffset.y);
    }

    public void StopAttack() {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Slime") {
            // memberikan damage pada musuh
        }
    }
}
