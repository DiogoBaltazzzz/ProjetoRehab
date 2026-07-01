using UnityEngine;

public class ExerciseStatsManager : MonoBehaviour
{
    public static ExerciseStatsManager instance;

    private float startTime;
    private int errors;
    private int hintsUsed;
    private int attempts;
    private int maxSequence;

    private string currentExercise;
    private bool exerciseRunning = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartExercise(string exerciseName)
    {
        currentExercise = exerciseName;

        PlayerPrefs.SetString("LastExercise", exerciseName);
        PlayerPrefs.Save();

        startTime = Time.time;
        errors = 0;
        hintsUsed = 0;
        attempts = 0;
        maxSequence = 0;
        exerciseRunning = true;

        Debug.Log("Exercício iniciado: " + currentExercise);
    }

    public void AddError()
    {
        errors++;
    }

    public void AddHint()
    {
        hintsUsed++;
    }

    public void AddAttempt()
    {
        attempts++;
    }

    public void SetMaxSequence(int value)
    {
        if (value > maxSequence)
            maxSequence = value;
    }

    public void FinishExercise()
    {
        if (string.IsNullOrEmpty(currentExercise))
        {
            Debug.LogWarning("Nenhum exercício ativo para guardar estatísticas.");
            return;
        }

        if (!exerciseRunning)
        {
            Debug.Log("Estatísticas já tinham sido guardadas para: " + currentExercise);
            return;
        }

        float totalTime = Time.time - startTime;

        PlayerPrefs.SetString("LastExercise", currentExercise);
        PlayerPrefs.SetFloat(currentExercise + "_Time", totalTime);
        PlayerPrefs.SetInt(currentExercise + "_Errors", errors);
        PlayerPrefs.SetInt(currentExercise + "_Hints", hintsUsed);
        PlayerPrefs.SetInt(currentExercise + "_Attempts", attempts);
        PlayerPrefs.SetInt(currentExercise + "_MaxSequence", maxSequence);

        PlayerPrefs.Save();

        exerciseRunning = false;

        Debug.Log("Exercício concluído/guardado: " + currentExercise);
        Debug.Log("Tempo: " + totalTime);
        Debug.Log("Erros: " + errors);
        Debug.Log("Dicas: " + hintsUsed);
        Debug.Log("Tentativas: " + attempts);
        Debug.Log("Recorde sequência: " + maxSequence);
    }
}