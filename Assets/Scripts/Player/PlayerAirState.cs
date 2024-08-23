public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlide);
        else if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);

        float xVelocity = xInput * player.moveSpeed * .8f;

        if (rb.velocity.y != 0 && xInput == 0)
            xVelocity = player.facingDir * player.moveSpeed * .8f;

        player.SetVelocity(xVelocity, rb.velocity.y);
    }
}
