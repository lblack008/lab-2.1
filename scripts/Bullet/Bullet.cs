using Godot;
using System;

[GlobalClass]
public partial class Bullet : CharacterBody2D
{
	[Export]
	protected float _damage = 10f;

	//обработка движения пули
	public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
	}

	//удаление пули при выходе за границы экрана
	private void OnScreenLeave()
	{
		QueueFree();
	}

	//сеттер для изменения урона пули
	public void SetDamage(float damage)
	{
		_damage = damage;
	}
}
