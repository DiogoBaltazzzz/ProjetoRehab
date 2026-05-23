using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MemoryGameManager : MonoBehaviour
{
    public string exerciseName = "Memoria";

    public MemoryButton[] buttons;

    public float flashTime = 0.8f;
    public float timeBetweenFlashes = 0.4f;

    private List<int> sequence = new List<int>();
    private int playerIndex = 0;
    private bool playerTurn = false;
    private bool statsSaved = false;

    private void Start()
    {
        if (ExerciseStatsManager.instance != null)
            ExerciseStatsManager.instance.StartExercise(exerciseName);

        StartGame();
    }

    private void OnDisable()
    {
        SaveStats();
    }

    private void OnApplicationQuit()
    {
        SaveStats();
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
            buttons[index].Flash();
            yield return new WaitForSeconds(flashTime + timeBetweenFlashes);
        }

        playerIndex = 0;
        playerTurn = true;

        Debug.Log("Repete a sequência.");
    }

    public void PlayerPressedButton(int buttonIndex)
    {
        if (!playerTurn)
            return;

        Debug.Log("Botão pressionado: " + buttonIndex);

        if (buttonIndex == sequence[playerIndex])
        {
            playerIndex++;

            if (playerIndex >= sequence.Count)
                StartCoroutine(HandleRoundSuccess());
        }
        else
        {
            StartCoroutine(HandleWrongAnswer());
        }
    }

    IEnumerator HandleRoundSuccess()
    {
        playerTurn = false;

        Debug.Log("Correto!");

        yield return new WaitForSeconds(1f);

        AddNewStep();
        StartCoroutine(ShowSequenceRoutine());
    }

    IEnumerator HandleWrongAnswer()
    {
        playerTurn = false;

        Debug.Log("Errado! A sequência vai recomeçar.");

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
}