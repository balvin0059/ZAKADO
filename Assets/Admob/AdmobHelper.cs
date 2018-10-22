using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Heater.Admob
{
    public class AdmobHelper
    {
        private static string MD5Hash( string s )
        {
            var encoding = new UTF8Encoding();
            var md5 = new MD5CryptoServiceProvider();

            var bytes = encoding.GetBytes( s );
            var hashBytes = md5.ComputeHash( bytes );

            var hashString = string.Empty;
            foreach ( var bit in hashBytes )
            {
                hashString += Convert.ToString( bit, 16 ).PadLeft( 2, '0' );
            }

            return hashString.PadLeft( 32, '0' );
        }

        public static string DeviceId
        {
            get
            {
#if UNITY_EDITOR

                return SystemInfo.deviceUniqueIdentifier;

#elif UNITY_ANDROID

			    AndroidJavaClass jc = new AndroidJavaClass( "com.unity3d.player.UnityPlayer" );
			    AndroidJavaObject currentActivity = jc.GetStatic<AndroidJavaObject>( "currentActivity" );
			    AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject>( "getContentResolver" );
			    AndroidJavaObject secure = new AndroidJavaObject( "android.provider.Settings$Secure" );
			    string deviceID = secure.CallStatic<string>( "getString" , contentResolver, "android_id" );
			    return MD5Hash( deviceID ).ToUpper();

#elif UNITY_IOS

			    return MD5Hash( UnityEngine.iOS.Device.advertisingIdentifier );

#else
			    return SystemInfo.deviceUniqueIdentifier;

#endif
            }
        }
    }
}
