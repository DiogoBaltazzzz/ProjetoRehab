using UnityEngine;

public class TeaBagCupSocket : MonoBehaviour
{
    public TeaExerciseSequence teaExercise;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entrou no TeaBagCupSocket: " + other.name + " | Tag: " + other.tag);

        GameObject obj = other.attachedRigidbody != null
            ? other.attachedRigidbody.gameObject
            : other.gameObject;

        Debug.Log("Objeto principal: " + obj.name + " | Tag: " + obj.tag);

        if (obj.CompareTag("TeaBag") || other.CompareTag("TeaBag"))
        {
            Debug.Log("TeaBag detetado dentro da chávena.");

            if (teaExercise != null)
                teaExercise.AddTea(obj);
            else
                Debug.LogError("TeaExercise não está ligado no TeaBagCupSocket.");
        }
    }
}