using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator animator; // Assign your player's Animator component here
    public Rigidbody2D rb;

    private Vector2 movement;
    private bool isGrounded;
    private bool isAttacking;
    private bool isHurt; // New: to prevent input while hurt
    private bool isDead; // New: to stop all input when dead
    private float attackCooldown = 0.5f; // Cooldown for attacks
    private float nextAttackTime = 0f;
    private float hurtDuration = 0.5f; // How long the player is "hurt" and can't act
    private float hurtEndTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure Rigidbody2D and Animator are assigned, or try to get them
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If dead, stop all input and movement
        if (isDead)
        {
            movement = Vector2.zero; // Stop movement
            animator.SetFloat("Speed", 0); // Ensure idle animation if dead
            return; // Exit Update early
        }

        // If hurt, prevent other actions but allow movement if desired, or stop completely
        if (isHurt)
        {
            if (Time.time >= hurtEndTime)
            {
                isHurt = false; // End hurt state
            }
            // You might want to prevent movement during hurt, or allow it.
            // For now, let's allow movement but prevent other actions.
            // If you want to stop movement too, uncomment the line below:
            // movement = Vector2.zero;
        }

        // --- Movement Input ---
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = 0; // For 2D side-scrollers, vertical input usually isn't direct movement

        // Set animator parameter for horizontal movement
        if (animator != null && !isHurt) // Don't update speed if hurt (or if you want to allow it, remove !isHurt)
        {
            animator.SetFloat("Speed", Mathf.Abs(movement.x));
        }
        else if (isHurt) // If hurt, keep speed at 0 for animation purposes
        {
            animator.SetFloat("Speed", 0);
        }

        // --- Jumping Input ---
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded && !isAttacking && !isHurt)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            if (animator != null)
            {
                animator.SetTrigger("Jump"); // Trigger jump animation
            }
        }

        // --- Attack Input ---
        if (Time.time >= nextAttackTime && !isAttacking && !isHurt)
        {
            if (Input.GetButtonDown("Fire1")) // Default 'Fire1' is left mouse click or left Ctrl
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }

        // Handle player facing direction
        if (movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }
    }

    void FixedUpdate()
    {
        // Only move if not attacking, not hurt, and not dead
        if (!isAttacking && !isHurt && !isDead)
        {
            rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
        }
        else if (isDead)
        {
            rb.linearVelocity = Vector2.zero; // Stop all physics movement
        }
    }

    void Attack()
    {
        isAttacking = true;
        if (animator != null)
        {
            animator.SetTrigger("Attack"); // Trigger attack animation
        }
        // You might want to add a delay or an animation event to reset isAttacking
        Invoke("ResetAttack", attackCooldown * 0.8f); // Reset attack state slightly before cooldown ends

        // --- Add your attack logic here ---
        Debug.Log("Player Attacked!");
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    // --- New Methods for Hurt and Dead states ---
    public void TakeDamage()
    {
        if (isDead || isHurt) return; // Can't take damage if already dead or currently hurt

        isHurt = true;
        hurtEndTime = Time.time + hurtDuration;
        if (animator != null)
        {
            animator.SetTrigger("Hurt"); // Trigger hurt animation
        }
        Debug.Log("Player took damage!");
        // You would typically call a Health script's method here, e.g., healthScript.DecreaseHealth(damageAmount);
    }

    public void Die()
    {
        if (isDead) return; // Already dead

        isDead = true;
        if (animator != null)
        {
            animator.SetTrigger("Dead"); // Trigger dead animation
        }
        // Optionally disable other components like colliders, input, etc.
        // GetComponent<Collider2D>().enabled = false;
        // this.enabled = false; // Disable this script if no further player input is needed
        Debug.Log("Player died!");
    }
}
