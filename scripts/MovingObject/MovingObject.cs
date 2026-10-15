using Godot;
using System;

[GlobalClass]
public partial class MovingObject : Area2D
{
	[Export]
	protected Vector2 _velocity = new Vector2(-300f, 0);
    [Export] 
	protected bool _isSineMovement = false;
    [Export] 
	protected Vector2 _amplitudeRange = new Vector2(50f, 150f);
    [Export] 
	protected float _verticalSpeed = 2f;
    protected float _amplitude;
    protected float _offset;

	//движение объекта по ходу игры
    public override void _Process(double delta)
    {
        Move((float)delta);
    }

	//отслеживание, где находится объект
    public override void _Ready()
    {		
        VisibleOnScreenNotifier2D notifier = new VisibleOnScreenNotifier2D();
		AddChild(notifier);
		notifier.Connect(
			"screen_exited",
			new Callable(
				this, "OnScreenExited"
			)
		);

		if (_isSineMovement)
        {
            _amplitude = (float)GD.RandRange(_amplitudeRange.X, _amplitudeRange.Y);
        }

		Setup();
    }

	//заглушка, которая реализовывается в дочерних классах
	protected virtual void Setup()
	{}

	//удаление объекта при выходе за пределы экрана
	private async void OnScreenExited()
	{
		Timer timer = new Timer();
		timer.WaitTime = 1f;

		timer.OneShot = true;
		AddChild(timer);
		timer.Start();
		await ToSignal(timer, "timeout");

		//вывод в консоль информации о том, что объект покинул игровое поле
		GD.Print($"{Name} has exited the scene.");
		QueueFree();
	}

	//определение позиции для спавна
	public virtual Vector2 GetSpawnPosition()
	{
		return new Vector2(0, 0);
	}

	//перемещение объекта 
	protected virtual void Move(float delta)
	{
		//объект движется либо по прямой, либо по синусоиде
		if (_isSineMovement)
        {
            _offset += _verticalSpeed * delta;
            Position += new Vector2(
                _velocity.X * delta,
                Mathf.Sin(_offset) * _amplitude * delta
            );
        }
        else
        {
            GlobalPosition += _velocity * delta;
        }
	}
}
