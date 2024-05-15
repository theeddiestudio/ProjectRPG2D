using UnityEngine;

public class PlayerBlackholeState : PlayerState
{
    private float flyTime = 0.4f;
    private bool isSkillUsed;

    private float defaultGravityScale;
    public PlayerBlackholeState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        defaultGravityScale = rb.gravityScale;

        isSkillUsed = false;
        stateTimer = flyTime;
        rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();

        rb.gravityScale = defaultGravityScale;
        player.MakeTransparent(false);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0)
            rb.velocity = new Vector2(0, 10);
        
        if (stateTimer <= 0)
        {
            rb.velocity = new Vector2(0, -.1f);

            if (!isSkillUsed)
            {
                if (player.skill.blackhole.CanUseSkill())
                    isSkillUsed=true;
            }
        }

        if (player.skill.blackhole.isBlackholeFinished())
            stateMachine.ChangeState(player.airState);

    }
}

