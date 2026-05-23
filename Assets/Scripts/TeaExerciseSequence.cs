using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;

public class TeaExerciseSequence : MonoBehaviour
{
    public enum ExerciseStep
    {
        ColocarChavena,
        AdicionarAgua,
        AdicionarCha,
        AdicionarAcucar,
        Concluido
    }

    public ExerciseStep currentStep = ExerciseStep.ColocarChavena;

    [Header("Visuals & UI")]
    public GameObject waterVisual;
    public Renderer waterRenderer;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI stepsText;

    [Header("Highlights")]
    public GameObject cupHighlight;
    public GameObject waterButtonHighlight;
    public GameObject teaBagHighlight;
    public GameObject sugarHighlight;

    [Header("Settings")]
    public GameObject teaCube;
    public float hintDelay = 15f;

    private bool cupPlaced = false;
    private bool waterAdded = false;
    private bool teaAdded = false;
    private bool sugarAdded = false;

    private Coroutine hintCoroutine;

    private void Start()
    {
        if (ExerciseStatsManager.instance != null)
            ExerciseStatsManager.instance.StartExercise("Cha");

        ShowInstruction("Objetivo: Preparar uma chávena de chá.\nColoca a chávena no dispensador.");
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
        text += GetStepText("Colocar chávena", currentStep > ExerciseStep.ColocarChavena);
        text += GetStepText("Adicionar água", currentStep > ExerciseStep.AdicionarAgua);
        text += GetStepText("Adicionar chá", currentStep > ExerciseStep.AdicionarCha);
        text += GetStepText("Adicionar açúcar", currentStep > ExerciseStep.AdicionarAcucar);

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
        if (waterButtonHighlight != null) waterButtonHighlight.SetActive(false);
        if (teaBagHighlight != null) teaBagHighlight.SetActive(false);
        if (sugarHighlight != null) sugarHighlight.SetActive(false);
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
            case ExerciseStep.ColocarChavena:
                if (cupHighlight != null)
                {
                    cupHighlight.SetActive(true);
                    RegisterHint();
                }
                break;

            case ExerciseStep.AdicionarAgua:
                if (waterButtonHighlight != null)
                {
                    waterButtonHighlight.SetActive(true);
                    RegisterHint();
                }
                break;

            case ExerciseStep.AdicionarCha:
                if (teaBagHighlight != null)
                {
                    teaBagHighlight.SetActive(true);
                    RegisterHint();
                }
                break;

            case ExerciseStep.AdicionarAcucar:
                if (sugarHighlight != null)
                {
                    sugarHighlight.SetActive(true);
                    RegisterHint();
                }
                break;
        }
    }

    public void CupPlaced()
    {
        if (currentStep != ExerciseStep.ColocarChavena)
            return;

        cupPlaced = true;
        currentStep = ExerciseStep.AdicionarAgua;

        UpdateStepsUI();
        ShowInstruction("Prime o botão de água quente.");
        StartHintTimer();
    }

    public void AddWater()
    {
        if (!cupPlaced)
        {
            RegisterError();
            ShowInstruction("Coloca a chávena primeiro.");
            return;
        }

        if (waterAdded)
        {
            RegisterError();
            ShowInstruction("A água já foi colocada.");
            return;
        }

        if (currentStep != ExerciseStep.AdicionarAgua)
        {
            RegisterError();
            ShowInstruction("Ainda não podes adicionar água.");
            return;
        }

        waterAdded = true;

        if (waterVisual != null)
            waterVisual.SetActive(true);

        currentStep = ExerciseStep.AdicionarCha;

        UpdateStepsUI();
        ShowInstruction("Água quente servida.\nAgora coloca o saco de chá.");
        StartHintTimer();
    }

    public void AddTea(GameObject teaBag)
    {
        if (!waterAdded)
        {
            RegisterError();
            ShowInstruction("Primeiro coloca a água quente.");
            return;
        }

        if (teaAdded)
        {
            RegisterError();
            ShowInstruction("O chá já foi colocado.");
            return;
        }

        if (currentStep != ExerciseStep.AdicionarCha)
        {
            RegisterError();
            ShowInstruction("Ainda não podes colocar o chá.");
            return;
        }

        teaAdded = true;

        if (waterRenderer != null)
            waterRenderer.material.color = new Color(0.5f, 0.4f, 0.1f);

        if (teaBag != null)
        {
            Rigidbody rb = teaBag.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = true;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            Collider[] colliders = teaBag.GetComponentsInChildren<Collider>();
            foreach (var col in colliders)
                col.enabled = false;
        }

        if (teaCube != null)
            teaCube.SetActive(false);

        currentStep = ExerciseStep.AdicionarAcucar;

        UpdateStepsUI();
        ShowInstruction("Saco de chá colocado.\nAgora adiciona o açúcar.");
        StartHintTimer();
    }

    public void AddSugar(GameObject sugar)
    {
        if (!waterAdded)
        {
            RegisterError();
            ShowInstruction("Primeiro coloca a água quente.");
            return;
        }

        if (!teaAdded)
        {
            RegisterError();
            ShowInstruction("Primeiro coloca o chá.");
            return;
        }

        if (sugarAdded)
        {
            RegisterError();
            ShowInstruction("O açúcar já foi colocado.");
            return;
        }

        if (currentStep != ExerciseStep.AdicionarAcucar)
        {
            RegisterError();
            ShowInstruction("Ainda não podes adicionar o açúcar.");
            return;
        }

        sugarAdded = true;

        if (sugar != null)
        {
            var grab = sugar.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            if (grab != null)
                grab.enabled = false;

            Rigidbody rb = sugar.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }

            Collider[] colliders = sugar.GetComponentsInChildren<Collider>();
            foreach (var col in colliders)
                col.enabled = false;

            sugar.SetActive(false);
        }

        currentStep = ExerciseStep.Concluido;

        UpdateStepsUI();
        ShowInstruction("Açúcar colocado.\nChá concluído com sucesso!");

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