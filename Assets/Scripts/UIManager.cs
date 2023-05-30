using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject healthTextPrefab;
    public GameObject damageTextPrefab;

    public Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
        
    }
    // Start is called before the first frame update

    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed;
    }

    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        //damageReceived = -damageReceived;

        tmpText.text = "-" + damageReceived.ToString();
    }

    public void CharacterHealed(GameObject character, int healAmount)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tmpText.text ="+" + healAmount.ToString();
    }

    public void OnExitGame(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
                        Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            #endif

            #if (UNITY_EDITOR)
                        UnityEditor.EditorApplication.isPlaying = false;
            #elif (UNITY_STANDALONE)
                        Application.Quit();
            #elif (UNITY_WEBGL)
                        SceneManager.LoadScene("QuitScene");
            #endif

        }


    }
}
