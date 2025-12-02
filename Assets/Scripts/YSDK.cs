using UnityEngine;
using UnityEngine.Events;
using YG;

public class YSDK : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent[] _onRewardAdvEvents;
    [SerializeField] private UnityEvent[] _onInterAdvEvents;
    [SerializeField] private UnityEvent[] _onAnyAdvCloseEvents;

    [Header("Params")]
    [SerializeField] private bool _showAdvOnStart = false;

    private const string REVIVE_ID = "Revive";

    private void Start()
    {
        YG2.StickyAdActivity(true);

        if (_showAdvOnStart)
            ShowInterAdv();
    }

    private void OnEnable()
    {
        YG2.onCloseAnyAdv += CloseAnyAdv;
        YG2.onErrorInterAdv += CloseAnyAdv;
        YG2.onRewardAdv += OnReward;
    }

    private void OnDisable()
    {
        YG2.onCloseAnyAdv -= CloseAnyAdv;
        YG2.onErrorInterAdv -= CloseAnyAdv;
        YG2.onRewardAdv -= OnReward;
    }

    public void ShowInterAdv()
    {
        YG2.InterstitialAdvShow();

        foreach (var item in _onInterAdvEvents)
        {
            item?.Invoke();
        }

        AdvIsShow(YG2.nowAdsShow);
    }

    public void ShowRewardedAdv()
    {
        YG2.RewardedAdvShow(REVIVE_ID);

        AdvIsShow(YG2.nowAdsShow);
    }

    private void OnReward(string id)
    {
        foreach (var item in _onRewardAdvEvents)
        {
            item?.Invoke();
        }
    }

    private void AdvIsShow(bool isShow)
    {
        if (isShow == false)
        {
            CloseAnyAdv();
        }
    }

    private void CloseAnyAdv()
    {
        foreach (var item in _onAnyAdvCloseEvents)
        {
            item?.Invoke();
        }
    }
}
