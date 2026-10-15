using Godot;
using System;

[GlobalClass]
public partial class Dragon : FlyingEnemy
{
	[Export]
    private float _bulletSpeed = -800f;
    private PackedScene _enemyBulletScene = GD.Load<PackedScene>("res://scenes/EnemyBullet.tscn");

	//враг делает выстрел через равные промежутки времени
	private void OnShootTimerTimeout()
	{
		var bullet = (EnemyBullet)_enemyBulletScene.Instantiate();
		AddSibling(bullet);

		bullet.Position = Position + new Vector2(-50, 0);
        bullet.Velocity = new Vector2(_bulletSpeed, 0);
	}
}
