// Manager/Forge.cs
using System.Collections.Generic;
using UnityEngine;
using TeamWorkSpaces.LeisureModule;


    public class Forge : MonoBehaviour
    {
        public QuestionManager questionManager;

        public void ValidateResponse(List<string> selectedWords)
        {
            if (questionManager.CurrentQuestion == null)
            {
                Debug.LogError("No hay una pregunta cargada actualmente.");
                return;
            }

            string response = string.Join(" ", selectedWords);

            // Verificar si la respuesta coincide con alguna correcta
            foreach (var answer in questionManager.CurrentQuestion.correct_answers)
            {
                if (response.Equals(answer.answer, System.StringComparison.OrdinalIgnoreCase))
                {
                    Debug.Log("�Respuesta correcta!");
                    // Añadir efectos o lógica adicional aqu�
                    return;
                }
            }

            Debug.Log("Respuesta incorrecta.");
            // Añadir efectos o lógica adicional aqu�
        }
    }
