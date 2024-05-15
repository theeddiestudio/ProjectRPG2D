using System.Collections;
using UnityEngine;

public class Player : Entity
{

    public bool isBusy { get; private set; }

    [Header("Move Info")]
    public float moveSpeed = 7.0f;
    public float jumpForce = 15.0f;
    public float swordReturnImpact;

    // [Header("Dash Info")]
    // public float dashSpeed;
    // public float dashDuration;
    // [SerializeField] private float dashCooldown;
    
    // private float dashUsageTimer;
    
    public float dashDir {  get; private set; }

    [Header("Attack Info")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = 0.2f;

    public SkillManager skill {  get; private set; }
    public GameObject sword { get; private set; }

    #region States
    public PlayerStateMachine stateMachine {  get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerCounterAttackState counterAttack { get; private set; }
    
    public PlayerAimSwordState aimSword { get; private set; }
    public PLayerCatchSwordState catchSword { get; private set; }
    public PlayerBlackholeState blackholeState { get; private set; }
    
    #endregion

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");

        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        
        aimSword = new PlayerAimSwordState(this, stateMachine, "AimSword");
        catchSword = new PLayerCatchSwordState(this, stateMachine, "CatchSword");
        blackholeState = new PlayerBlackholeState(this, stateMachine, "Jump");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);

        skill = SkillManager.manager;
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        CheckInputDash();
    }

    public void AssignNewSword(GameObject _newSword)
    {
        sword = _newSword;
    }

    public void CatchSword()
    {
        stateMachine.ChangeState(catchSword);
        Destroy(sword);
    }

    public void ExitBlackhole()
    {
        stateMachine.ChangeState(airState);
    }

    private void CheckInputDash()
    {
        if(isWallDetected())
            return;


        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.manager.dash.CanUseSkill())
        {
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }
}
