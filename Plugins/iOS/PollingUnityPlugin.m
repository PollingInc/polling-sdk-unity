/*
 *  PollingUnityPlugin.m
 *  Polling Unity Plugin for iOS
 *
 *  Copyright © 2024 Polling.com. All rights reserved
 */

#import <Polling/Polling.h>


#if !defined(POL_EXTERN_C_BEGIN)
#if defined(__cplusplus)
#define POL_EXTERN_C_BEGIN extern "C" {
#define POL_EXTERN_C_END   }
#else
#define POL_EXTERN_C_BEGIN
#define POL_EXTERN_C_END
#endif
#endif

#if !defined(POL_INLINE)
#if defined(__GNUC__) && (__GNUC__ == 4) && !defined(DEBUG)
#define POL_INLINE static __inline__ __attribute__((always_inline))
#elif defined(__GNUC__)
#define POL_INLINE static __inline__
#elif defined(__cplusplus)
#define POL_INLINE static inline
#endif
#endif


POL_INLINE char *POLCopyInCStr(char *inCStr)
{
    return strdup(inCStr);
}

POL_INLINE char *POLCopyOutCStr(char *outCStr) {
    return strdup(outCStr);
}

POL_INLINE NSString *POLCopyInString(char *inCStr)
{
    return [NSString stringWithUTF8String:inCStr ? inCStr : ""];
}

POL_INLINE char *POLCopyOutString(NSString *outString)
{
    return strdup(outString.UTF8String);
}

@interface POLUnityPluginController : NSObject <POLPollingDelegate>
- init NS_UNAVAILABLE;
+ (instancetype)pluginController;

- (void)configureCallbacksWithTarget:(char *)target
    success:(char *)success failure:(char *)failure
    reward:(char *)reward surveyAvailable:(char *)surveyAvailable;
@end

@implementation POLUnityPluginController {
    char *_target;
    char *_success;
    char *_failure;
    char *_reward;
    char *_survey;
}

+ (instancetype)pluginController
{
	static POLUnityPluginController *polController;
	static dispatch_once_t onceToken;
	dispatch_once(&onceToken, ^{
		polController = POLUnityPluginController.new;
	});
	return polController;
}

- (void)configureCallbacksWithTarget:(char *)target
    success:(char *)success failure:(char *)failure
    reward:(char *)reward surveyAvailable:(char *)surveyAvailable
{
    if (target)
        _target = POLCopyInCStr(target);
    if (success)
        _success = POLCopyInCStr(success);
    if (failure)
        _failure = POLCopyInCStr(failure);
    if (reward)
        _reward = POLCopyInCStr(reward);
    if (surveyAvailable)
        _survey = POLCopyInCStr(surveyAvailable);
}

- (void)pollingOnSuccess:(NSString *)response
{
    if (!_target || !_success)
        return;

    char *param = POLCopyOutString(response);
    UnitySendMessage(_target, _success, param);
}

- (void)pollingOnFailure:(NSString *)error
{
    if (!_target || !_failure)
        return;

    char *param = POLCopyOutString(error);
    UnitySendMessage(_target, _failure, param);
}

- (void)pollingOnReward:(POLReward *)reward
{
    if (!_target || !_reward)
        return;

    NSDictionary *dict = @{
		@"reward_name": reward.name,
		@"reward_amount": reward.amount,
	};
	NSData *data = [NSJSONSerialization dataWithJSONObject:dict options:0 error:nil];
	NSString *json = [NSString.alloc initWithData:data encoding:NSUTF8StringEncoding];

    char *param = POLCopyOutString(json);
    UnitySendMessage(_target, _reward, param);
}

- (void)pollingOnSurveyAvailable
{
    if (!_target || !_survey)
        return;

    char *param = POLCopyOutCStr("");
    UnitySendMessage(_target, _survey, param);
}

@end


POL_EXTERN_C_BEGIN

void POLUnityPluginConfigureCallbacks(char *target, char *success,
    char *failure, char *reward, char *surveyAvailable)
{
    POLUnityPluginController *pc = POLUnityPluginController.pluginController;
    [pc configureCallbacksWithTarget:target success:success
        failure:failure reward:reward surveyAvailable:surveyAvailable];
}

void POLUnityPluginInitialize(char *customerId, char *apiKey)
{
    POLPolling.polling.customerID = POLCopyInString(customerId);
    POLPolling.polling.apiKey = POLCopyInString(apiKey);
    POLPolling.polling.delegate = POLUnityPluginController.pluginController;
}

void POLUnityPluginLogEvent(char *eventName, char *eventValue)
{
    [POLPolling.polling logEvent:POLCopyInString(eventName) value:POLCopyInString(eventValue)];
}

void POLUnityPluginLogPurchase(int integerCents)
{
    [POLPolling.polling logPurchase:integerCents];
}

void POLUnityPluginLogSession()
{
    [POLPolling.polling logSession];
}

void POLUnityPluginSetApiKey(char *apiKey)
{
    POLPolling.polling.apiKey = POLCopyInString(apiKey);
}

void POLUnityPluginSetCustomerId(char *customerId)
{
    POLPolling.polling.customerID = POLCopyInString(customerId);
}

void POLUnityPluginSetViewType(int viewType)
{
    POLPolling.polling.viewType = (POLViewType)viewType;
}

void POLUnityPluginShowEmbedView()
{
    [POLPolling.polling showEmbedView];
}

void POLUnityPluginShowSurvey(char *uuid)
{
    [POLPolling.polling showSurvey:POLCopyInString(uuid)];
}

POL_EXTERN_C_END
