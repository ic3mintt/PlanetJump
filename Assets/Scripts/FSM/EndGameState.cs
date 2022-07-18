using UnityEngine;
using UnityEngine.UI;

public class EndGameState : IGameState
{
    private bool buttonPressedDown;
    private Button replayButton;
    private Canvas endGameCanvas;
    
    public void Enter()
    {
        endGameCanvas.gameObject.SetActive(true);
        replayButton = endGameCanvas.GetComponentInChildren<Button>();
        replayButton.onClick.AddListener(OnButtonClick);;
    }

    public void GiveInfo(Canvas canvas)
    {
        endGameCanvas = canvas;
    }

    public void Exit()
    {
        endGameCanvas.gameObject.SetActive(false);
        buttonPressedDown = false;
        replayButton.onClick.RemoveListener(OnButtonClick);
    }

    public IGameState GetNewState()
    {
        return buttonPressedDown ? new PlayState() : null;
    }

    private void OnButtonClick() => buttonPressedDown = true;
}
