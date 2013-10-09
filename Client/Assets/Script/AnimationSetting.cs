using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SmoothMoves;
public class AnimationData
{
	static Dictionary<AnimationClipSM_Lite, Dictionary<string, int>> triggerEventData = new Dictionary<AnimationClipSM_Lite, Dictionary<string, int>>();

	public static Dictionary<string, int> GetAnimClipTriggerEventCounts(BoneAnimation anim,string clipName)
	{
		if (anim == null)
			return new Dictionary<string, int>();
		if (!anim.AnimationClipExists(clipName))
			return new Dictionary<string, int>();

		AnimationClipSM_Lite clip = anim.mAnimationClips[anim.GetAnimationClipIndex(clipName)];
		Dictionary<string, int> result;
		if (!triggerEventData.ContainsKey(clip))
			GenerateTriggerEventInfo(anim);
		if (triggerEventData.TryGetValue(clip, out result))
			return result;
		else
			return new Dictionary<string, int>();
	}

	//取得clip中所有eventTag的數量
	public static int GetAnimClipTriggerEventCount(BoneAnimation anim, string clipName, string eventTag)
	{
		if (anim == null)
			return 0;
		Dictionary<string, int> eventCounts = GetAnimClipTriggerEventCounts(anim, clipName);
		int result;
		eventCounts.TryGetValue(eventTag, out result);
		return result;
	}

	//計算BoneAnimation中各別clip包含的userTriggerTag的數量
	static void GenerateTriggerEventInfo(BoneAnimation anim)
	{
		Dictionary<AnimationClipSM_Lite, Dictionary<string, int>> temp = new Dictionary<AnimationClipSM_Lite, Dictionary<string, int>>();
		foreach (TriggerFrame tf in anim.triggerFrames)
		{
			AnimationClipSM_Lite clip = anim.mAnimationClips[tf.clipIndex];
			Dictionary<string, int> eventData;
			if (!temp.TryGetValue(clip, out eventData))
			{
				eventData = new Dictionary<string, int>();
				temp.Add(clip, eventData);
			}
			
			foreach (TriggerFrameBone tfb in tf.triggerFrameBones)
			{
				if (!string.IsNullOrEmpty(tfb.userTriggerTag))
				{
					if (eventData.ContainsKey(tfb.userTriggerTag))
						eventData[tfb.userTriggerTag]++;
					else
						eventData.Add(tfb.userTriggerTag, 1);
				}
			}
		}

		foreach (KeyValuePair<AnimationClipSM_Lite, Dictionary<string, int>> kvp in temp)
		{
			if (triggerEventData.ContainsKey(kvp.Key))
				triggerEventData[kvp.Key] = kvp.Value;
			else
				triggerEventData.Add(kvp.Key, kvp.Value);
		}
	}
	
}

public struct AnimInfo
{
	public string clipName;
	public int times;
	public float totalTime;
}

public struct AnimationSetting
{
	public const string IDLE_ANIM = "Idle";
	public const string WALK_ANIM = "Walk";

	public const string HIT_TAG = "Damage";
	public const string ATKSTART_TAG = "AttackStart";
	public const string ATKEND_TAG = "AttackEnd";
	public const string START_TAG = "Start";
	public const string END_TAG = "End";
}