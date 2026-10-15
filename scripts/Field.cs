using Godot;
using System;

public partial class Field : Node2D
{
	private Timer _timer;
	private RandomNumberGenerator _random = new RandomNumberGenerator();

	[Export]
	private Godot.Collections.Array<PackedScene> _objects = new Godot.Collections.Array<PackedScene>();


	//генерация рандомный чисел и получение таймера
    public override void _Ready()
    {
        _random.Randomize();
		_timer = GetNode<Timer>("Timer");
	}
	
	//спавн врагов через определённое время
	private void OnSpawnTimerTimeout()
	{
		SpawnEnemy();
	}

	//спавн врагов случайным образом
	private void SpawnEnemy()
	{
		int randomIndex = _random.RandiRange(0, _objects.Count-1);

		//сообщение в консоль, что был заспавнен враг
		GD.Print($"Spawn enemy: {_objects[randomIndex].ResourcePath}");
		MovingObject obj = (MovingObject)_objects[randomIndex].Instantiate();
		
		obj.Position = obj.GetSpawnPosition();
		AddChild(obj);
	}

	private void OnArea2DBodyEntered(Node2D body)
    {
        if (body is Player) 
        {
            GD.Print("Player died!");
			//еслии игрок попадает в край поля - игра начинается сначала
            GetTree().CallDeferred(SceneTree.MethodName.ReloadCurrentScene); 
        }
	}
}
