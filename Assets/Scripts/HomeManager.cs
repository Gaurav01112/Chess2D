using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gameSelectionPanel;

    [SerializeField] private Button playerVsPlayerBtn;

    [Header("Game Selection")] [SerializeField]
    private Button whiteBtn;

    [SerializeField] private Button blackBtn;
    [SerializeField] private Button randomBtn;

    private void Start()
    {
        playerVsPlayerBtn.onClick.AddListener(OnClickPlayerVsPlayerButton);
        
        whiteBtn.onClick.AddListener(OnClickWhiteBtn);
        blackBtn.onClick.AddListener(OnClickBlackBtn);
        randomBtn.onClick.AddListener(OnClickRandomBtn);
    }

    private void OnClickPlayerVsPlayerButton()
    {
        gameSelectionPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    private void OnClickWhiteBtn()
    {
        ChessManager.Instance.SetGameType(ChessManager.GameType.White);
        SceneLoader.LoadScene(SceneLoader.Scene.Game);
    }

    private void OnClickBlackBtn()
    {
        ChessManager.Instance.SetGameType(ChessManager.GameType.Black);
        SceneLoader.LoadScene(SceneLoader.Scene.Game);
    }

    private void OnClickRandomBtn()
    {
        ChessManager.Instance.SetGameType(ChessManager.GameType.Random);
        SceneLoader.LoadScene(SceneLoader.Scene.Game);
    }
}