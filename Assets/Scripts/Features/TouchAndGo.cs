using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TouchAndGo : MonoBehaviour {

    [Header("Player statistics:")]
    public Vector3 movementDirection;
    public float movementSpeed;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator animator;

    Touch touch;
	Vector3 touchPosition;
	bool isMoving = false;

	float previousDistanceToTouchPos, currentDistanceToTouchPos;

	void Start () {
		
	}
	
	void Update () {
        Move();
        Animate();
	}

    void Move()
    {
        if (isMoving)
            currentDistanceToTouchPos = (touchPosition - transform.position).magnitude;

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                previousDistanceToTouchPos = 0;
                currentDistanceToTouchPos = 0;
                isMoving = true;
                touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;
                movementDirection = (touchPosition - transform.position).normalized;
                rb.velocity = new Vector2(movementDirection.x * movementSpeed,
                                            movementDirection.y * movementSpeed);
            }
        }

        if (currentDistanceToTouchPos > previousDistanceToTouchPos)
        {
            isMoving = false;
            rb.velocity = Vector2.zero;
        }

        if (isMoving)
            previousDistanceToTouchPos = (touchPosition - transform.position).magnitude;
    }

    void Animate()
    {
        if (movementDirection != Vector3.zero && isMoving == true)
        {
            animator.SetFloat("Horizontal", movementDirection.x);
            animator.SetFloat("Vertical", movementDirection.y);
            animator.SetFloat("Speed", movementSpeed);
        }
        else
        {
            animator.SetFloat("Speed", 0.0f);
        }
    }
}
