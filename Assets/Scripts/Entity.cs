using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public int facingDir = 1;
    [SerializeField] protected bool facingRight = true;
    
    [Header("Collision Check")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public EntityFX fx { get; private set; }
    #endregion

    [Header("Knockback Info")]
    [SerializeField] protected Vector2 knockBackDirection;
    [SerializeField] protected float knockBackDuration = 0.07f;
    protected bool isKnocked;

    protected virtual void Awake()
    {

    }

    protected virtual void Update()
    {
        
    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFX>();
    }

    public virtual void Damage()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockBack");
    }

    protected virtual IEnumerator HitKnockBack()
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockBackDirection.x * -facingDir, knockBackDirection.y);

        yield return new WaitForSeconds(knockBackDuration);

        isKnocked = false;
    }

    #region Collision
    public virtual bool isGrounded() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool isWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion
    #region Flip
    public virtual void Flip()
    {      
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }      
           
    public virtual void FlipController(float _input)
    {
        if (_input > 0 && !facingRight)
            Flip();
        else if (_input < 0 && facingRight)
            Flip();
    }
    #endregion
    #region Velocity
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        if (isKnocked)
            return;

        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
    public void SetVelocityZero()
    {
        if (isKnocked)
            return;

        rb.velocity = new Vector2(0, 0);
    } 
    #endregion
}
