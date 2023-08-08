using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControll : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;

    Vector2 movementInput;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;
    List<RaycastHit2D> castCollision = new List<RaycastHit2D>();

    bool canMove = true;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        if (canMove) {
            // jika movement input bukan 0, mencoba untuk bergerak
            if (movementInput != Vector2.zero) {
                bool success = TryMove(movementInput);

                if (!success) {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }

                if (!success) {
                    success = TryMove(new Vector2(0, movementInput.y));
                }

                animator.SetBool("isMoving", success);
            } else {
                animator.SetBool("isMoving", false);
            }

            // mengatur arah sprite ke arah gerakan
            if (movementInput.x < 0) {
                spriteRenderer.flipX = true;
            } 
            else if (movementInput.x > 0) {
                spriteRenderer.flipX = false;
            }
        }
    }

    private bool TryMove(Vector2 direction) {
        if (direction != Vector2.zero) {
            // periksa potensi tabrakan
            int count = rb.Cast (
                direction,  // Nilai X dan Y antara -1 dan 1 yang merepresentasikan arah dari objek yang akan dicari tabrakannya.
                movementFilter, // pengaturan yang menentukan di mana tabrakan dapat terjadi seperti layer yang akan ditabrakkan.
                castCollision,  // daftar tabrakan untuk menyimpan tabrakan yang ditemukan setelah Cast selesai.
                moveSpeed * Time.fixedDeltaTime + collisionOffset   // jumlah yang akan di-cast sama dengan gerakan ditambah offset
            );

            if (count == 0) {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            } 
            else {
                return false;
            }
        }
        else {
            return false;
        }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire() {
        animator.SetTrigger("attack");
    }

    public void SwordAttack() {
        LockMovement();
        if (spriteRenderer.flipX) {
            swordAttack.AttackLeft();    
        } else {
            swordAttack.AttackRight();
        }
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement() {
        canMove = true;
    }
}
