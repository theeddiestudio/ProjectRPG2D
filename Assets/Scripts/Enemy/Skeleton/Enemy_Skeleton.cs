using UnityEngine;

public class Enemy_Skeleton : Enemy
{

    public float sneakUpDistance;

    #region States
    public SkeletonIdleState idleState {  get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletonAggressiveBattleState battleAggressiveState { get; private set; }
    public SkeletonBattlePauseState battlePauseState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
        moveState = new SkeletonMoveState(this, stateMachine, "Move", this);
        battleAggressiveState = new SkeletonAggressiveBattleState(this, stateMachine, "Move", this);
        battlePauseState = new SkeletonBattlePauseState(this, stateMachine,"Idle", this);
        attackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
