using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;


public class ProgressMeshRenderer : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] private Gradient ColorGradient;
    MeshRenderer render;

    float fillAmount
    {
        get => render.material.GetFloat("_FillAmount");
        set {
            render.material.SetFloat("_FillAmount", value);
        }
    }

    [SerializeField] float _speed;

    private CoroutineHandle AnimationCoroutine;
    
    private event Action<float> OnProgress;
    private event Action OnProgressCompleted;

    private void Awake()
    {
        render = GetComponent<MeshRenderer>();
        render.material = new Material(material);
    }

    public void SetProgress(float _progress)
    {
        if (_progress < 0 || _progress > 1)
        {
            Debug.LogWarning($"Invalid progress passed, expected value is between 0 and 1, got {_progress}. Clamping.");
            _progress = Mathf.Clamp01(_progress);
        }
        if (_progress != fillAmount)
        {
            if (AnimationCoroutine != null)
            {
                Timing.PauseCoroutines(AnimationCoroutine);
            }
            if(gameObject.activeInHierarchy)
                AnimationCoroutine = Timing.RunCoroutine(AnimateProgress(_progress).CancelWith(gameObject));
        }
    }

    private IEnumerator<float> AnimateProgress(float Progress)
    {
        float time = 0f;
        float initialProgress = fillAmount;

        while (time < 1f)
        {
            fillAmount = Mathf.Lerp(initialProgress, Progress, time);
            time += Time.deltaTime * _speed;

            // ProgressImage.color = ColorGradient.Evaluate(1 - ProgressImage.fillAmount);

            OnProgress?.Invoke(fillAmount);
            yield return 0;
        }

        fillAmount = Progress;
        // ProgressImage.color = ColorGradient.Evaluate(1 - ProgressImage.fillAmount);

        OnProgress?.Invoke(Progress);
        OnProgressCompleted?.Invoke();
    }

}
