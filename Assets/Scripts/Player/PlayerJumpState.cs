using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.airState);
        else if (player.IsWallDetected() && rb.velocity.y < .8f * player.jumpForce)
            stateMachine.ChangeState(player.wallSlide);

        player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
    }
}
