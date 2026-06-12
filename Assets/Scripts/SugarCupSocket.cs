using UnityEngine;

public class SugarCupSocket : MonoBehaviour
{
    public TeaExerciseSequence teaExercise;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entrou no SugarCupSocket: " + other.name + " | Tag: " + other.tag);

        GameObject obj = other.attachedRigidbody != null
            ? other.attachedRigidbody.gameObject
            : other.gameObject;

        Debug.Log("Objeto principal: " + obj.name + " | Tag: " + obj.tag);

        if (obj.CompareTag("Sugar") || other.CompareTag("Sugar"))
        {
            Debug.Log("Sugar detetado dentro da chávena.");

            if (teaExercise != null)
                teaExercise.AddSugar(obj);
            else
                Debug.LogError("TeaExercise não está ligado no SugarCupSocket.");
        }
    }
}