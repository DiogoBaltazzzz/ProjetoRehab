using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using System.Collections;

public class CoffeeExercise : MonoBehaviour
{
    public enum ExerciseStep
    {
        PegarChavena,
        ColocarNaMaquina,
        PremirBotao,
        Concluido
    }

    public ExerciseStep currentStep = ExerciseStep.PegarChavena;

    public GameObject coffeeStream;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI stepsText;

    public GameObject cupHighlight;
    public GameObject machineHighlight;
    public GameObject buttonHighlight;

    public float hintDelay = 15f;

    private bool cupPlaced = false;
    private bool coffeeServed = false;

    private Coroutine hintCoroutine;

    private void Start()
    {
        if (ExerciseStatsManager.instance != null)
            ExerciseStatsManager.instance.StartExercise("Cafe");

        ShowInstruction("Objetivo: Preparar uma chávena de café.\nPega na chávena.");
        UpdateStepsUI();
        StartHintTimer();
    }

    void ShowInstruction(string message)
    {
        if (instructionText != null)
            instructionText.text = message;

        Debug.Log(message);
    }

    void RegisterError()
    {
        if (ExerciseStatsManager.instance != null)
            ExerciseStatsManager.instance.AddError();
    }

    void RegisterHint()
    {
        if (ExerciseStatsManager.instance != null)
            ExerciseStatsManager.instance.AddHint();
    }

    void UpdateStepsUI()
    {
        if (stepsText == null)
            return;

        string text = "";
        text += GetStepText("Pegar chávena", currentStep > ExerciseStep.PegarChavena);
        text += GetStepText("Colocar na máquina", currentStep > ExerciseStep.ColocarNaMaquina);
        text += GetStepText("Premir botão", currentStep > ExerciseStep.PremirBotao);

        stepsText.text = text;
    }

    string GetStepText(string stepName, bool done)
    {
        if (done)
            return $"<color=green>[OK] {stepName}</color>\n";

        return $"<color=white>[ ] {stepName}</color>\n";
    }

    void DisableAllHighlights()
    {
        if (cupHighlight != null) cupHighlight.SetActive(false);
        if (machineHighlight != null) machineHighlight.SetActive(false);
        if (buttonHighlight != null) buttonHighlight.SetActive(false);
    }

    void StartHintTimer()
    {
        DisableAllHighlights();

        if (hintCoroutine != null)
            StopCoroutine(hintCoroutine);

        if (currentStep != ExerciseStep.Concluido)
            hintCoroutine = StartCoroutine(HintAfterDelay());
    }

    IEnumerator HintAfterDelay()
    {
        yield return new WaitForSeconds(hintDelay);

        switch (currentStep)
        {
            case ExerciseStep.PegarChavena:
                if (cupHighlight != null)
                {
                    cupHighlight.SetActive(true);
                    RegisterHint();
                }
                break;

            case ExerciseStep.ColocarNaMaquina:
                if (machineHighlight != null)
                {
                    machineHighlight.SetActive(true);
                    RegisterHint();
                }
                break;

            case ExerciseStep.PremirBotao:
                if (buttonHighlight != null)
                {
                    buttonHighlight.SetActive(true);
                    RegisterHint();
                }
                break;
        }
    }

    public void OnCupGrabbed()
    {
        if (currentStep == ExerciseStep.PegarChavena)
        {
            currentStep = ExerciseStep.ColocarNaMaquina;
            UpdateStepsUI();
            ShowInstruction("Coloca a chávena na máquina.");
            StartHintTimer();
        }
    }

    public void CupInserted()
    {
        if (currentStep != ExerciseStep.ColocarNaMaquina)
        {
            RegisterError();
            ShowInstruction("Primeiro pega na chávena.");
            return;
        }

        cupPlaced = true;
        currentStep = ExerciseStep.PremirBotao;

        UpdateStepsUI();
        ShowInstruction("Prime o botão para servir café.");
        StartHintTimer();
    }

    public void CupRemoved()
    {
        cupPlaced = false;

        if (!coffeeServed && currentStep != ExerciseStep.Concluido)
        {
            currentStep = ExerciseStep.ColocarNaMaquina;
            UpdateStepsUI();
            ShowInstruction("Volta a colocar a chávena na máquina.");
            StartHintTimer();
        }
    }

    public void PressButton()
    {
        if (coffeeServed)
        {
            RegisterError();
            ShowInstruction("O café já foi servido.");
            return;
        }

        if (currentStep != ExerciseStep.PremirBotao)
        {
            RegisterError();
            ShowInstruction("Ainda não podes premir o botão.");
            return;
        }

        if (!cupPlaced)
        {
            RegisterError();
            ShowInstruction("Coloca a chávena primeiro.");
            return;
        }

        coffeeServed = true;

        if (coffeeStream != null)
            coffeeStream.SetActive(true);

        currentStep = ExerciseStep.Concluido;

        UpdateStepsUI();
        ShowInstruction("Café servido! Exercício concluído.");

        if (ExerciseStatsManager.instance != null)
            ExerciseStatsManager.instance.FinishExercise();

        DisableAllHighlights();

        if (hintCoroutine != null)
            StopCoroutine(hintCoroutine);

        PauseMenuManager pauseMenu = FindObjectOfType<PauseMenuManager>();

        if (pauseMenu != null)
            pauseMenu.ShowPauseMenuAfterFinish();
    }
}