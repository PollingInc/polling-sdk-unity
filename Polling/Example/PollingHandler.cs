
using Polling;
using UnityEngine;

public class PollingHandler : MonoBehaviour
{
    public string customerId;
    private string apiKey;

    public string surveyUuid; //testing surveys directly from serialized field  when triggering ShowSurvey()

    Polling.Polling polling;

    void Start()
    {
        customerId = "unityTest";
        apiKey = "myApiKey0000000...";

        RequestIdentification request = new RequestIdentification(customerId, apiKey);
        CallbackHandler callbacks = new CallbackHandler(this.gameObject, OnSuccess, OnFailure, OnReward, OnSurveyAvailable);

        SdkPayload sdkPayload = new SdkPayload(request, callbacks, false);

        polling = new Polling.Polling();
        polling.Initialize(sdkPayload);
    }

    //----------------------------------------------------------------------------------------------------------------

    public void ChangeIdentification()
    {
        customerId = "unityTest2";
        apiKey = "myApiKey1111111...";

        polling.SetCustomerId(customerId);
        polling.SetApiKey(apiKey);

    }



    //----------------------------------------------------------------------------------------------------------------
    private void OnSuccess(string response)
    {
        Debug.Log("SUCCESS (Unity): " + response);
    }

    private void OnFailure(string error)
    {
        Debug.Log("ERROR (Unity): " + error);
    }

    private void OnReward(string response)
    {
        Debug.Log("REWARD (Unity): " + "JSON - " + response);

        Reward reward = Reward.Deserialize(response);
        HandleReward(reward);
    }

    private void OnSurveyAvailable()
    {
        Debug.Log("(Unity) There is a survey available.");
    }

    //----------------------------------------------------------------------------------------------------------------
    private void HandleReward(Reward reward)
    {
        Debug.Log($"(Unity) Reward: {reward.reward_name} | {reward.reward_amount}");

        bool valueSuccess = int.TryParse(reward.reward_amount, out int amount);

        if (valueSuccess)
        {
            Debug.Log($"(Unity) Reward converted: {reward.reward_name} | {amount}");
        }

        if(reward.complete_extra_json != null)
        {
            Debug.Log($"(Unity) Reward extra json: {reward.complete_extra_json}");
        }
        
    }

    //----------------------------------------------------------------------------------------------------------------

    public void PopupSurveyBottom()
    {
        polling.SetViewType(ViewType.Bottom);
        polling.ShowEmbedView();
    }
    public void PopupSurveyDialog()
    {
        polling.SetViewType(ViewType.Dialog);
        polling.ShowEmbedView();
    }

    public void ShowSurvey()
    {
        polling.ShowSurvey(surveyUuid);
    }

    public void LogEvent()
    {
        polling.LogEvent("unityTest", 1);
    }

    public void LogPurchase()
    {
        polling.LogPurchase(1000); //10USD
    }

    public void LogSession()
    {
        polling.LogSession();
    }



}