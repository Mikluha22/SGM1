using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceHealthbar : MonoBehaviour
{
    public Slider healthSlider;
    private BaseCharacter character;
    public TextMeshProUGUI hpText;
 
   

    void Start()
    {
        character = GetComponentInParent<BaseCharacter>();
    }

    void Update()
    {
        if (character != null)
            healthSlider.value = character.GetHealthPercent();
        hpText.text = $"{character.GetHealthPercent() * 100:F0}%";
    }
}
