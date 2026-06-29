using System.Collections;
using UnityEngine;

public class MemoryButton : MonoBehaviour
{
    private MemoryGameManager gameManager;
    private int buttonIndex;

    [Header("Renderer")]
    public Renderer buttonRenderer;

    [Header("Materials")]
    public Material normalMaterial;
    public Material sequenceMaterial;
    public Material playerPressedMaterial;

    [Header("Timing")]
    public float flashTime = 0.5f;
    public float pressedFlashTime = 0.2f;

    private Coroutine flashCoroutine;

    private void Awake()
    {
        if (buttonRenderer == null)
            buttonRenderer = GetComponent<Renderer>();
    }

    public void Setup(MemoryGameManager manager, int index)
    {
        gameManager = manager;
        buttonIndex = index;

        SetNormal();
    }

    public int GetIndex()
    {
        return buttonIndex;
    }

    public void PressButton()
{
    Debug.Log("Botão pressionado pelo jogador: " + buttonIndex);

    if (gameManager != null)
    {
        bool accepted = gameManager.PlayerPressedButton(this);

        if (accepted)
        {
            if (ExerciseAudioManager.instance != null)
                ExerciseAudioManager.instance.PlayStepComplete();

            FlashPlayerPressed();
        }
    }
    else
    {
        Debug.LogError("GameManager não foi configurado no botão: " + name);
    }
}

    public void FlashSequence()
    {
        Debug.Log("Botão a piscar na sequência: " + buttonIndex);

        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(FlashRoutine(sequenceMaterial, flashTime));
    }

    public void FlashPlayerPressed()
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(FlashRoutine(playerPressedMaterial, pressedFlashTime));
    }

    public void SetNormal()
    {
        SetMaterial(normalMaterial);
    }

    private IEnumerator FlashRoutine(Material flashMaterial, float duration)
    {
        SetMaterial(flashMaterial);

        yield return new WaitForSeconds(duration);

        SetNormal();

        flashCoroutine = null;
    }

    private void SetMaterial(Material material)
    {
        if (buttonRenderer == null)
        {
            Debug.LogError("Button Renderer em falta no botão: " + name);
            return;
        }

        if (material == null)
        {
            Debug.LogError("Material em falta no botão: " + name);
            return;
        }

        buttonRenderer.material = material;
    }
}