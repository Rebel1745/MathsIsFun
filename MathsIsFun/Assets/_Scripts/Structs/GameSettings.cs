using UnityEngine;
using UnityEngine.UIElements;

public struct GameSettings
{
    public string GameName;
    public VisualElement GameVE;
    public VisualElement GameSettingsVE;
    public IGame GameInterface;
    public IGameSettings GameSettingsInterface;
    public int NumberOfQuestions;
    public int NumberOfQuestionCorrect;
    public int SmallestNumber;
    public int LargestNumber;
    public bool AutoNextQuestion;
    public bool UseTimer;
    public bool UnlimitedTime;
    public int MaxTimeToAnswer;
    public float TotalTimeTaken;
}
