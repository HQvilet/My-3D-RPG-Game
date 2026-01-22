
using UnityEngine;
using TMPro;
using System;
using System.Collections;
using DialogueGraph;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

public class StaticDialogueBox : MonoBehaviour
{
    [SerializeField] OptionBox optionPref;
    [SerializeField] GameObject optionContainer;

    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private TextMeshProUGUI nameTag;

    [SerializeField] private bool isShown;
    private bool isSelecting;
    private DialogueGraphAsset dialogueAsset;
    private GraphNodeData currentNode;
    
    List<OptionBox> m_cache = new();
    int m_index;
    public IEnumerator Show()
    {

        isShown = true;
        m_index = 0;
        do
        {
            currentNode = dialogueAsset.MoveToNextNode(currentNode, m_index);

            if (currentNode == null)
                continue;

            if (currentNode.GetNodeType() == DialogueType.EVENT)
            {
                Bus<IContentPackageRequest>.Raise(new IContentPackageRequest(((EventNode)currentNode).assetToLoad));
                continue;
            }
            
            NormalDialogueNode currentContentNode = (NormalDialogueNode)currentNode;
            if (currentContentNode == null)
                continue;
            
            nameTag.text = currentContentNode._name;
            SetText(currentContentNode._content);
            TextPopUp();
            yield return new WaitForSeconds(0.5f);

            if (currentContentNode.GetNodeType() == DialogueType.OPTIONAL)
            {

                optionContainer.SetActive(true);
                isSelecting = true;
                AddToOptionBox(((OptionalDialogueNode)currentContentNode)._options);

                yield return new WaitUntil(() => !isSelecting);

                foreach (var a in m_cache)
                {
                    Destroy(a.gameObject);
                }
                m_cache.Clear();                    
                optionContainer.SetActive(false);
            }
            else
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        }
        while (currentNode != null);
        isShown = false;
        gameObject.SetActive(false);
        yield return null;
    }

    
    // public void SetContext(DialogueContext context) => currentContext = context;
    public void AddToOptionBox(List<DialogueOption> options)
    {
        int i = 0;
        foreach (var option in options)
        {
            var a = Instantiate<OptionBox>(optionPref, optionContainer.transform);
            a.SetInfo(i, option._optionContent,() =>
            {
                isSelecting = false;
                m_index = a.GetIndex();
            });
            // a.SetInfo(i, option._optionContent, this);
            m_cache.Add(a);
            i++;
        }   
    }


    public void SetText(string text)
    {
        textMesh.SetText(text);
        textMesh.ForceMeshUpdate();
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
    public void Activate(DialogueGraphAsset asset)
    {
        if (isShown)
            return;
        Debug.Log("Show dialogue");
        this.dialogueAsset = asset;
        gameObject.SetActive(true);
        StartCoroutine(Show());
    }

}
