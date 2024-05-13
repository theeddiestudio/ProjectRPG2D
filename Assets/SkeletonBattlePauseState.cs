using UnityEngine;

public class SkeletonBattlePauseState : SkeletonBattleState
{
    public SkeletonBattlePauseState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetVelocityZero();
        Debug.Log("Enter");

        stateTimer = 5;
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit");
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            Debug.Log("Change here");

        Debug.Log("Update");
        Debug.Log(stateTimer);
    }
}

// flip for sometime using statetimer and change to battle if player detected again.
