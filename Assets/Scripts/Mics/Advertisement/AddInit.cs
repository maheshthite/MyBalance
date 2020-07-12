using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;
using System;

public class AddInit : MonoBehaviour
{
    private static BannerView bannerView;
    private static InterstitialAd interstitial;
    public static AudioSource m_MyAudioSource;


    [SerializeField]
    private bool testMode;

    static string InterstitialPlacementID = "video";
    static string myPlacementId = "rewardedVideo";
    static string BanerAdPlacementId = "BanerAd";

    void Awake()
    {
        //GameDistribution.OnResumeGame += OnResumeGame;
       // GameDistribution.OnPauseGame += OnPauseGame;
        //sGameDistribution.OnPreloadRewardedVideo += OnPreloadRewardedVideo;

    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject mus = GameObject.Find("music");
        m_MyAudioSource = mus.GetComponent<AudioSource>();
        if (HeaderTextScript.bAdvertimentFlagWebGL)
        {
            // Debug.Log("ApplixirWebGL AddInit  ");
            // ApplixirWebGL.init(4055, 5013, 2975);
           // if (ScoreController.bStartGame == false)
                ShowInterstitialVideo();
        }
        if(HeaderTextScript.bAdvertimentFlag)
         {
            MobileAds.Initialize(initStatus => { });

            RequestBanner();
            RequestInterstitial();
        }
    }
    private void RequestBanner()
    {

#if UNITY_ANDROID
    string adUnitId = "ca-app-pub-3405518223004066/1008392046";
#elif UNITY_IOS
            string adUnitId = "ca-app-pub-3405518223004066/9640850098";
#else
        string adUnitId = "unexpected_platform";
#endif

    // Create a 320x50 banner at the top of the screen.
    Debug.Log("bannerView.LoadAd");
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.BottomRight);
    }
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3405518223004066/8695310372";
#elif UNITY_IOS
        string adUnitId = "ca-app-pub-3405518223004066/9475723785";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Called when an ad request has successfully loaded.
        interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        // Load the interstitial with the request.


        interstitial.LoadAd(request);
    }
    public static void ShowInterstitialVideo()
    {
       
        if(HeaderTextScript.bAdvertimentFlagWebGL)
        {

            // Debug.Log("ApplixirWebGL: ShowInterstitialVideo");
            // ApplixirWebGL.ShowVideo(4055, 5013, 2975, ApplixirWebGLCallBack);
            //Garter.I.RequestAd("AdUnits1", GameArterCallBackReward);
            //GameDistribution.Instance.ShowAd();
            HeaderTextScript.iAdvertisePlaying = 5;
        }
        else
        {
            Debug.Log("ShowInterstitialVideo: " + InterstitialPlacementID);
            //Advertisement.Show(InterstitialPlacementID);
            interstitial.Show();
        }

    
    }

    public static void ShowRewardedVideo()
    {
        Debug.Log("ShowRewardedVideo: " + myPlacementId);
        //Advertisement.Show(myPlacementId);
    }
    public static void ShowBannerAdv()
    {

            Debug.Log("bannerView.LoadAd");
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();

            // Load the banner with the request.
            if(bannerView != null)
                bannerView.LoadAd(request);

    }
    public static void HideBannerAdv()
    {

        bannerView.Hide();
    }

 

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");

    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
        HeaderTextScript.iAdvertisePlaying = 5;
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");

    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        HeaderTextScript.iAdvertisePlaying = 5;
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
        HeaderTextScript.iAdvertisePlaying = 5;
    }
}
