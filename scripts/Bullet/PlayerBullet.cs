using Godot;
using System;

[GlobalClass]
public partial class PlayerBullet : Bullet
{
    private void OnBodyEntered(Node2D area)
	{
		//сообщение в консоль для пояснения, с чем столкнулась пуля
		GD.Print("Player Bullet collided with: " + area.Name);
		if (area is Enemy)
		{
			Enemy enemy = (Enemy)area;
			enemy.TakeDamage(_damage);
			QueueFree();
		}
		else if (area is Obstacle || area is BenignObject)
		{
			QueueFree();
		}
	}
}
