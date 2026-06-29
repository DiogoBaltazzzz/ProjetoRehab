using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MemoryGameManager : MonoBehaviour
{
    public string exerciseName = "Memoria";

    [Header("Buttons")]
    public MemoryButton[] buttons;

    [Header("Timing")]
    public float flashTime = 0.8f;
    public float timeBetweenFlashes = 0.4f;
    public float inputCooldown = 0.35f;

    private List<int> sequence = new List<int>();
    private int playerIndex = 0;
    private bool playerTurn = false;
    private bool statsSaved = false;
    private float lastInputTime = -999f;

    private void Start()
    {
        SetupButtons();

        if (ExerciseStatsManager.instance != null)
            ExerciseStatsManager.instance.StartExercise(exerciseName);

        StartGame();
    }

    void SetupButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] != null)
                buttons[i].Setup(this, i);
        }
    }

    void StartGame()
    {
        sequence.Clear();
        playerIndex = 0;
        playerTurn = false;

        if (ExerciseStatsManager.instance != null)
            ExerciseStatsManager.instance.AddAttempt();

        Debug.Log("Novo jogo iniciado.");

        AddNewStep();
        StartCoroutine(ShowSequenceRoutine());
    }

    void AddNewStep()
    {
        int randomButton = Random.Range(0, buttons.Length);
        sequence.Add(randomButton);

        if (ExerciseStatsManager.instance != null)
            ExerciseStatsManager.instance.SetMaxSequence(sequence.Count);

        Debug.Log("Nível " + sequence.Count);
    }

    IEnumerator ShowSequenceRoutine()
    {
        playerTurn = false;

        Debug.Log("Memoriza a sequência...");

        yield return new WaitForSeconds(1f);

        foreach (int index in sequence)
        {
            if (buttons[index] != null)
            {
                buttons[index].flashTime = flashTime;
                buttons[index].FlashSequence();
            }

            yield return new WaitForSeconds(flashTime + timeBetweenFlashes);
        }

        playerIndex = 0;
        playerTurn = true;
        lastInputTime = -999f;

        Debug.Log("Repete a sequência.");
        Debug.Log("Sequência esperada: " + string.Join(", ", sequence));
    }

    public bool PlayerPressedButton(MemoryButton button)
{
    if (!playerTurn)
    {
        Debug.Log("Clique ignorado: ainda não é a vez do jogador.");
        return false;
    }

    if (Time.time - lastInputTime < inputCooldown)
    {
        Debug.Log("Clique ignorado por cooldown.");
        return false;
    }

    lastInputTime = Time.time;

    int pressedIndex = button.GetIndex();
    int expectedIndex = sequence[playerIndex];

    Debug.Log("Jogador carregou: " + pressedIndex + " | Esperado: " + expectedIndex);

    if (pressedIndex == expectedIndex)
    {
        playerIndex++;

        if (playerIndex >= sequence.Count)
            StartCoroutine(HandleRoundSuccess());

        return true;
    }
    else
    {
        StartCoroutine(HandleWrongAnswer());
        return true;
    }
}

    IEnumerator HandleRoundSuccess()
    {
        playerTurn = false;

        Debug.Log("Correto!");

        if (ExerciseAudioManager.instance != null)
            ExerciseAudioManager.instance.PlaySuccess();

        yield return new WaitForSeconds(1f);

        AddNewStep();
        StartCoroutine(ShowSequenceRoutine());
    }

    IEnumerator HandleWrongAnswer()
    {
        playerTurn = false;

        Debug.Log("Errado! A sequência vai recomeçar.");

        if (ExerciseStatsManager.instance != null)
            ExerciseStatsManager.instance.AddError();

        if (ExerciseAudioManager.instance != null)
            ExerciseAudioManager.instance.PlayError();

        yield return new WaitForSeconds(1f);

        StartGame();
    }

    void SaveStats()
    {
        if (statsSaved)
            return;

        statsSaved = true;

        if (ExerciseStatsManager.instance != null)
            ExerciseStatsManager.instance.FinishExercise();
    }

    private void OnDisable()
    {
        SaveStats();
    }

    private void OnApplicationQuit()
    {
        SaveStats();
    }
}