using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polling
{
    [System.Serializable]
    public class Reward
    {
        public string reward_amount;
        public string reward_name;
        public string complete_extra_json;

        public static Reward Deserialize(string jsonArray)
        {
            var rewards = JsonUtility.FromJson<Reward>(jsonArray);
            return rewards;
        }
    }
}
