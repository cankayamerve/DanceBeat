using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class UIManager : MonoBehaviour
{
  public static UIManager Instance;
  [SerializeField] private Image image;
  [SerializeField] private TMP_Text instructionTxt;
  [SerializeField] private TMP_Text scoreTxt;
  [SerializeField] private TMP_Text timerTxt;
  [SerializeField] private UnityEngine.UI.Button restartButton;
  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(this);
    }
  }
  public void UpdateSprite(Sprite sprite)
  {
    image.sprite = sprite;
  }

  public void UpdateInstructionMessage(string message)
  {
    instructionTxt.text = message;
  }
  public void UpdateScore(int score) { 
    scoreTxt.text = "SCORE\n"+score.ToString(); 
  }
  public void UpdateTaskTimer(float time)
  {
    int minutes = Mathf.FloorToInt(time / 60);
    int seconds = Mathf.FloorToInt(time % 60);
    timerTxt.text = string.Format("{0}:{1:00}", minutes, seconds);
  }
  public void RestartScene()
  {
    Scene currentScene = SceneManager.GetActiveScene();
    SceneManager.LoadScene(currentScene.name);
  }
  public void EnableRestartButton()
  {
    restartButton.gameObject.SetActive(true);
  }
}
