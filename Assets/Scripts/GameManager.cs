using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject menuPanel;        // Menu Inicial
    public GameObject exercisePanel;    // Painel de Exercício
    public TextMeshProUGUI instructionText;

    private void Start()
    {
        ShowMenu(); // Mostrar o menu ao iniciar
    }

    // Função para mostrar o menu inicial
    public void ShowMenu()
    {
        menuPanel.SetActive(true);       // Ativa o menu inicial
        exercisePanel.SetActive(false); // Desativa o painel de exercício
        instructionText.text = "Escolha um exercício"; // Atualiza a mensagem
    }

    // Função para começar o exercício 1
    public void StartExercise1()
    {
        menuPanel.SetActive(false);     // Desativa o menu inicial
        exercisePanel.SetActive(true);  // Ativa o painel do exercício
        instructionText.text = "Iniciando exercício 1..."; // Instruções do exercício 1
        // Coloque a lógica do Exercício 1 aqui
    }

    // Função para começar o exercício 2
    public void StartExercise2()
    {
        menuPanel.SetActive(false);     // Desativa o menu inicial
        exercisePanel.SetActive(true);  // Ativa o painel do exercício
        instructionText.text = "Iniciando exercício 2..."; // Instruções do exercício 2
        // Coloque a lógica do Exercício 2 aqui
    }
}