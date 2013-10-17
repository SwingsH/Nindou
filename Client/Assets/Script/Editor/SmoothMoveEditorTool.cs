using UnityEngine;
using UnityEditor;
using SmoothMoves;
using Resources = UnityEngine.Resources;
using System.Collections;

public class SmoothMoveEditorTool {

	static object buffer;


	#region AnimationData
	[MenuItem("Assets/AnimationData/Set StartEnd Tag")]
	public static void SetTag()
	{
		BoneAnimationData baData = Selection.activeObject as BoneAnimationData;
		if (baData)
		{
			foreach (AnimationClipSM acsm in baData.animationClips)
			{
				AnimationClipBone acb = acsm.GetAnimationClipBone(baData.GetBoneDataIndexFromName(GLOBALCONST.BODY, true));

				if (acb.keyframes.Count >= 1)
				{
					acb.keyframes[0].userTriggerCallback = true;
					acb.keyframes[0].userTriggerTag = AnimationSetting.START_TAG;
				}
				if (acb.keyframes.Count > 1)
				{
					acb.keyframes[acb.keyframes.Count - 1].userTriggerCallback = true;
					acb.keyframes[acb.keyframes.Count - 1].userTriggerTag = AnimationSetting.END_TAG;
				}
			}
		}
	}
	[MenuItem("Assets/AnimationData/TransFormOnly")]
	public static void SetAnimationData_TransFormOnly()
	{
		BoneAnimationData baData = Selection.activeObject as BoneAnimationData;
		if (baData)
		{
			foreach (AnimationClipSM acsm in baData.animationClips)
			{
				foreach (AnimationClipBone acb in acsm.bones)
				{
					if (acb.keyframes.Count >= 1)
					{
						acb.keyframes[0].keyframeType = KeyframeSM.KEYFRAME_TYPE.TransformOnly;
					}
					for (int i = 0; i < acb.keyframes.Count; i++)
					{
						acb.keyframes[i].useKeyframeType = false;
						acb.keyframes[i].useAtlas = false;
						acb.keyframes[i].atlas = null;
						acb.keyframes[i].useTextureGUID = false;
						acb.keyframes[i].textureGUID = string.Empty;
						acb.keyframes[i].usePivotOffset = false;
					}
				}
			}
		}
	}
	[MenuItem("Assets/AnimationData/TestImage")]
	public static void SetAnimationData_Image()
	{
		BoneAnimationData baData = Selection.activeObject as BoneAnimationData;
		if (baData)
		{
			TextureAtlas ta = UnityEngine.Resources.Load("Atlas/RedKappa", typeof(ScriptableObject)) as TextureAtlas;
			foreach (AnimationClipSM acsm in baData.animationClips)
			{
				foreach (AnimationClipBone acb in acsm.bones)
				{
					if (acb.keyframes.Count >= 1)
					{
						acb.keyframes[0].keyframeType = KeyframeSM.KEYFRAME_TYPE.Image;
						acb.keyframes[0].useKeyframeType = true;
						acb.keyframes[0].useAtlas = true;
						acb.keyframes[0].useTextureGUID = true;
						acb.keyframes[0].atlas = ta;
						acb.keyframes[0].textureGUID = ta.GetTextureGUIDFromName(baData.boneDataList[acb.boneDataIndex].boneName);
					}
					for (int i = 1; i < acb.keyframes.Count; i++)
					{
						acb.keyframes[i].useKeyframeType = false;
						acb.keyframes[i].useAtlas = false;
						acb.keyframes[i].useTextureGUID = false;
						acb.keyframes[i].usePivotOffset = false;
					}
				}
			}
		}
	}

	[MenuItem("Assets/AnimationData/Set Z by Depth")]
	public static void SetAnimationData_ZPos()
	{
		BoneAnimationData baData = Selection.activeObject as BoneAnimationData;
		if (baData)
		{
			foreach (AnimationClipSM acsm in baData.animationClips)
			{
				foreach (AnimationClipBone acb in acsm.bones)
				{

					for (int i = 0; i < acb.keyframes.Count; i++)
					{
						acb.keyframes[i].localPosition3.val.z = 0.1f * (acb.keyframes[i].depth - 5);
					}
				}
			}
		}
	}
	#endregion

	
	[MenuItem("Assets/SmoothAtlas/CopyAtlasSetting")]
	public static void CopyAtlasSetting()
	{
		TextureAtlas ta = Selection.activeObject as TextureAtlas;
		if (ta)
			buffer = ta;
	}
	[MenuItem("Assets/SmoothAtlas/PastePivotOffsets")]
	public static void PastePivotOffsets()
	{
		TextureAtlas tabuffer = buffer as TextureAtlas;
		TextureAtlas ta = Selection.activeObject as TextureAtlas;
		if (ta && tabuffer)
		{
			for (int i = 0; i < tabuffer.textureNames.Count; i++)
			{
				for (int j = 0; j < ta.textureNames.Count; j++)
				{
					if (ta.textureNames[j].StartsWith(tabuffer.textureNames[i]))
					{
						ta.defaultPivotOffsets[j] = tabuffer.defaultPivotOffsets[i];

						break;
					}
				}
			}
		}
	}

}
