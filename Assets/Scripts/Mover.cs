using UnityEngine;
using UnityEngine.InputSystem; 
///Otherwise I cannot code with the Keyboard.current

/// <summary>
/// GETXR 2026 - Assignment 1: Basic Movement.
/// Use on the 'Player' GameObject. Complete the TODOs so the object strafes
/// smoothly on two world-space axes based on keyboard input — no rotation.
/// Use Keyboard.current, not the deprecated Input.GetAxis.
/// </summary>

public class Mover : MonoBehaviour
{
    [Header("Movement Settings")]
    // Done: expose this field in the Inspector - changed private to public
    public float moveSpeed = 5.0f;

    public float sprintSpeed = 10.0f;

    // Done: add your own field for a running speed — a direct,
    // Inspector-exposed value, used below while Shift
    // is held.

    void Start()
    {
        Debug.Log($"[Mover] Initialized on {gameObject.name}. MoveSpeed is set to {moveSpeed}.");
    }

    void Update()
    {
        // Done: read A/D and W/S via Keyboard.current as -1/0/+1 each.
        float x = 0f;
        float z = 0f;

        if (Keyboard.current.dKey.isPressed) {x += 1f;}
        if (Keyboard.current.aKey.isPressed) {x -= 1f;}
        if (Keyboard.current.wKey.isPressed) {z += 1f;}
        if (Keyboard.current.sKey.isPressed) {z -= 1f;}

        ///Input.GetAxis works like a gas pedal in a car.

        // Done: combine into a Vector3 (X, 0, Z), normalized if diagonal.
        Vector3 direction = new Vector3(x, 0, z);
        direction = direction.normalized; ///Otherwise, movement diagonally would be faster.

        // Straight strafe: A/D = left/right, W/S = forward/back. Don't rotate
        // the object to turn — that's a tank-control scheme, not a strafe.

        // Done: pick moveSpeed or your new running-speed field depending on whether Keyboard.current.leftShiftKey is held.
        float currentSpeed = moveSpeed;
        if (Keyboard.current.leftShiftKey.isPressed)
        {
            currentSpeed = sprintSpeed;
        }

        // Done: apply Time.deltaTime and transform.Translate to move the object.
        transform.Translate(direction * currentSpeed * Time.deltaTime, Space.World); 
        ///Time.deltaTime - game would run faster on better hardware otherwise.
        ///It does units every single frame, not per second.
        ///Space.World - moves an object by vector (3 terms multiplied) in world-space
        ///(not in its own local axes).
        
    }
}
