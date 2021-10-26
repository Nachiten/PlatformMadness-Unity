using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class PerderNivelManager : MonoBehaviour
{
    private GameManager gameManager;
    private LevelLoader levelLoader;

    private CanvasGroup lostPanel;

    private const float maxAlpha = 0.8f, animationTime = 0.5f, waitTime = 1.5f;

    private void Awake()
    {
        levelLoader = GetComponent<LevelLoader>();
        lostPanel = GameObject.Find("PerdioJuego").GetComponent<CanvasGroup>();
        
        Assert.IsNotNull(levelLoader);
        Assert.IsNotNull(lostPanel);
    }

    private void Start()
    {
        lostPanel.alpha = 0;
        
        gameManager = GameManager.instance;
        Assert.IsNotNull(gameManager);

        gameManager.lostGameEvent += onLostGame;
    }
    
    private void OnDestroy()
    {
        gameManager.lostGameEvent -= onLostGame;
    }

    private void onLostGame()
    {
        LeanTween.value(0, maxAlpha, animationTime).setOnUpdate(updateAlpha).setOnComplete(waitTimeBeforeRestarting);
    }

    private void updateAlpha(float value)
    {
        lostPanel.alpha = value;
    }

    private void waitTimeBeforeRestarting()
    {
        LeanTween.value(0, waitTime, waitTime).setOnComplete(restartLevel);
    }

    private void restartLevel()
    {
        levelLoader.cargarNivel(SceneManager.GetActiveScene().buildIndex);
    }

}
