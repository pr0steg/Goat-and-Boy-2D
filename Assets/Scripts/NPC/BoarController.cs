using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoarController : MonoBehaviour
{
    [Header("Boar statistics:")]
    public Vector3 movementDirection;
    public float movementSpeed;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator animator;

    Touch touch;
    Vector3 pointPosition;
    bool isMoving = false;

    float previousDistancePos, currentDistancePos;

    void Start()
    {

    }

    void Update()
    {
        if (Time.timeScale == 1.0f)
        {
            Move();
            Animate();
        }
    }

    void Move()
    {
        if (isMoving)
            currentDistancePos = (pointPosition - transform.position).magnitude;

        if (!isMoving)
        {
            previousDistancePos = 0;
            currentDistancePos = 0;
            isMoving = true;
            pointPosition = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
            pointPosition.z = 0;

            if (pointPosition.x <= -6.5f)
            {
                pointPosition.x = -6.4f;
            }
            if (pointPosition.x >= 6.5f)
            {
                pointPosition.x = 6.4f;
            }

            if (pointPosition.y <= -2.5f)
            {
                pointPosition.y = -2.49f;
            }
            if (pointPosition.y >= 3.5f)
            {
                pointPosition.y = 3.49f;
            }

            movementDirection = (pointPosition - transform.position).normalized;
            rb.velocity = new Vector2(movementDirection.x * movementSpeed,
                                        movementDirection.y * movementSpeed);
        }

        if (currentDistancePos > previousDistancePos)
        {
            isMoving = false;
            rb.velocity = Vector2.zero;
        }

        if (isMoving)
            previousDistancePos = (pointPosition - transform.position).magnitude;
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

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Boar")
        {
            Physics2D.IgnoreCollision(collision.collider, this.gameObject.GetComponent<Collider2D>());
        }

        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Scene0");
        }
    }
}