using UnityEngine;
using UnityEngine.UIElements;

public class ResultsScreen : MonoBehaviour
{
    public static ResultsScreen instance;

    public VisualElement _resultsScreen;
    private Label _numberOfQuestionsText;
    private Label _numberOfQuestionsCorrectText;
    private Label _numberOfQuestionsIncorrectText;
    private Label _percentCorrectText;
    private Label _ratingText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _resultsScreen = root.Q<VisualElement>("ResultsScreen");
        _numberOfQuestionsText = root.Q<Label>("NumberOfQuestionsText");
        _numberOfQuestionsCorrectText = root.Q<Label>("NumberOfQuestionsCorrectText");
        _numberOfQuestionsIncorrectText = root.Q<Label>("NumberOfQuestionsIncorrectText");
        _percentCorrectText = root.Q<Label>("PercentCorrectText");
        _ratingText = root.Q<Label>("RatingText");
    }

    public void ShowResults(int numberOfQuestions, int correct)
    {
        _numberOfQuestionsText.text = numberOfQuestions.ToString();
        _numberOfQuestionsCorrectText.text = correct.ToString();
        _numberOfQuestionsIncorrectText.text = (numberOfQuestions - correct).ToString();
        int percentCorrect = Mathf.RoundToInt(((float)correct / (float)numberOfQuestions) * 100);
        _percentCorrectText.text = percentCorrect + "%";
        _ratingText.text = PercentToRating(percentCorrect);

        _resultsScreen.style.display = DisplayStyle.Flex;
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
