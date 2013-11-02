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
	[MenuItem("Assets/AnimationData/SetTransFormOnly")]
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
	[MenuItem("Tools/SmoothMove/AnimationData/TransFormOnly_SetTestImage(Need BoneAnimation)")]
	public static void SetAnimationData_TransFormOnly_CreateImage()
	{
		//SetAnimationData_TransFormOnly();
		if (Selection.activeGameObject == null)
			return;
		BoneAnimation boneData = Selection.activeGameObject.GetComponent<BoneAnimation>();
		TextureAtlas atlas = UnityEngine.Resources.Load("Atlas/RedKappa", typeof(ScriptableObject)) as TextureAtlas;
		if (atlas == null)
		{
			Debug.Log(atlas);
			return;

		}
		
		for (int i = 0; i < GLOBALCONST.BONE_NAME.Length; i++)
		{
			string boneName = GLOBALCONST.BONE_NAME[i];
			Debug.Log(boneName);
			Transform spriteTrans = boneData.GetSpriteTransform(boneName);
			if (spriteTrans == null)
				continue;
			Sprite sprite = spriteTrans.GetComponent<SmoothMoves.Sprite>();

			if (sprite == null)
				sprite = spriteTrans.gameObject.AddComponent<Sprite>();
			if (atlas.GetTextureIndex(atlas.GetTextureGUIDFromName(boneName)) >= 0)
			{
				sprite.atlas = atlas;
				sprite.textureGUID = atlas.GetTextureGUIDFromName(boneName);
			}
			
			sprite.useDefaultPivot = true;
		}
	}

	[MenuItem("Tools/SmoothMove/AnimationData/Play All Animation(Need BoneAnimation)")]
	public static void Play_All_Animation()
	{
		if (Selection.activeGameObject == null)
			return;
		BoneAnimation boneData = Selection.activeGameObject.GetComponent<BoneAnimation>();
		if (boneData == null)
			return;
		for (int i = 0; i < boneData.GetClipCount(); i++)
		{
			AnimationState AS =	boneData.PlayQueued(i);
			AS.wrapMode = WrapMode.Once;
			AS.speed = 1;
		}
	}
	[MenuItem("Assets/AnimationData/SetTestImage")]
	public static void SetAnimationData_Image()
	{
		SetAnimationData_Image( Selection.activeObject as BoneAnimationData,"NinJaBlack");
	}
	public static void SetAnimationData_Image(BoneAnimationData baData, string name)
	{
		TextureAtlas ta = UnityEngine.Resources.Load("Atlas/" + name, typeof(ScriptableObject)) as TextureAtlas;
		SetAnimationData_Image(baData, ta);
	}
	public static void SetAnimationData_Image(BoneAnimationData baData,TextureAtlas atlas)
	{
		if (baData && atlas)
		{
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
						acb.keyframes[0].atlas = atlas;
						acb.keyframes[0].textureGUID = atlas.GetTextureGUIDFromName(baData.boneDataList[acb.boneDataIndex].boneName);
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
		//Keyframe的depth 0 是最上層，1往後一層依此類推
		BoneAnimationData baData = Selection.activeObject as BoneAnimationData;
		if (baData)
		{
			//找每個clip裡的
			for (int animIndex = 0; animIndex < baData.AnimationClipCount; animIndex++)
			{
				//每個bone
				for (int boneIndex = 0; boneIndex < baData.BoneCount; boneIndex++)
				{
					//boneData這裡有才有keyframe
					AnimationClipBone clipBone = baData.GetAnimationClipBoneFromBoneDataIndex(animIndex, boneIndex);
					//取parent的boneData，有parent的話，z座標要轉換成相對的值
					AnimationClipBone parentClipBone = GetParentClipBone(baData,animIndex, baData.boneDataList[clipBone.boneDataIndex].boneName);
					
					//修改keyframe的zpos
					//depth 0 zpos 為0，1 為 0.1f
					foreach (KeyframeSM key in clipBone.keyframes)
					{
						//沒有設depth為關鍵影格的話也把zpos的關鍵影格關了，好像有些情況會有問題
						if (key.useDepth == false)
						{
							key.localPosition3.useZ = false;
							continue;
						}

						int depthDiffer = key.depth;
						if (parentClipBone != null)
						{
							KeyframeSM parentKey = parentClipBone.GetKeyframe(key.frame);
							//沒有parentKey的話depth會算錯，所以要往前取
							//GetPreviousKeyframe 0 跟 1都是往後，依這個名稱看來一定會取到keyfram，除非整個bone都沒有keyframe
							if(parentKey == null)
								parentKey = parentClipBone.GetPreviousKeyframe(key.frame, -1);
							//如果還是沒有應該就是root是parent，parent都沒有keyframe
							if (parentKey != null)
							{
								//計算parent的depth
								//如果parent是5，this是3的話，應該要是世界座標的0.3 parent為0.5，所以local是-0.2
								depthDiffer = key.depth - parentKey.depth;
							}
						}
						
						key.localPosition3.val.z = 0.1f * depthDiffer;
					}
					//最後一格的zpos要開著，確保最少一個zpos是可用的，這樣動畫才不會有問題
					if (clipBone.keyframes.Count > 0)
					{
						//最後一格如果沒用depth也把座標z設為跟第一格一樣
						if (clipBone.keyframes[clipBone.keyframes.Count - 1].useDepth == false)
							clipBone.keyframes[clipBone.keyframes.Count - 1].localPosition3.val.z = clipBone.keyframes[0].localPosition3.val.z;
						clipBone.keyframes[clipBone.keyframes.Count - 1].localPosition3.useZ = true;
					}

				}
			}
		}
	}

	static AnimationClipBone GetParentClipBone(BoneAnimationData baData,int animIndex, string boneName)
	{
		if (baData == null || string.IsNullOrEmpty(boneName))
			return null;
		string parentName = GetParentBoneName(baData, boneName);
		if (string.IsNullOrEmpty(parentName))
			return null;
		return baData.GetAnimationClipBoneFromBoneDataIndex(animIndex, baData.GetBoneDataIndexFromName(parentName, true));
		
	}
	static  string GetParentBoneName(BoneAnimationData baData, string boneName)
	{
		if(baData == null || string.IsNullOrEmpty(boneName))
			return "";

		string boneFullPath = "";
		foreach (string path in baData.boneTransformPaths)
			if (path.EndsWith(boneName))
				boneFullPath = path;
		if (string.IsNullOrEmpty(boneFullPath))
			return "";
		string[] nodes = boneFullPath.Split('/');
		if (nodes.Length > 1)
			return nodes[nodes.Length - 2];
		else
			return "";
	}
	[MenuItem("Assets/AnimationData/Filp X")]
	public static void SetAnimationData_Filp_X()
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
						acb.keyframes[i].localPosition3.val.x *= -1;
						acb.keyframes[i].localRotation.val *= -1;
					}
				}
			}
		}
	}
	//危險，使用時請小心，一次性使用後封印程式碼
	[MenuItem("Assets/AnimationData/OffsetBonePos")]
	public static void SetAnimationData_OffsetBonePos()
	{
		//名稱位移群組index要相對
		Vector3[] v3Group = new Vector3[]
		{
			//new Vector3(-17.04f, 13.36f, -102.40f),
			//new Vector3(-4.19f,1.47f,-108.11f),
			//new Vector3(-25.92f,112.08f,-275.08f),
			//new Vector3(-10.82f,8.63f,-106.99f),
		};
		string[] BoneNameGroup = new string[]
		{
			//"HandR",
			//"Head",
			//"LegL",
			//"LegR",
		};
		for (int index = 0; index < BoneNameGroup.Length && index < v3Group.Length; index++)
		{
			Vector3 v3 = v3Group[index];
			v3.z = 0; //不用改這個，以防萬一分開設為0
			string BoneName = BoneNameGroup[index];

			BoneAnimationData baData = Selection.activeObject as BoneAnimationData;
			if (baData)
			{
				foreach (AnimationClipSM acsm in baData.animationClips)
				{
					foreach (AnimationClipBone acb in acsm.bones)
					{
						if (BoneName == baData.boneDataList[acb.boneDataIndex].boneName)
						{
							for (int i = 0; i < acb.keyframes.Count; i++)
							{
								acb.keyframes[i].localPosition3.val += v3;
							}
						}
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

	[MenuItem("Assets/SmoothAtlas/FlipPivotOffsetX")]
	public static void FlipPivotOffsetX()
	{
		TextureAtlas ta = Selection.activeObject as TextureAtlas;
		if (ta)
		{
			for (int i = 0; i < ta.textureNames.Count; i++)
			{
				Vector2 temp = ta.defaultPivotOffsets[i];
				temp.x *= -1;
				ta.defaultPivotOffsets[i] = temp;
			}
		}
	}
}
