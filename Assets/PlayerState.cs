using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    private string animBoolName;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        Debug.Log("I entered " + this.animBoolName + " State");
    }

    public virtual void Update()
    {
        Debug.Log("I am in " + this.animBoolName + " State");
    }

    public virtual void Exit()
    {
        Debug.Log("I exited " + this.animBoolName + " State");
    }
}
