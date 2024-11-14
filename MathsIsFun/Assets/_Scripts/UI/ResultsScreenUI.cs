using UnityEngine;
using TMPro;

public class ResultsScreenUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _numberOfQuestionsText;
    [SerializeField] private TMP_Text _numberOfQuestionsCorrectText;
    [SerializeField] private TMP_Text _numberOfQuestionsIncorrectText;
    [SerializeField] private TMP_Text _percentCorrectText;
    [SerializeField] private TMP_Text _ratingText;

    public void ShowResults(int numberOfQuestions, int correct)
    {
        _numberOfQuestionsText.text += " " + numberOfQuestions;
        _numberOfQuestionsCorrectText.text += " " + correct;
        _numberOfQuestionsIncorrectText.text += " " + (numberOfQuestions - correct);
        int percentCorrect = Mathf.RoundToInt(((float)correct / (float)numberOfQuestions) * 100);
        _percentCorrectText.text += " " + percentCorrect + "%";
        _ratingText.text += " " + PercentToRating(percentCorrect);
    }

    private string PercentToRating(int percent)
    {
        string rating = "";

        if (percent == 100) rating = "Perfect!";
        else if (percent >= 90) rating = "Magnificent";
        else if (percent >= 80) rating = "Great";
        else if (percent >= 70) rating = "Good";
        else if (percent >= 60) rating = "OK";
        else if (percent >= 50) rating = "Average";
        else if (percent >= 40) rating = "Below average";
        else if (percent >= 30) rating = "Not good";
        else if (percent >= 20) rating = "Rubbish";
        else if (percent >= 10) rating = "Useless";
        else rating = "";

        return rating;
    }
}
