using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControll : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollision = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        // jika movement input bukan 0, mencoba untuk bergerak
        if (movementInput != Vector2.zero) {
            // periksa potensi tabrakan
            int count = rb.Cast (
                movementInput,  // Nilai X dan Y antara -1 dan 1 yang merepresentasikan arah dari objek yang akan dicari tabrakannya.
                movementFilter, // pengaturan yang menentukan di mana tabrakan dapat terjadi seperti layer yang akan ditabrakkan.
                castCollision,  // daftar tabrakan untuk menyimpan tabrakan yang ditemukan setelah Cast selesai.
                moveSpeed * Time.fixedDeltaTime + collisionOffset   // jumlah yang akan di-cast sama dengan gerakan ditambah offset
            );

            if (count == 0) {
                rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
            }
        }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }
}
