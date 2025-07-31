
using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class StaticDialogueBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private TextMeshProUGUI nameTag;
    private DialogueContext currentContext;

    public IEnumerator Show()
    {
        DialogueBoxPopUp();
        // yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        nameTag.text = currentContext.CharacterName;
        
        foreach(string text in currentContext.contexts)
        {
            SetText(text);
            TextPopUp();
            yield return new WaitForSeconds(0.5f);

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        }
        DialogueBoxFadeOut();
        

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        StartCoroutine(Show());
    }

    public void SetContext(DialogueContext context) => currentContext = context;

    public void SetText(string text){
        textMesh.SetText(text);
        textMesh.ForceMeshUpdate();
    }

    public void DialogueBoxPopUp()
    {
        // gameObject.SetActive(true);
    }

    public void DialogueBoxFadeOut(){
        currentContext = currentContext.nextDialogue;
        if(currentContext == null)
        {
            gameObject.SetActive(false);
            StopCoroutine(Show());
        }
            
    }

    public void TextPopUp(){
        StartCoroutine(PopUp());
    }

    private IEnumerator PopUp(){
        float a = 0f;
        while(a <= 1f){
            a += Time.deltaTime * 1f;
            textMesh.color = new Color(textMesh.color.r ,textMesh.color.g ,textMesh.color.b ,a);
            yield return null;
        }
    }
    public void Activate()
    {
        gameObject.SetActive(true);
        StartCoroutine(Show());
    }

}
