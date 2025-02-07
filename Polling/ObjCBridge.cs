using UnityEngine;
using System.Runtime.InteropServices;

namespace Polling
{
#if UNITY_IPHONE
    public class ObjCBridge {
        [DllImport ("__Internal")]
        public static extern void POLUnityPluginConfigureCallbacks(
            string target, string success, string failure,
            string reward, string surveryAvailable);

        [DllImport ("__Internal")]
        public static extern void POLUnityPluginInitialize(string customerId, string apiKey);

        [DllImport ("__Internal")]
        public static extern void POLUnityPluginLogEvent(string eventName, string eventValue);

        [DllImport ("__Internal")]
        public static extern void POLUnityPluginLogPurchase(int integerCents);

        [DllImport ("__Internal")]
        public static extern void POLUnityPluginLogSession();

        [DllImport ("__Internal")]
        public static extern void POLUnityPluginSetApiKey(string apiKey);

        [DllImport ("__Internal")]
        public static extern void POLUnityPluginSetCustomerId(string customerId);

        [DllImport ("__Internal")]
        public static extern void POLUnityPluginSetViewType(int viewType);

        [DllImport ("__Internal")]
        public static extern void POLUnityPluginShowEmbedView();

        [DllImport ("__Internal")]
        public static extern void POLUnityPluginShowSurvey(string surveyUuid);
    }
#endif
}
