using Godot;
using System;

[GlobalClass]
public partial class Player : CharacterBody2D
{
    [Export]
    private float _gravity = 980f;
    [Export]
    private float _moveSpeed = 300f;
    [Export]
    private float _jumpVelocity = -800f;
    [Export]
    private float _minJumpVelocity = -300f;
    [Export]
    private float _bulletSpeed = 500f;
    public bool IsDead {get; set; } = false; 
    private Vector2 _velocity;
    private PackedScene _bulletScene = GD.Load<PackedScene>("res://scenes/PlayerBullet.tscn");

    //реализация прыжков игрока
    public override void _PhysicsProcess(double delta)
    {
        if (IsDead) return; 

        _velocity = Velocity;

        if (!IsOnFloor())
        {
            _velocity.Y += _gravity * (float)delta;

        }

        float direction = Input.GetAxis("move_left", "move_right");
        _velocity.X = direction * _moveSpeed;

        if (IsOnFloor() && Input.IsActionJustPressed("jump"))
        {
            _velocity.Y = _jumpVelocity;
        }

        if (Input.IsActionJustReleased("jump") && _velocity.Y < _minJumpVelocity)
        {
            _velocity.Y = _minJumpVelocity;
        }

        Velocity = _velocity;
        MoveAndSlide();
    }

    //обработка ввода
    public override void _Input(InputEvent @event)
    {
        //выстрел по ЛКМ
        if (@event.IsActionPressed("shoot"))
        {
            Shoot();
        }
        //выход игры по Escape
        if (@event.IsActionPressed("exit"))
        {
            GetTree().Quit();
        }
    }

    //реализация стельбы
    private void Shoot()
    {
        //сообщение в консоль, что игрок стреляет
        GD.Print("Shooting");

        //инстанцирование пули
        PlayerBullet bullet = (PlayerBullet)_bulletScene.Instantiate();
        AddSibling(bullet);

        //задание позиции пули
        bullet.Position = Position + new Vector2(50, 0);
        bullet.Velocity = new Vector2(_bulletSpeed, 0);
    }
}
