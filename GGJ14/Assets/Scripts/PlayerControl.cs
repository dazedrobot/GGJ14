using UnityEngine;
using System.Collections;


public enum State
{
	Idle,
	Jump,
	Smash,
	Smashing,
	Pay,
	Disappear,
	Faded
}


public class PlayerControl : MonoBehaviour
{


	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.

    public State state = State.Idle;               // State of the player.


	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.

    
    public bool canJump = true;
    public bool canSmash = true;
    public bool canPay = true;
    public bool canDisappear = true;

	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.
	private Animator anim;					// Reference to the player's animator component.

    
    private Transform hammer;
    private SpriteRenderer[] hammerSprites;
    
    private SpriteRenderer coin;

    private SpriteRenderer[] allSprites;

	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		//anim = GetComponent<Animator>();
        
        hammer = transform.Find ("Hammer");
        hammerSprites = hammer.GetComponentsInChildren<SpriteRenderer> (true);
        allSprites = transform.GetComponentsInChildren<SpriteRenderer> (true);

        coin = transform.Find ("Coin").GetComponent<SpriteRenderer>();
	}


	void Update()
	{
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  

		// If the jump button is pressed and the player is grounded then the player should jump.
		if((state == State.Idle) && grounded)
        {
            if(Input.GetButtonDown("Jump") && canJump)
                state = State.Jump;
            if(Input.GetButtonDown("Smash") && canSmash)
                state = State.Smash;
            if(Input.GetButtonDown("Pay") && canPay)
            {
                state = State.Pay;
                coin.enabled = true;
            }
            if(Input.GetButtonDown("Disappear") && canDisappear)
            {
                state = State.Disappear;
                SetSpritesOpacity(allSprites,0.3f);
            }
        }

        
        if (state == State.Pay && Input.GetButtonUp ("Pay"))
        {
            state = State.Idle;
            coin.enabled = false;
        }

        if (state == State.Disappear && Input.GetButtonUp ("Disappear"))
        {
            state = State.Idle;
            SetSpritesOpacity(allSprites,1);
        }
	}


	void FixedUpdate ()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");

		// The Speed animator parameter is set to the absolute value of the horizontal input.
		//anim.SetFloat("Speed", Mathf.Abs(h));

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(h * rigidbody2D.velocity.x < maxSpeed)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * h * moveForce);

		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);

		// If the input is moving the player right and the player is facing left...
		if(h > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(h < 0 && facingRight)
			// ... flip the player.
			Flip();

		switch(state)
		{
        case State.Jump:
			// Set the Jump animator trigger parameter.
			//anim.SetTrigger("Jump");

			// Play a random jump audio clip.
			//int i = Random.Range(0, jumpClips.Length);
			//AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));

			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			state = State.Idle;
            break;
        case State.Smash:
            StartCoroutine(Smash());
            break;
        case State.Pay:
            break;
		}
	}
    
    public IEnumerator Smash()
    {
        state = State.Smashing;
        hammer.collider2D.enabled = true;
        SetSpritesVisible (hammerSprites, true);
        yield return new WaitForSeconds(0.4f);
        hammer.collider2D.enabled = false;
        SetSpritesVisible (hammerSprites, false);
        state = State.Idle;
    }
    
    void SetSpritesVisible(SpriteRenderer[] sprites, bool visibility)
    {
        foreach (SpriteRenderer sr in sprites)
        {
            sr.enabled = visibility;
        }
    }
    
    void SetSpritesOpacity(SpriteRenderer[] sprites, float opacity)
    {
        foreach (SpriteRenderer sr in sprites)
        {
            Color current = sr.material.color;
            current.a = opacity;
            sr.material.color = current;
        }
    }

	
	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
