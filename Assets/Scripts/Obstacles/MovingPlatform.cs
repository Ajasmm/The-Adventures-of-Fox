using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] PlatformDirection movementDirection = PlatformDirection.RIGHT;
    [SerializeField] float distance = 5;
    [Range(0.1F, 1F)]
    [SerializeField] float speed = 1;

    float sine, fullRad, tempRad, movementValue;
    Vector2 rawPosition, currentPos;

    CharacterMovement player;
    Rigidbody2D rbody;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        rawPosition = rbody.position;
        currentPos = rawPosition;

        fullRad = Mathf.PI * 2;
    }

    private void FixedUpdate()
    {
        // Calculating the movement of the platform

        tempRad += fullRad * speed * Time.fixedDeltaTime;
        tempRad %= fullRad;

        sine = 0.5F + (Mathf.Sin(tempRad) * 0.5F);
        movementValue = sine * distance;

        if (movementDirection == PlatformDirection.UP)
        {
            currentPos.y = movementValue;
            currentPos.y += rawPosition.y;
        }
        else
        {
            currentPos.x = movementValue;
            currentPos.x += rawPosition.x;
        }

        // adding the moving velocity to the player
        player?.SetPlatformVelocity((currentPos - rbody.position) * (1 / Time.fixedDeltaTime));

        // applying the current position to the rigidbody
        rbody.MovePosition(currentPos);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<CharacterMovement>();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player?.SetPlatformVelocity(Vector3.zero);
            player = null;
        }
    }

    [Serializable]
    private enum PlatformDirection
    {
        UP,
        RIGHT
    }
}
