using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using System;

namespace Heater.Admob
{
    /// <summary>
    /// Example:
    /// m_admobManager = new AdmobManager();
    /// m_admobManager.RequestInterstitial( ADMOB_ID );
    /// m_admobManager.RequestBanner( ADMOB_ID, AdSize.Banner, AdPosition.Top );
    /// </summary>
    public class AdmobManager
    {
        private BannerView m_banner = null;
        private InterstitialAd m_interstitial = null;

        public void Clear()
        {
            if ( null != m_banner )
            {
                m_banner.Destroy();
            }
            if ( null != m_interstitial )
            {
                m_interstitial.Destroy();
            }
        }

        public bool IsShowBanner
        {
            set
            {
                if ( null == m_banner )
                {
                    return;
                }

                if ( value )
                {
                    m_banner.Show();
                }
                else
                {
                    m_banner.Hide();
                }
            }
        }

        public void RequestBanner( string id, AdSize size, AdPosition position, bool isTest = false )
        {
            if ( null != m_banner )
            {
                m_banner.Destroy();
            }

            var builder = CreateBuilder( isTest );
            m_banner = new BannerView( id, size, position );
            m_banner.LoadAd( builder.Build() );
        }

        public void RequestInterstitial( string id, bool isTest = false )
        {
            if ( null != m_interstitial )
            {
                m_interstitial.Destroy();
            }

            var builder = CreateBuilder( isTest );
            m_interstitial = new InterstitialAd( id );
            m_interstitial.LoadAd( builder.Build() );
            m_interstitial.OnAdLoaded += OnInterstitalLoaded;
        }

        private AdRequest.Builder CreateBuilder( bool isTest )
        {
            AdRequest.Builder builder = new AdRequest.Builder();
            if ( isTest )
            {
                builder.AddTestDevice( AdmobHelper.DeviceId );
            }

            return builder;
        }

        private void OnInterstitalLoaded( object sender, EventArgs args )
        {
            m_interstitial.Show();
        }
    }
}