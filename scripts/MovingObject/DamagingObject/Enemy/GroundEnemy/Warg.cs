using Godot;
using System;

[GlobalClass]
public partial class Warg : GroundEnemy // Предполагаем, что GroundEnemy теперь наследует Area2D
{
    [Export] 
    private Player _player;
    [Export] 
    private float _speed = 500f;
    [Export] 
    private float _spawnX = 1300f;
    [Export] 
    private float _spawnY = 500f;
    
    private Sprite2D _sprite;
    // private Vector2 _velocity = new Vector2(0, 0);

    public override void _Ready()
    {
        base._Ready();

        _player = GetTree().GetFirstNodeInGroup("player") as Player;
        _sprite = GetNode<Sprite2D>("Sprite2D"); 
        
        GlobalPosition = GetSpawnPosition();
    }

    //Переопределение движения варга
    protected override void Move(float delta)
    {
        if (_player != null)
        {
            if (_player.IsOnFloor())
            {
                float currentDistance = _player.GlobalPosition.X - GlobalPosition.X;

                if (currentDistance < 0)
                {
                    _velocity.X = -_speed;
                    _sprite.FlipH = true;
                }
                else
                {
                    _velocity.X = _speed;
                    _sprite.FlipH = false;
                }
            }
        }

        GlobalPosition = new Vector2(GlobalPosition.X + _velocity.X * delta, _spawnY);
    }

    public override Vector2 GetSpawnPosition()
    {
        return new Vector2(_spawnX, _spawnY);
    }
}
