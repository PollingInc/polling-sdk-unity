using System.Collections;
using System.Collections.Generic;
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
        public void Initialize(SdkPayload sdkPayload)
        {
            //TO BE IMPLEMENTED
        }

        public void LogEvent(string eventName, string eventValue)
        {
            //TO BE IMPLEMENTED
        }

        public void LogEvent(string eventName, int eventValue)
        {
            //TO BE IMPLEMENTED
        }

        public void LogPurchase(int integerCents)
        {
            //TO BE IMPLEMENTED
        }

        public void LogSession()
        {
            //TO BE IMPLEMENTED
        }

        public void SetApiKey(string apiKey)
        {
            //TO BE IMPLEMENTED
        }

        public void SetCustomerId(string customerId)
        {
            //TO BE IMPLEMENTED
        }

        public void SetViewType(ViewType viewType)
        {
            //TO BE IMPLEMENTED
        }

        public void ShowEmbedView()
        {
            //TO BE IMPLEMENTED
        }

        public void ShowSurvey(string surveyUuid)
        {
            
        }
#endif
    }
}