using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MemoryButton : MonoBehaviour
{
    public int buttonIndex;
    public MemoryGameManager gameManager;

    public Renderer buttonRenderer;

    public Material normalMaterial;
    public Material sequenceMaterial; // cor quando o PC mostra a sequência
    public Material playerMaterial;   // cor quando o utilizador carrega

    public float flashTime = 0.5f;

    public void PressButton()
    {
        Debug.Log("Botão pressionado: " + buttonIndex);

        PlayerFlash();

        if (gameManager != null)
            gameManager.PlayerPressedButton(buttonIndex);
    }

    public void Flash()
    {
        StartCoroutine(FlashRoutine(sequenceMaterial));
    }

    public void PlayerFlash()
    {
        StartCoroutine(FlashRoutine(playerMaterial));
    }

    private IEnumerator FlashRoutine(Material materialToUse)
    {
        if (buttonRenderer != null && materialToUse != null)
            buttonRenderer.material = materialToUse;

        yield return new WaitForSeconds(flashTime);

        if (buttonRenderer != null && normalMaterial != null)
            buttonRenderer.material = normalMaterial;
    }

    public void OnXRSelectEntered(SelectEnterEventArgs args)
    {
        PressButton();
    }
}