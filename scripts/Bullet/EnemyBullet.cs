using Godot;
using System;

[GlobalClass]
public partial class EnemyBullet : Bullet
{
	private void OnBodyEntered(Node2D area)
	{
		//сообщение в консоль для пояснения, с чем столкнулась пуля
		GD.Print("Enemy Bullet collided with: " + area.Name);
		if (area is Player)
		{
			Player player = (Player)area;
			GetTree().CallDeferred(SceneTree.MethodName.ReloadCurrentScene); 
		}
		else if (area is Obstacle || area is StaticBody2D)
		{
			QueueFree();
		}
	}
}
