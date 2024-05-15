using UnityEngine;

public class Crystal_Skill_Controller : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();
    private CircleCollider2D cd => GetComponent<CircleCollider2D>();

    private float crystalExistTimer;
    private bool canMove;
    private bool canExplode;
    private bool canGrow;
    private float moveSpeed;
    private float growSpeed;

    public void SetupCrystal(float _crystalDuration, float _moveSpeed, float _growSpeed, bool _canMove, bool _canExplode)
    {
        crystalExistTimer = _crystalDuration;
        moveSpeed = _moveSpeed;
        growSpeed = _growSpeed;
        canMove = _canMove;
        canExplode = _canExplode;
    }

    private void Update()
    {
        crystalExistTimer -= Time.deltaTime;

        if (crystalExistTimer < 0)
        {
            FinishCrystal();
        }

        if (canGrow)
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(5, 5), Time.deltaTime * growSpeed);
    }

    private void AnimationExplodeTriggers()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cd.radius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
                hit.GetComponent<Enemy>().Damage();
        }
    }

    public void FinishCrystal()
    {
        if (canExplode)
        {
            canGrow = true;
            anim.SetTrigger("Explode");
        }
        else
            SelfDestroy();
    }

    public void SelfDestroy() => Destroy(gameObject);
}
