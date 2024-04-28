using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCanvas : MonoBehaviour
{
    [Header("START")]
    [SerializeField] private CanvasGroup _storyCanvasGroup;
    [SerializeField] private CanvasGroup _imageCanvasGroup;
    [SerializeField] private float fadeDuration = 3.5f;

    [Header("END")]
    [SerializeField] private CanvasGroup _ENDStoryCanvasGroup; //you win
    [SerializeField] private CanvasGroup _ENDThanksForPlayingCanvasGroup; //thx for playing
    [SerializeField] private CanvasGroup _ENDImageCanvasGroup; 
    [SerializeField] private float endCameraSwapWaitTime = 1f;
    [SerializeField] private float endCutsceneWaitTime = 10f;

    [Header("CUTSCENE")]
    [SerializeField] dtwen endCutscene;

    public static event System.Action OnSwapCameraNow;

    private void Awake()
    {

        GameWinCondition.OnGameWin += GameWinCondition_OnGameWin;
        StoryCanvas.OnSwapCameraNow += GameEndCutscene;
    }

    private void GameWinCondition_OnGameWin()
    {
        GameEnd();
    }

    void Start()
    {
        GameStart();
        //GameEnd();
    }

    async void GameStart()
    {
        await UniTask.WaitForSeconds(fadeDuration);
        FadeIn(_storyCanvasGroup);
        await UniTask.WaitForSeconds(15f);
        FadeOut(_storyCanvasGroup);
        await UniTask.WaitForSeconds(fadeDuration);
        FadeOut(_imageCanvasGroup);
    }
    async void GameEnd()
    {
        FadeIn(_ENDImageCanvasGroup); // goes black
        await UniTask.WaitForSeconds(fadeDuration);
        FadeIn(_ENDStoryCanvasGroup); // you win
        await UniTask.WaitForSeconds(7f);
        FadeOut(_ENDStoryCanvasGroup); // you win fade out
        OnSwapCameraNow?.Invoke(); // swap camera here.

    }

    async void GameEndCutscene()
    {
        endCutscene.SwapCams();
        await UniTask.WaitForSeconds(endCameraSwapWaitTime);
        FadeOut(_ENDImageCanvasGroup);
        await UniTask.WaitForSeconds(fadeDuration);
        endCutscene.PlayCutscene();
        await UniTask.WaitForSeconds(endCutsceneWaitTime); // wait for cutscene to finish
        FadeIn(_ENDImageCanvasGroup); // goes black again
        await UniTask.WaitForSeconds(fadeDuration);
        FadeIn(_ENDThanksForPlayingCanvasGroup); // thx for playing
    }



    void FadeIn(CanvasGroup canvasGroup)
    {
        canvasGroup.DOFade(1f, fadeDuration);
    }
    void FadeOut(CanvasGroup canvasGroup)
    {
        canvasGroup.DOFade(0f, fadeDuration);
    }
}
