using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Polling
{

    public enum ViewType
    {
        None = 0,
        Dialog = 1,
        Bottom = 2
    }


    public class Polling : IPolling
    {

#if UNITY_ANDROID
        AndroidJavaObject polling;

        public Polling()
        {
            polling = JavaBridge.Polling();
        }

        public bool IsTargetedPlatformCompatible()
        {
            return true;
        }

        public void Initialize(SdkPayload sdkPayload)
        {
            polling.Call("initialize", sdkPayload.sdkPayload);
        }


        public void SetCustomerId(string customerId)
        {
            polling.Call("setCustomerId", customerId);
        }

        public void SetApiKey(string apiKey)
        {
            polling.Call("setApiKey", apiKey);
        }

        public void SetViewType(ViewType viewType)
        {
            polling.Call("setViewType", viewType.ToString());
        }



        public void LogPurchase(int integerCents)
        {
            polling.Call("logPurchase", integerCents);
        }


        public void LogSession()
        {
            polling.Call("logSession");
        }


        public void LogEvent(string eventName, string eventValue)
        {
            polling.Call("logEvent", eventName, eventValue);
        }

        public void LogEvent(string eventName, int eventValue)
        {
            polling.Call("logEvent", eventName, eventValue.ToString());
        }

        public void ShowSurvey(string surveyUuid)
        {
            polling.Call("showSurvey", surveyUuid, JavaBridge.UnityActivity());
        }

        public void ShowEmbedView()
        {
            polling.Call("showEmbedView", JavaBridge.UnityActivity());
        }

        /*
        public List<string> GetLocalSurveyResults(string surveyUuid)
        {
            return localStorage.getItem(surveyUuid);
        }
        */
#elif UNITY_IOS

        public bool IsTargetedPlatformCompatible()
        {
            return true;
        }

        public void Initialize(SdkPayload sdkPayload)
        {
            RequestIdentification req = sdkPayload.requestIdentification;
            CallbackHandler cbs = sdkPayload.callbackHandler;
            bool disableSurveyPoll = sdkPayload.disableAvailableSurveysPoll;

            string targetGameObjectName = cbs.gameObject.name;
            string onSuccessName = cbs.onSuccess.Method.Name;
            string onFailureName = cbs.onFailure.Method.Name;
            string onRewardName = cbs.onReward.Method.Name;
            string onSurveyAvailableName = cbs.onSurveyAvailable.Method.Name;
            ObjCBridge.POLUnityPluginConfigureCallbacks(
                targetGameObjectName, onSuccessName, onFailureName,
                onRewardName, onSurveyAvailableName);
            ObjCBridge.POLUnityPluginInitialize(req._CustomerID, req._APIKey);
            ObjCBridge.POLUnityPluginSetDisableAvailableSurveysPoll(disableSurveyPoll);
        }

        public void LogEvent(string eventName, string eventValue)
        {
            ObjCBridge.POLUnityPluginLogEvent(eventName, eventValue);
        }

        public void LogEvent(string eventName, int eventValue)
        {
            ObjCBridge.POLUnityPluginLogEvent(eventName, eventValue.ToString());
        }

        public void LogPurchase(int integerCents)
        {
            ObjCBridge.POLUnityPluginLogPurchase(integerCents);
        }

        public void LogSession()
        {
            ObjCBridge.POLUnityPluginLogSession();
        }

        public void SetApiKey(string apiKey)
        {
            ObjCBridge.POLUnityPluginSetApiKey(apiKey);
        }

        public void SetCustomerId(string customerId)
        {
            ObjCBridge.POLUnityPluginSetCustomerId(customerId);
        }

        public void SetViewType(ViewType viewType)
        {
            ObjCBridge.POLUnityPluginSetViewType((int)viewType);
        }

        public void ShowEmbedView()
        {
            ObjCBridge.POLUnityPluginShowEmbedView();
        }

        public void ShowSurvey(string surveyUuid)
        {
            ObjCBridge.POLUnityPluginShowSurvey(surveyUuid);
        }
#else
        public bool IsTargetedPlatformCompatible()
        {
            return false;
        }

        public void Initialize(SdkPayload sdkPayload)
        {
            NotImplementedWarning();
        }

        public void LogEvent(string eventName, string eventValue)
        {
            NotImplementedWarning();
        }

        public void LogEvent(string eventName, int eventValue)
        {
            NotImplementedWarning();
        }

        public void LogPurchase(int integerCents)
        {
            NotImplementedWarning();
        }

        public void LogSession()
        {
            NotImplementedWarning();
        }

        public void SetApiKey(string apiKey)
        {
            NotImplementedWarning();
        }

        public void SetCustomerId(string customerId)
        {
            NotImplementedWarning();
        }

        public void SetViewType(ViewType viewType)
        {
            NotImplementedWarning();
        }

        public void ShowEmbedView()
        {
            NotImplementedWarning();
        }

        public void ShowSurvey(string surveyUuid)
        {
            NotImplementedWarning();
        }

#endif

        public static void NotImplementedWarning()
        {
            Debug.LogWarning("Polling SDK is not implemented for the targeted platform.");
        }

    }
}
