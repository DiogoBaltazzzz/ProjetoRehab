using UnityEngine;
using UnityEngine.SceneManagement;  // Necessário para carregar as cenas

public class MenuManager : MonoBehaviour
{
    // Função para carregar o Exercício 1 (Preparar Chá)
    public void StartExercise1()
    {
        SceneManager.LoadScene("Cafe"); // Nome da cena do exercício 1 (Exemplo: "Exercicio1")
    }

    // Função para carregar o Exercício 2 (Preparar Café)
    public void StartExercise2()
    {
        SceneManager.LoadScene("Cha"); // Nome da cena do exercício 2 (Exemplo: "Exercicio2")
    }

    // Função para carregar o Exercício 3 (se aplicável)
    public void StartExercise3()
    {
        SceneManager.LoadScene("Memoria"); // Nome da cena do exercício 3
    }

    // Função para carregar o Exercício 4 (se aplicável)
    public void StartExercise4()
    {
        SceneManager.LoadScene("Memoria Hard"); // Nome da cena do exercício 4
    }

    // Função para mostrar as estatísticas
    public void OpenStatistics()
{
    SceneManager.LoadScene("Estatisticas");
}
}