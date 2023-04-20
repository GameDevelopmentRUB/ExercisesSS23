using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Vector3 moveDirection;

    SpriteRenderer sr;
    Camera mainCam;

    private void Awake() => sr = GetComponent<SpriteRenderer>();

    void Start()
    {
        // Cache camera, because otherwise it'll look for the tag 'MainCamera' every time
        mainCam = Camera.main;

        // HSV = Hue (base color), Saturation (how much of that color), Value (black <-> white)
        sr.color = Random.ColorHSV(0f, 1f, 0f, 1f, 1f, 1f);

        // Time-related functions
        // Invoke(nameof(SwitchDirection), 3f);
        // InvokeRepeating(nameof(SwitchDirection), 1f, 3f);
        StartCoroutine(SwitchDirectionCoroutine());
    }

    void Update()
    {
        // Moves continuously in 'moveDirection'
        transform.position += speed * Time.deltaTime * moveDirection;

        // This calculates the edges of the screen in relative coordinates ('Viewport')
        // (Could also be a Vector2, doesn't really matter in this case, since we don't use z)
        Vector3 bottomLeft = mainCam.ViewportToWorldPoint(Vector3.zero);
        Vector3 topRight = mainCam.ViewportToWorldPoint(Vector3.one);

        // If the object leaves the screen, destroy it
        if (transform.position.x < bottomLeft.x || transform.position.x > topRight.x ||
            transform.position.y < bottomLeft.y || transform.position.y > topRight.y)
            Destroy(gameObject);
    }

    // This is called when no camera is rendering the object - that includes the Scene camera
    // void OnBecameInvisible() => Destroy(gameObject);

    void SwitchDirection()
    {
        // Generates a random direction with equal chance for every direction
        moveDirection = Random.insideUnitCircle.normalized;

        // Atan2 calculates the angle (in radians) out of a directional vector 
        // Atan would only go from -90° to 90°, but Atan2 covers every direction from -180° to 180°
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Flip sprite if moving to the left
        sr.flipY = moveDirection.x < 0;
    }

    // Coroutines can be used for complex behaviour over time. 'yield return' suspends the method
    IEnumerator SwitchDirectionCoroutine()
    {
        // This makes the sprite transparent over a second, then visible over another.
        // yield return null waits until the next frame before continuing from the same point 
        while (sr.color.a > 0f)
        {
            var currentColor = sr.color;
            currentColor.a -= Time.deltaTime;
            sr.color = currentColor;

            yield return null;
        }

        while (sr.color.a < 1f)
        {
            var currentColor = sr.color;
            currentColor.a += Time.deltaTime;
            sr.color = currentColor;

            yield return null;
        }

        // After the previous while-loops have run once, this loop will run indefinitely and switch the moving direction every few seconds
        // (Doesn't lead to an infinite loop because of the yields)
        while (true)
        {
            SwitchDirection();
            yield return new WaitForSeconds(1f);
            SwitchDirection();
            yield return new WaitForSeconds(3f);
            SwitchDirection();
            yield return new WaitForSeconds(0.5f);
        }
    }
}