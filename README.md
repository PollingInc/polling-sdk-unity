# Polling.com Unity SDK Wrapper

## Introduction

Polling.com Unity SDK Wrapper is a C# wrapper for the [Poling.com Java SDK](https://github.com/PollingInc/polling-sdk)

This SDK allows you to send events, log sessions and purchases, display embedded survey pages or show a specific survey seamlessly within your Unity application.

Polling SDK provides an easy way to integrate polling functionality into your Android projects. This guide walks you through integrating the `.aar` package and initializing the SDK.

---

## Installation

### Step 1: Unity package setup
1. Drag and drop the latest [.unitypackage](https://github.com/PollingInc/polling-sdk-unity/releases) in your Unity's Assets directory.
2. Select all files to be imported and them click on `Import`

---

### Step 2: Setup your Polling Handler and learn what you can do with SDK
Inside Assets/Polling/Example/ you should find a PollingHandler.cs script.
This is a handler example that you can copy and create your own custom handler by creating a new class based on it with your specific needs and use of codebase from your own project.
Check **Step 3** for more info.

There is also a project example you can find, which is our [Clicker Game example](https://github.com/PollingInc/GameconClicker-polling-sdk) to demonstrate how the Polling SDK Unity Wrapper works.

---
1. For Unity's Android implementation, you should explore the documetation in the [Java SDK](https://github.com/PollingInc/polling-sdk/blob/master/README.md) as the C# wrapper mirrors it.
   You will not be interacting with Java code directly, so no need to panic.

   All the concepts about each of the items listed below are described there, so check it out before proceeding or in case you don't understand any of the classes/objects listed below.
   

3. Anyways, here are some differences you may see in C#:

   (Before starting this, make sure your handler includes `using Polling;`

#### 2.1 RequestIdentification
This is actually unaltered, it's the same as Java syntax:
```C#
RequestIdentification requestIdentification = new RequestIdentification(customerId, apiKey);
```

#### 2.2 CallbackHandler
You don't need to override anything here but just reference functions to be your callbacks. Here's how it goes:
```C#
CallbackHandler callbackHandler = new CallbackHandler(this.gameObject, OnSuccess, OnFailure, OnReward, OnSurveyAvailable);

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
```
#### 2.3 SdkPayload
For this wrapper, in comparison to Java, you don't need to reference any Activity as Unity is contained into an Activity:
```C#
SdkPayload sdkPayload = new SdkPayload(request, callbacks, false);
```
---
#### 2.4 Initialize()
Finally, we can initialize the SDK similarly to how it's done in Java, but with a changing that Polling class is referenced as `Polling.Polling`:
```C#
Polling.Poliing polling = new Polling.Polling();
polling.Initialize(sdkPayload);
```
Again, it's recommended to keep `polling` variable in a global scope of your class, so you can use it anywhere.

---
#### 2.5 View Types
For Android, the view types for the surveys are the same as Java as it is actually a Java WebView being rendered here.
What changes here is the way they are set:
Only using `polling.SetViewType(ViewType viewType)`

Just as a reminder what are the two types of survey views:

* **Dialog:** Opens like a centered popup in the middle of the screen as a square that keeps its edges free to view the background.
* **Bottom:** Opens like a sheet in the bottom of the screen. Uses more screen and occupies full-width with no edges.
---

### Step 3: Use the SDK:
These are the function you can use:
`polling.LogSession()` - Logs a simple Session event for the given user
`polling.LogPurchase(int integerCents)` - Logs a Purchase event for the given user with the amount in cents
`polling.LogEvent(String eventName, String | int eventValue)` - Sends a custom event name and value - NOTE: This method is only available for Business+ plans.
`polling.ShowEmbedView()` - Opens the Polling.com embed view popup, which will show the user's surveys (list of surveys, random or a fixed survey depending on the user's settings)
`polling.ShowSurvey(String surveyUuid)` - Opens a popup with a specific survey by its UUID
`polling.SetApiKey(String apiKey)` - Changes the API key on the fly, useful if you want to handle multiple embeds with a single SDK instance
`polling.SetCustomerId(String customerId)` - Changes the Customer ID on the fly
`polling.SetViewType(ViewType viewType)` - Changes the View type mode on the fly

---
### Usage example
Finally, here's a full code example, which is the `PollingHandler.cs` you find in Assets/Polling/Example/ as mentioned earlier:

```C#

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
```