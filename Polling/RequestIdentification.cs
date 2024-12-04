using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polling { 

    public class RequestIdentification
    {
        private string customerId;
        private string apiKey;

#if UNITY_ANDROID
        public AndroidJavaObject requestIdentification;

        public RequestIdentification(string customerId, string apiKey)
        {
           var javaObj = JavaBridge.RequestIdentification(customerId, apiKey);

           this.customerId = javaObj.Get<string>("customerId");
           this.apiKey = javaObj.Get<string>("apiKey");

           requestIdentification = javaObj;
        }
#elif UNITY_IOS
        public RequestIdentification(string customerId, string apiKey)
        {
            //TO BE IMPLEMENTED
        }
#endif


    }

}
