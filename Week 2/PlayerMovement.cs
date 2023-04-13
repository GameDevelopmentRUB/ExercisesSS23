using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // This is the default sprite. We are using a different variable, so that the idle sprite doesn't have to be part of the walk cycle
    [SerializeField] Sprite idleSprite;

    // This array holds the walk cycle
    [SerializeField] Sprite[] sprites;

    // Time between sprite changes
    [SerializeField] float animationTime = 1f;

    // Speed of the character
    [SerializeField] float speed = 5f;

    SpriteRenderer sr;

    int spriteIndex = 0; 
    float timer;

	// Cache the SpriteRenderer (could also be done in Start())
    void Awake() => sr = GetComponent<SpriteRenderer>();

    void Update()
    {
        Vector3 moveVector = Vector3.zero;

        // Get input and save state in moveVector
        if (Input.GetKey(KeyCode.W)) moveVector.y = 1;
        if (Input.GetKey(KeyCode.A)) moveVector.x = -1;
        if (Input.GetKey(KeyCode.S)) moveVector.y = -1;
        if (Input.GetKey(KeyCode.D)) moveVector.x = 1;

        // Normalize vector, so that magnitude for diagonal movement is also 1
        moveVector.Normalize();

        // Frame rate independent movement
        transform.position += Time.deltaTime * speed * moveVector;

        // Flip the sprite if facing to the left
        if (moveVector.x < 0)
            sr.flipX = true;
        else if (moveVector.x > 0)
            sr.flipX = false;

        // Check if the character is moving
        if (moveVector != Vector3.zero)
        {
            // This will count time in real seconds. So after 1 second timer will be at 1
            timer += Time.deltaTime;

            // This will be called when our timer reaches the specified time (and the array contains sprites)
            if (timer >= animationTime && sprites.Length > 0)
            {
                // Load the next sprite and loop around when end of the array is reached
                spriteIndex = (spriteIndex + 1) % sprites.Length;
                sr.sprite = sprites[spriteIndex];

                // Reset the timer. Otherwise it'll continue going up and (timer >= animationTime) will be true in every single frame
                timer = 0f;
            }
        }
        else
        {
            // Reset the sprite to idle if the character is not moving
            sr.sprite = idleSprite;
			spriteIndex = 0;
			timer = 0f;
        } 
    }
}
