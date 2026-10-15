using Godot;
using System;

[GlobalClass]
public partial class DamagingObject : MovingObject
{
    private Player _player;
    //в начале игры добавляется инофрмация, что игрок находится на поле
	public override void _Ready()
    {		
		BodyEntered += OnBodyEntered;
    
		base._Ready();
        // Находим игрока один раз при старте игры по его группе
        _player = GetTree().GetFirstNodeInGroup("player") as Player;
    }
	
	//обработка столкновения с игроком
	private void OnBodyEntered(Node2D body)
    {
        if (body is Player)
        {
            // Если другой враг уже убил игрока в этот же миг — ничего не делаем
            if (_player.IsDead) return;

            // Меняем статус игрока на "мёртв". Теперь другие враги сюда не пройдут!
            _player.IsDead = true;

			//вывод в консоль информации о том, с каким объектом столкнулся игрок
            GD.Print($"Player hit by {Name}!");
            //CallDeferred обрабатывает ошибку, возникающую при обычном вызове QueueFree()
			//ReloadCurrentScene прописан для того, чтобы после смерти игрока раннер запускался снова. Так он будет бесконечным
            GetTree().CallDeferred(SceneTree.MethodName.ReloadCurrentScene); 
        }
    }
}
