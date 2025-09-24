using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    public GameObject left_button_text;
    public GameObject middle_button_text;
    public GameObject right_button_text;

    public void SetupText(string left_text, string middle_text, string right_text)
    {
        left_button_text.GetComponent<TextMeshProUGUI>().SetText(left_text);
        middle_button_text.GetComponent<TextMeshProUGUI>().SetText(middle_text);
        right_button_text.GetComponent<TextMeshProUGUI>().SetText(right_text);
    }
}
