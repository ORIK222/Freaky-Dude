using System;
using Firebase.Analytics;
using UnityEngine;

public class IronSourceAdsManager : MonoBehaviour
{
    public static IronSourceAdsManager Instance;

    public Action RewardedVideoEnded;
    public Action InterstetialVideoEnded;
    
    private void Awake()
    {
        IronSource.Agent.setMetaData("is_child_directed","false");
        Instance = this;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        IronSource.Agent.loadInterstitial();
        IronSource.Agent.loadRewardedVideo();
        IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdLoadFailedEvent += RewardedAdFailedLoadEvent;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedAdFailedShowEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdFailedLoadEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdFailedShowEvent;
    }

    private void RewardedAdFailedShowEvent(IronSourceError arg1, IronSourceAdInfo arg2)
    {
        FirebaseAnalytics.LogEvent("rewarded_failed_show", new Parameter(FirebaseAnalytics.ParameterSuccess, "failed"));
    }

    private void RewardedAdFailedLoadEvent(IronSourceError obj)
    {
        FirebaseAnalytics.LogEvent("rewarded_failed_load", new Parameter(FirebaseAnalytics.ParameterSuccess, "failed"));
    }

    private void InterstitialAdFailedShowEvent(IronSourceError obj)
    {
        FirebaseAnalytics.LogEvent("interstitial_failed_load", new Parameter(FirebaseAnalytics.ParameterSuccess, "failed"));
    }

    private void InterstitialAdFailedLoadEvent(IronSourceError obj)
    {
        FirebaseAnalytics.LogEvent("interstitial_failed_show", new Parameter(FirebaseAnalytics.ParameterSuccess, "failed"));
    }

    private void InterstitialAdClosedEvent()
    {
        FirebaseAnalytics.LogEvent("interstitial_show_success", new Parameter(FirebaseAnalytics.ParameterSuccess, "success"));
        IronSource.Agent.loadInterstitial();
        InterstetialVideoEnded?.Invoke();
    }

    public void ShowRewardedVideo()
    {
        IronSource.Agent.showRewardedVideo();
    }

    public void ShowInterstitialVideo()
    {
        IronSource.Agent.showInterstitial();
    }

    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    {
        FirebaseAnalytics.LogEvent("rewarded_show_success", new Parameter(FirebaseAnalytics.ParameterSuccess, "success"));
        IronSource.Agent.loadRewardedVideo();
        RewardedVideoEnded?.Invoke();
    }

}
