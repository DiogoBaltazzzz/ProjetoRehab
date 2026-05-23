using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StatisticsSceneUI : MonoBehaviour
{
    public TextMeshProUGUI exerciseNameText;

    public TextMeshProUGUI timeLabelText;
    public TextMeshProUGUI timeValueText;

    public TextMeshProUGUI attemptsLabelText;
    public TextMeshProUGUI attemptsValueText;

    public TextMeshProUGUI thirdLabelText;
    public TextMeshProUGUI thirdValueText;

    private void Start()
    {
        string lastExercise = PlayerPrefs.GetString("LastExercise", "Sem exercício");

        float time = PlayerPrefs.GetFloat(lastExercise + "_Time", 0f);
        int attempts = PlayerPrefs.GetInt(lastExercise + "_Attempts", 0);
        int hints = PlayerPrefs.GetInt(lastExercise + "_Hints", 0);
        int maxSequence = PlayerPrefs.GetInt(lastExercise + "_MaxSequence", 0);

        exerciseNameText.text = GetReadableExerciseName(lastExercise);

        timeLabelText.text = "Tempo";
        timeValueText.text = time.ToString("F1") + "s";

        attemptsLabelText.text = "Tentativas";
        attemptsValueText.text = attempts.ToString();

        if (lastExercise == "Memoria" || lastExercise == "MemoriaHard")
        {
            thirdLabelText.text = "Recorde";
            thirdValueText.text = maxSequence.ToString();
        }
        else
        {
            thirdLabelText.text = "Dicas";
            thirdValueText.text = hints.ToString();
        }
    }

    string GetReadableExerciseName(string exerciseName)
    {
        switch (exerciseName)
        {
            case "Cafe":
                return "Exercício Café";
            case "Cha":
                return "Exercício Chá";
            case "Memoria":
                return "Jogo da Memória";
            case "MemoriaHard":
                return "Jogo da Memória Difícil";
            default:
                return "Sem exercício realizado";
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}