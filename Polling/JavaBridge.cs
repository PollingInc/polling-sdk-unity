using UnityEngine;

namespace Polling
{
#if UNITY_ANDROID
    internal static class JavaBridge
    {
        internal static AndroidJavaObject UnityActivity()
        {
            var unityActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            return unityActivity;
        }

        
        internal static AndroidJavaObject RequestIdentification(string customerId, string apiKey)
        {
            if (customerId == null || apiKey == null) return null;

            return new AndroidJavaObject("com.polling.sdk.core.models.RequestIdentification", customerId, apiKey);
        }


        internal static AndroidJavaObject CallbackHandler(string gameObjectName, string onSuccessName, string onFailureName, string onRewardName, string onSurveyAvailableName)
        {
            var callbackHandler = 
                new AndroidJavaObject("com.polling.sdk.core.models.UnityCallbackHandler", gameObjectName,
                    onSuccessName, onFailureName,
                    onRewardName, onSurveyAvailableName
                );

            return callbackHandler;
        }

        
        internal static AndroidJavaObject SdkPayload(AndroidJavaObject requestIdentification, AndroidJavaObject callbackHandler, bool disableAvailableSurveysPoll)
        {
            var unityActivity = UnityActivity();

            return new AndroidJavaObject("com.polling.sdk.SdkPayload", unityActivity, requestIdentification, callbackHandler, disableAvailableSurveysPoll);
        }

        internal static AndroidJavaObject Polling()
        {
            return new AndroidJavaObject("com.polling.sdk.Polling");
        }

    }

#endif

}