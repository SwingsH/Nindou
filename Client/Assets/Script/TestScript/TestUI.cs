using UnityEngine;
using System.Collections;

public class TestUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(210, 10, 100, 40), Time.timeScale.ToString()))
		{
			TimeMachine.SetTimeScale(Time.timeScale == 1 ? 5 : (Time.timeScale == 5 ? 10 : 1));
		}
		if (BattleManager.Instance != null )
		{
			if (BattleManager.IsBattleStart)
			{
				#region 臨時大招施放按鈕

				if (BattleManager.Instance.Players != null)
				{
					Rect r = new Rect(10, Screen.height * 0.85f, Screen.width * 0.25f, Screen.height * 0.15f);
					for (int i = 0; i < BattleManager.Instance.Players.Length; i++)
					{
						if (BattleManager.Instance.Players[i] == null)
							continue;
						ActionUnit au = BattleManager.Instance.Players[i] as ActionUnit;
						r.x = i * r.width;
                        // 技能使用
						if (au.ExtrimSkill != null)
						{
							string content = au.ExtrimSkill.Name;
							if (au.ExtrimSkill.Castable)
							{
								if (GUI.Button(r, content))
									au.CastExtrimSkill();
							}
							else
							{
								GUIStyle gs = GUI.skin.label;
								gs.alignment = TextAnchor.MiddleCenter;
								GUI.Label(r, content, gs);
							}
						}
					}
				}
			#endregion
			}
			else
			{
				uint battleID = 0;
				for (uint i = 1; i <= 3; i++)
				{
					if (GUI.Button(new Rect(Screen.width / 2 - 160, Screen.height / 2 - 3 * 40 + (i -1) * 80, 80, 80), "Battle" + i.ToString()))
					{
						battleID = i;
					}
				}
				if (battleID != 0)
				{
					BattleState.BattleID = battleID;
					GameControl.Instance.ChangeGameState(BattleState.instance);
				}
			}
		}
#if UNITY_EDITOR
		if (UnityEditor.Selection.activeGameObject)
		{
			GUI.Label(new Rect(610, 10, 100, 40), UnityEditor.Selection.activeGameObject.GetInstanceID().ToString());
		}

		if (UnityEditor.Selection.activeGameObject && UnityEditor.Selection.activeGameObject.GetComponent<SmoothMoves.BoneAnimation>() != null)
		{
			SmoothMoves.BoneAnimation anim = UnityEditor.Selection.activeGameObject.GetComponent<SmoothMoves.BoneAnimation>();
			if (GUI.Button(new Rect(510, 10, 100, 40), "Play Walk"))
			{
				anim.CrossFade("Walk");
			}
			if (GUI.Button(new Rect(510, 50, 100, 40), "Stop Walk"))
			{
				anim.CrossFade("Idle");
			}
			if (GUI.Button(new Rect(510, 90, 100, 40), "Play Atk 3"))
			{
				anim.PlayQueued("Attack2");
			}
			if (GUI.Button(new Rect(510, 130, 100, 40), "Walk Speed"))
			{
				anim["Walk"].speed *= 2;
			}
			if (GUI.Button(new Rect(510, 170, 100, 40), "Walk normalizedSpeed"))
			{
				anim["Walk"].normalizedSpeed *= 2;
			}
			int line = 0;

			foreach (AnimationState AS in UnityEditor.Selection.activeGameObject.animation)
			{
				GUI.Box(new Rect(610, 10 + line, 300, 40), AS.name + " " + anim.IsPlaying(AS.name) + "\n " + AS.speed.ToString() + " " + AS.normalizedSpeed.ToString() + " " + AS.length.ToString() + " " + AS.time.ToString());
				line += 30;
			}
		}
#endif
	}

}
