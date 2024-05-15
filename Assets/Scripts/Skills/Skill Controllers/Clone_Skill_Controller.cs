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
    private bool canDuplicateClone;
    private int cloneFacingDir = 1;

    private float chanceOfSpawn;

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

    public void SetupClone(Transform _newTransform, Vector3 _offset, Transform _closestEnemy, float _cloneDuration, bool _canAttack, bool _canDuplicateClone, float _chanceOfSpawn)
    {
        if( _canAttack )
            anim.SetInteger("AttackNumber", Random.Range(1,3));

        transform.position = _newTransform.position + _offset;

        cloneTimer = _cloneDuration;

        closestEnemy = _closestEnemy;
        canDuplicateClone = _canDuplicateClone;
        chanceOfSpawn = _chanceOfSpawn;
        
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
            {
                hit.GetComponent<Enemy>().Damage();

                if (canDuplicateClone)
                {
                    if (Random.Range(0,100) < chanceOfSpawn)
                    {
                        SkillManager.manager.clone.CreateClone(hit.transform, new Vector3(.5f * cloneFacingDir, 0));
                    }
                }
            }
        }
    }

    private void FaceClosestTarget()
    {
        if (closestEnemy != null)
        {
            if (transform.position.x > closestEnemy.position.x)
            {
                transform.Rotate(0,180,0);
                cloneFacingDir = -1;
            }
        }
    }
}
