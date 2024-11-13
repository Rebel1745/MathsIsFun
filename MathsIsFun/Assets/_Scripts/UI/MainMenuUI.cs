using UnityEngine;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _pickGameUI;

    public void PickGameButtonPressed()
    {
        _pickGameUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }
}
