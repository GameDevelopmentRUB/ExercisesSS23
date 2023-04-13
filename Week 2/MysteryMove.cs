using UnityEngine;

public class MysteryMove : MonoBehaviour
{
    float speed = 5f;
    float timer = 1f;
    SpriteRenderer sr;
    
    // Awake is called once before Start()
    void Awake() => sr = GetComponent<SpriteRenderer>();

    void Update()
    {
        // This code moves the object step by step
        if (Input.GetKeyDown(KeyCode.Q)) transform.position += new Vector3(0, 1);
        if (Input.GetKeyDown(KeyCode.E)) transform.position += new Vector3(0, -1);

	// This code moves it continously. Holding I and U at the same time will move it right, since the else-block will not be called
        if (Input.GetKey(KeyCode.I))
            transform.position += Time.deltaTime * speed * new Vector3(1, 0);
        else if (Input.GetKey(KeyCode.U))
            transform.position += Time.deltaTime * speed * new Vector3(-1, 0);


	// This timer will reach 0 after 0.5 seconds
        timer -= Time.deltaTime * 2f;

        if (timer <= 0f) 
        {
            sr.flipX = !sr.flipX;
            timer = 1f;
            sr.color = Color.white; // Color.white will set a sprite to its original color
        }

        if (sr.flipX)  
        {
            Color newColor = sr.color;
	    // The following line is not frame rate independent. To fix this, calculate newColor.a -= Time.deltaTime instead
            newColor.a -= 0.0667f; // .a is the alpha (0 = transparent, 1 = opaque)
            sr.color = newColor;
        }
    }
}
