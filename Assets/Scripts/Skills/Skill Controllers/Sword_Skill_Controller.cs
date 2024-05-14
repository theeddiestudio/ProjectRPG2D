using NUnit.Framework;
using System.Collections.Generic;
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

    public float bounceSpeed;
    public bool isBouncing = true;
    public int amountOfBouncing = 4;
    public List<Transform> bounceEnemyTargets;
    private int targetIndex;

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

        if (isBouncing && bounceEnemyTargets.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, bounceEnemyTargets[targetIndex].position, bounceSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, bounceEnemyTargets[targetIndex].position) < 0.1f)
            {
                targetIndex++;
                amountOfBouncing--;

                if (amountOfBouncing <= 0)
                {
                    isBouncing = false;
                    isReturning = true;
                }

                if (targetIndex >= bounceEnemyTargets.Count)
                    targetIndex = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (isReturning)
            return;

        // bounce check
        if (collision.GetComponent<Enemy>() != null)
        {
            if (isBouncing && bounceEnemyTargets.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                        bounceEnemyTargets.Add(hit.transform);
                }
            }
        }

        StuckIntoObject(collision);
    }

    private void StuckIntoObject(Collider2D collision)
    {

        canRotate = false;

        cd.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (isBouncing && bounceEnemyTargets.Count > 0)
            return;

        anim.SetBool("Rotation", false);
        transform.parent = collision.transform;
    }
}
