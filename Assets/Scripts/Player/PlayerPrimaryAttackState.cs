using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;

    private float lastAttackTime;
    private float comboWindow = 0.4f;

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        xInput = 0; // We need this to fix bug on attack direction. [Section - battle system]

        if (comboCounter > 2 || Time.time >= lastAttackTime + comboWindow)
            comboCounter = 0;

        player.anim.SetInteger("ComboCounter", comboCounter);

        float attackDir = player.facingDir;

        if (xInput != 0)
            attackDir = xInput;
            
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);

        stateTimer = 0.1f;
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", 0.15f);

        comboCounter++;
        lastAttackTime = Time.time;
    }

    public override void Update()
    {
        base.Update();

        // rb.velocity = new Vector2(0, 0);
        if (stateTimer < 0)
            player.SetVelocityZero();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
