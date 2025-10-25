using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image ProgressImage;
    [SerializeField] private float Speed;

    [SerializeField] private Gradient ColorGradient;
    private Coroutine AnimationCoroutine;
    
    private event Action<float> OnProgress;
    private event Action OnProgressCompleted;

    private void Start()
    {
        ProgressImage.color = ColorGradient.Evaluate(0f);
    }

    public void SetProgress(float _progress)
    {
        if (_progress < 0 || _progress > 1)
        {
            Debug.LogWarning($"Invalid progress passed, expected value is between 0 and 1, got {_progress}. Clamping.");
            _progress = Mathf.Clamp01(_progress);
        }
        if (_progress != ProgressImage.fillAmount)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }
            AnimationCoroutine = StartCoroutine(AnimateProgress(_progress));
        }
    }

    private IEnumerator AnimateProgress(float Progress)
    {
        float time = 0f;
        float initialProgress = ProgressImage.fillAmount;

        while (time < 1f)
        {
            ProgressImage.fillAmount = Mathf.Lerp(initialProgress, Progress, time);
            time += Time.deltaTime * Speed;

            ProgressImage.color = ColorGradient.Evaluate(1 - ProgressImage.fillAmount);

            OnProgress?.Invoke(ProgressImage.fillAmount);
            yield return null;
        }

        ProgressImage.fillAmount = Progress;
        ProgressImage.color = ColorGradient.Evaluate(1 - ProgressImage.fillAmount);

        OnProgress?.Invoke(Progress);
        OnProgressCompleted?.Invoke();
    }
}
