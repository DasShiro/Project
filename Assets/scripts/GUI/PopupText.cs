using UnityEngine;
using TMPro; // Add this for TextMeshPro
using System.Collections; // Required for IEnumerator

public class PopupText : MonoBehaviour
{
    public GameObject textObject; // Assign the TextMeshPro GameObject in the Inspector

    public void ShowPopup()
    {
        // Set the text and make it visible
        textObject.GetComponent<TMP_Text>().text = "Work in Progress !";
        textObject.SetActive(true);

        // Optionally, you can hide the text after a few seconds
        StartCoroutine(HidePopupAfterDelay(2f)); // Hides after 2 seconds
    }

    private IEnumerator HidePopupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        textObject.SetActive(false);
    }
}