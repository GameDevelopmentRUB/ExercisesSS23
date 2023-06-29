using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] LayerMask groundMask;
    [SerializeField] float rayLength = 0.55f;
    [SerializeField] float ballSpeed = 10f;
 
    Rigidbody rb;

    void Awake() => rb = GetComponent<Rigidbody>();

    private void Update()
    {
        // Setting velocity directly makes sure that each jump is the same, regardless of current velocity
        if (Input.GetButtonDown("Jump"))
            rb.velocity = new Vector3(rb.velocity.x, 5f, rb.velocity.z);
    }

    private void FixedUpdate()
    {
        rb.AddForce(Input.GetAxis("Horizontal") * ballSpeed, 0f, Input.GetAxis("Vertical") * ballSpeed);

        // The code above will continuously add forces, making the ball faster each time. That's why we should clamp the velocity
        var newVelocity = Vector3.ClampMagnitude(new Vector3(rb.velocity.x, 0f, rb.velocity.z, 5f);
        newVelocity.y = rb.velocity.y;
        rb.velocity = newVelocity;

        // This sends a ray from the center of the ball downwards, so that we can check if the ball is on the ground
        if (Physics.Raycast(transform.position, Vector3.down, rayLength, groundMask))
            Debug.Log("Ground was hit");
    }

    // This will draw the ray into the scene. This is only visible in the Editor and not in the final game
    private void OnDrawGizmos() => Gizmos.DrawRay(transform.position, Vector3.down * rayLength);
}