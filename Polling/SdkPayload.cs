using UnityEngine;

namespace Polling {

    public class SdkPayload
    {
        public RequestIdentification requestIdentification;
        public CallbackHandler callbackHandler;
        public bool disableAvailableSurveysPoll;

#if UNITY_ANDROID
        public AndroidJavaObject sdkPayload;


        public SdkPayload(RequestIdentification requestIdentification, CallbackHandler callbackHandler, bool disableAvailableSurveysPoll)
        {
            this.requestIdentification = requestIdentification;
            this.callbackHandler = callbackHandler;
            this.disableAvailableSurveysPoll = disableAvailableSurveysPoll;

            sdkPayload = JavaBridge.SdkPayload(requestIdentification.requestIdentification, callbackHandler.callbackHandler, disableAvailableSurveysPoll);
        }

#elif UNITY_IOS
        public SdkPayload(RequestIdentification requestIdentification, CallbackHandler callbackHandler, bool disableAvailableSurveysPoll)
        {
	    this.requestIdentification = requestIdentification;
            this.callbackHandler = callbackHandler;
            this.disableAvailableSurveysPoll = disableAvailableSurveysPoll;
        }
#endif
    }

}
