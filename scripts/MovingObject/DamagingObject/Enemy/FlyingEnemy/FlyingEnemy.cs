using Godot;
using System;

[GlobalClass]
public partial class FlyingEnemy : Enemy
{
    [Export]
	private float _spawnX = 1300f;
	[Export]
	private Vector2 _spawnYRange = new Vector2(100f, 400f);

    //определение точки спавна (воздушные враги спавнятся на случайной высоте)
	public override Vector2 GetSpawnPosition()
	{
		return new Godot.Vector2(
			_spawnX,
			(float)GD.RandRange(
				_spawnYRange.X,
				_spawnYRange.Y
			)
		);
	}
}
