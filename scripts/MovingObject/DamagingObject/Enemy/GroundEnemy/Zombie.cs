using Godot;
using System;

[GlobalClass]
public partial class Zombie : GroundEnemy
{
	[Export]
	private float _spawnX = 1300f;
	[Export]
	private float _spawnY = 450f;
    private bool _isDashing = false;
    [Export]
	private float _dashForce = 800f;
    private Timer _dashTimer;


    public override void _Ready()
    {
		//метод наследует всю изначальную логику
        base._Ready();

		//поиск узла таймера для рывков к игроку
        _dashTimer = GetNode<Timer>("DashTimer");
        
    }

    //задание точки спавна
    public override Vector2 GetSpawnPosition()
    {
        return new Vector2(_spawnX, _spawnY);
    }

    //переопределение движения врага
    protected override void Move(float delta)
	{
        if (_isDashing)
		{
			Position += new Vector2(-_dashForce * delta, 0);
		} else
		{
			base.Move(delta);
		}
	}

    private void OnTimerTimeout()
	{
		_isDashing = true;
		_dashTimer.Start();
	}

	private void OnDashTimerTimeout()
	{
		_isDashing = false;
	}

}
