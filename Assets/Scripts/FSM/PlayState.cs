using UnityEngine;

public class PlayState : IGameState
{
    private Transform player;
    private Camera screen;
    private Canvas playCanvas;
    private GameObjectHandler gameObjectHandler;
    
    public void Enter()
    {
        playCanvas.gameObject.SetActive(true);
        gameObjectHandler.SpawnGameObjects();
        player = gameObjectHandler.Player.transform;
    }
    
    public void GiveInfo(Canvas canvas, Camera camera, GameObjectHandler gameObjectHandler)
    {
        playCanvas = canvas;
        screen = camera;
        this.gameObjectHandler = gameObjectHandler;
    }
    
    public void Exit()
    {
        playCanvas.gameObject.SetActive(false);
        gameObjectHandler.Destroy();
    }

    public IGameState GetNewState()
    {
        
        var playerPosition = screen.WorldToViewportPoint(player.position);
        if (playerPosition.x > 1 || playerPosition.x < 0 || playerPosition.y > 1 || playerPosition.y < 0) 
        {
            return new EndGameState();
        }
        return null;
    }
}
