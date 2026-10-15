using Godot;
using System;

[GlobalClass]
public partial class Warg : GroundEnemy
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
            //если игрок на земле - варг считывает разницу в их позициях
            if (_player.IsOnFloor())
            {
                float currentDistance = _player.GlobalPosition.X - GlobalPosition.X;

                //в зависимости от того, слева варг или справа - изменяется его направление скорости и отражается спрайт
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

        //изменяем позицию варга
        GlobalPosition = new Vector2(GlobalPosition.X + _velocity.X * delta, _spawnY);
    }

    //задание позиции спавна
    public override Vector2 GetSpawnPosition()
    {
        return new Vector2(_spawnX, _spawnY);
    }
}
