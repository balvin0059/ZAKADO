using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Heater.Admob;
using GoogleMobileAds.Api;

public class Admobs : MonoBehaviour {
    public string[] ADMOB_ID;
    public bool isTest;

    void Start()
    {
        Ads();
    }
    void Ads()
    {
        // Test Device
        AdRequest.Builder builder = new AdRequest.Builder();
        builder.AddTestDevice(AdmobHelper.DeviceId);

        // Create
        BannerView m_banner = new BannerView(ADMOB_ID[0], AdSize.Banner, AdPosition.Top);
        m_banner.LoadAd(builder.Build());

        //InterstitialAd m_interstitial = new InterstitialAd(ADMOB_ID[1]);
        //m_interstitial.LoadAd(builder.Build());
        // Visibility
        m_banner.Show();
    }
}
