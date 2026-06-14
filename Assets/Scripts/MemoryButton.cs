using System.Collections;
using UnityEngine;

public class MemoryButton : MonoBehaviour
{
    public int buttonIndex;
    public MemoryGameManager gameManager;

    public Renderer buttonRenderer;
    public Material normalMaterial;
    public Material litMaterial;
    public Material pressedMaterial;

    public float flashTime = 0.5f;
    public float pressedFlashTime = 0.2f;

    public void PressButton()
    {
        Debug.Log("Botão pressionado: " + buttonIndex);

        if (gameManager != null)
            gameManager.PlayerPressedButton(buttonIndex);

        FlashPressed();
    }

    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    public void FlashPressed()
    {
        StartCoroutine(PressedRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        if (buttonRenderer != null && litMaterial != null)
            buttonRenderer.material = litMaterial;

        yield return new WaitForSeconds(flashTime);

        if (buttonRenderer != null && normalMaterial != null)
            buttonRenderer.material = normalMaterial;
    }

    private IEnumerator PressedRoutine()
    {
        if (buttonRenderer != null && pressedMaterial != null)
            buttonRenderer.material = pressedMaterial;

        yield return new WaitForSeconds(pressedFlashTime);

        if (buttonRenderer != null && normalMaterial != null)
            buttonRenderer.material = normalMaterial;
    }
}