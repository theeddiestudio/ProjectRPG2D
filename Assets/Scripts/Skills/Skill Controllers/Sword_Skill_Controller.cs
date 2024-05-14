using UnityEngine;
using UnityEngine.WSA;

public class Sword_Skill_Controller : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;

    private bool canRotate = true;
    private bool isReturning;
    [SerializeField] private float swordReturnSpeed = 12;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }

    public void SetupSword(Player _player, Vector2 _dir, float _gravityScale)
    {
        player = _player;
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;

        anim.SetBool("Rotation" , true);
    }

    public void ReturnSword()
    {
        // rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = null;
        isReturning = true;
    }

    private void Update()
    {
        if (canRotate)
            transform.right = rb.velocity;

        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, swordReturnSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) <= 0.5f)
            {
                player.CatchSword();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (isReturning)
            return;

        anim.SetBool("Rotation", false);

        canRotate = false;

        cd.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        transform.parent = collision.transform;
    }
}
