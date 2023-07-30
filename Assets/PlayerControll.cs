using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    Vector2 movementInput;
    // Start is called before the first frame update
    void Start() {
        
    }

    private void FixedUpdate() {
        if (movementInput != Vector2.zero) {}
        {
            
        }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }
}
