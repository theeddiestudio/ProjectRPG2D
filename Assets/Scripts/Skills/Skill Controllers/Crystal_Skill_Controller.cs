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

    private Transform closestTarget;

    public void SetupCrystal(float _crystalDuration, float _moveSpeed, float _growSpeed, Transform _closestTarget, bool _canMove, bool _canExplode)
    {
        crystalExistTimer = _crystalDuration;
        moveSpeed = _moveSpeed;
        growSpeed = _growSpeed;
        canMove = _canMove;
        canExplode = _canExplode;

        closestTarget = _closestTarget;
    }

    public void ChooseRandomEnemy(LayerMask _whatIsEnemy)
    {
        float radius = SkillManager.manager.blackhole.GetBlackholeRadius();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, _whatIsEnemy);

        if (colliders.Length > 0)
            closestTarget = colliders[Random.Range(0, colliders.Length)].transform;
    }

    private void Update()
    {
        crystalExistTimer -= Time.deltaTime;

        if (crystalExistTimer < 0)
        {
            FinishCrystal();
        }

        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, closestTarget.position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, closestTarget.position) < 1)
            {
                FinishCrystal();
                canMove = false;
            }
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
