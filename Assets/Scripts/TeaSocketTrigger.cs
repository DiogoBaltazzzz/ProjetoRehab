using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeaSocketTrigger : MonoBehaviour
{
    public TeaExerciseSequence exerciseSequence;

    public void OnTeaInserted(SelectEnterEventArgs args)
    {
        GameObject insertedObject = args.interactableObject.transform.gameObject;
        Debug.Log("Objeto inserido no TeaSocket: " + insertedObject.name);

        exerciseSequence.AddTea(insertedObject);
    }
}
