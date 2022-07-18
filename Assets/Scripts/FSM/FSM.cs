using UnityEngine;

public class FSM : MonoBehaviour
{
    private IGameState currentState;
    
    [SerializeField] private Camera camera;
    [SerializeField] private Canvas playCanvas;
    [SerializeField] private Canvas endGameCanvas;
    [SerializeField] private GameObjectHandler gameObjectHandler;

    private void Start()
    {
        var state = new PlayState();
        state.GiveInfo(playCanvas, camera, gameObjectHandler);
        state.Enter();
        currentState = state;
    }

    private void Update()
    {
        var nextState = currentState.GetNewState();
        if(nextState != null)
        {
            currentState.Exit();
            if (nextState is EndGameState)
            {
                ((EndGameState)nextState).GiveInfo(endGameCanvas);
            }
            else
            {
                ((PlayState) nextState).GiveInfo(playCanvas,  camera, gameObjectHandler);
            }
            currentState = nextState;
            currentState.Enter();
        }
    }
}
