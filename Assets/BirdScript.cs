using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsAlive;
    public float tiltSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        birdIsAlive = true;
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
        {
            myRigidbody.velocity = Vector2.up * flapStrength;

            // Smoothly interpolate the rotation when the bird jumps
            Quaternion targetRotation = Quaternion.Euler(0, 0, 15f); // Adjust the tilt angle as needed
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
        }

        // Check the velocity of the rigidbody
        float verticalVelocity = myRigidbody.velocity.y;

        // Tilt the bird based on its vertical velocity
        float targetTiltAngle = Mathf.Lerp(-45f, 45f, (verticalVelocity + 5) / 10f);

        // Smoothly interpolate the current rotation to the target rotation
        float currentTiltAngle = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, targetTiltAngle, ref tiltSpeed, 0.1f);
        transform.rotation = Quaternion.Euler(0, 0, currentTiltAngle);

        if (Math.Abs(transform.position.y) > 17)
        {
            CallGamerOver();
        }
    }

    private void CallGamerOver()
    {
        Debug.Log("Game is over.");
        logic.GameOver();
        birdIsAlive = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CallGamerOver();
    }
}
