namespace Polling
{
    public interface IPolling
    {
        public void Initialize(SdkPayload sdkPayload);
        public void SetCustomerId(string customerId);
        public void SetApiKey(string apiKey);
        public void SetViewType(ViewType viewType);
        public void LogPurchase(int integerCents);
        public void LogSession();
        public void LogEvent(string eventName, string eventValue);
        public void LogEvent(string eventName, int eventValue);
        public void ShowSurvey(string surveyUuid);
        public void ShowEmbedView();



    }
}

