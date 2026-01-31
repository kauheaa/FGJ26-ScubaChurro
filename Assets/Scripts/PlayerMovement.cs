using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 10f;
    public float laneDistance = 3f;
    public float laneChangeSpeed = 10f;

    [Header("Jump & Slide")]
    public float jumpForce = 7f;
    public float gravity = -20f;
    public float slideDuration = 1f;

    private CharacterController controller;
    private Vector3 velocity;

    private int currentLane = 1;
    private float targetX = 0f;
    private bool isSliding = false;

    private Vector2 touchStart;
    private Vector2 touchEnd;
    private float minSwipeDistance = 50f;

    private Color originalColor;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        targetX = (currentLane - 1) * laneDistance;

        // Tallennetaan alkuperäinen väri
        if (TryGetComponent<Renderer>(out Renderer rend))
            originalColor = rend.material.color;
    }

    void Update()
    {
        HandleInput();
        MoveForward();
        MoveSideways();
        ApplyGravity();
    }

    private void MoveForward()
    {
        Vector3 forwardMove = Vector3.forward * forwardSpeed;
        controller.Move(forwardMove * Time.deltaTime);
    }

    private void MoveSideways()
    {
        float newX = Mathf.Lerp(transform.position.x, targetX, laneChangeSpeed * Time.deltaTime);
        Vector3 move = new Vector3(newX - transform.position.x, 0, 0);
        controller.Move(move);
    }

    private void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -1f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (controller.isGrounded)
            velocity.y = jumpForce;
    }

    private void Slide()
    {
        if (!isSliding)
            StartCoroutine(SlideCoroutine());
    }

    private IEnumerator SlideCoroutine()
    {
        isSliding = true;

        float originalHeight = controller.height;
        Vector3 originalCenter = controller.center;

        controller.height = originalHeight / 2f;
        controller.center = new Vector3(0, originalCenter.y / 2f, 0);

        yield return new WaitForSeconds(slideDuration);

        controller.height = originalHeight;
        controller.center = originalCenter;

        isSliding = false;
    }

    private void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, 0, 2);
        targetX = (currentLane - 1) * laneDistance;
    }

    private IEnumerator FlashColor(Color flashColor)
    {
        if (TryGetComponent<Renderer>(out Renderer rend))
        {
            rend.material.color = flashColor;
            yield return new WaitForSeconds(0.2f);
            rend.material.color = originalColor;
        }
    }

    private void HandleInput()
    {
        // Editor nuolilla
        if (Input.GetKeyDown(KeyCode.LeftArrow)) ChangeLane(-1);
        if (Input.GetKeyDown(KeyCode.RightArrow)) ChangeLane(1);
        if (Input.GetKeyDown(KeyCode.UpArrow)) Jump();
        if (Input.GetKeyDown(KeyCode.DownArrow)) Slide();

        // Mobiili swipe
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                touchStart = touch.position;
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                touchEnd = touch.position;
                Vector2 swipe = touchEnd - touchStart;

                if (swipe.magnitude >= minSwipeDistance)
                {
                    if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
                    {
                        // Oikea / vasen
                        if (swipe.x > 0)
                        {
                            ChangeLane(1);
                            StartCoroutine(FlashColor(Color.blue));
                        }
                        else
                        {
                            ChangeLane(-1);
                            StartCoroutine(FlashColor(Color.red));
                        }
                    }
                    else
                    {
                        // Ylös / alas
                        if (swipe.y > 0) Jump();
                        else Slide();
                    }
                }
            }
        }
    }
}
