
using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class DynamicDialogueBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private TextMeshProUGUI nameTag;
    
    private DialogueContext currentContext;

    public string context;

    public IEnumerator Show()
    {
        DialogueBoxPopUp();
        string temp = "";
        foreach(char text in context)
        {   
            temp += text;
            SetText(" : " + temp);
            yield return new WaitForSeconds(0.1f);
        }
        TimelineController.Instance.Pause();
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        TimelineController.Instance.Play();

    }

    public void SetContext(string context) => this.context = context;

    public void SetText(string text){
        textMesh.SetText(text);
        textMesh.ForceMeshUpdate();
    }

    public void DialogueBoxPopUp(){}

    public void DialogueBoxFadeOut()
    {
        currentContext = currentContext.nextDialogue;
        gameObject.SetActive(false);
            
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

    public void SetWeight(float weight)
    {
        textMesh.color = new Color(1, 1, 1, weight);
        // if(weight > 0)
        // gameObject.SetActive(weight > 0);
    }
}
