using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;

    [SerializeField] private float colorFadeSpeed;
    private float cloneTimer;

    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = 0.8f;

    private Transform closestEnemy;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if (cloneTimer < 0 )
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorFadeSpeed));

            if (sr.color.a <= 0 )
                Destroy(gameObject);
        }
    }

    public void SetupClone(Transform _newTransform, Vector3 _offset, Transform _closestEnemy, float _cloneDuration, bool _canAttack)
    {
        if( _canAttack )
            anim.SetInteger("AttackNumber", Random.Range(1,3));

        transform.position = _newTransform.position + _offset;

        cloneTimer = _cloneDuration;

        closestEnemy = _closestEnemy;
        FaceClosestTarget();
    }

    private void AnimationTrigger()
    {
        cloneTimer = -1; // negative number
    }

    private void AttactTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
                hit.GetComponent<Enemy>().Damage();
        }
    }

    private void FaceClosestTarget()
    {
        if (closestEnemy != null)
        {
            if (transform.position.x > closestEnemy.position.x)
                transform.Rotate(0,180,0);
        }
    }
}
