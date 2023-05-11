using UnityEngine;

public class KirbyController : MonoBehaviour
{
    [SerializeField] Transform spawnPosition;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float speed = 10f;

    // This will show a dropdown menu in the inspector where you can choose one or multiple layers
    [SerializeField] LayerMask collisionLayers;

    Rigidbody2D rb;
    SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Linear drag is kind of like air resistance and will make the object fall down slower
        // More drag means that the object will also need more force to be moved
        rb.drag = 5f;

        // To prevent a rigidbody from rotating, you can click on the checkbox in the Component or set this value
        rb.freezeRotation = false;
        
        // This sets the layer the GameObject is on. You can set which layers collide with each other in the Layer Collision Matrix
        gameobject.layer = 3;
        // To avoid using the integer, you can also use this instead. Of course the layer needs to be created first
        gameobject.layer = LayerMask.NameToLayer("IgnoreObstacles");

        // On newer versions of Unity this sets the layers that should be ignored:
        // rb.excludeLayers = collisionLayers;
    }

    // FixedUpdate is called at fixed timesteps instead of frames (default: 0.02s = 50fps)
    private void FixedUpdate()
    {
        // For continuous input you can use FixedUpdate
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(speed * Vector2.left);
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(speed * Vector2.right);
    }

    // Between FixedUpdate and Update is where the actual physics update happens

    void Update()
    {
        // There might be Update steps that don't have a FixedUpdate, which is why we put code like GetKeyDown 
        // in Update - if you press the button when there's no FixedUpdate, the call will be ignored
        if (Input.GetKeyDown(KeyCode.Space))
            // This code overwrites the current velocity on the y-axis while keeping the x-velocity, which
            // is physically inaccurate, but feels right in a lot games (especially when you need a jump)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    // This will be called on every physical (!) collision. Be sure to use the 2D version and not 'OnCollisionEnter', which is for 3D
    private void OnCollisionEnter2D(Collision2D collision)
    {  
        sr.color = Random.ColorHSV(0f, 1f, 0f, 1f, 1f, 1f);
    }

    // This will be called every time Kirby hits a collider that has 'Is Trigger' checked
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // A tag is used to differentiate between different objects. When 'Shredder' is hit, 
        // Kirby should be destroyed. Otherwise a copy of this Kirby should spawn
        if (collision.CompareTag("Shredder"))
            Destroy(gameObject);
        else
            Instantiate(gameObject, spawnPosition.position, Quaternion.identity);

        // Alternatively, you could put a script on different objects, like Shredder.cs or Spawner.cs
        // and let them handle instantiation and destruction, e.g. like this:
        // OnTriggerEnter2D(Collider2D collision) => Destroy(collision.gameObject);
    }
}
