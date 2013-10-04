using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * 2013/8/16
 * 戰鬥單位出現跟消失和戰鬥結果管理 
 * 單位移動是個自移動，不過要透
 */
public class BattleManager : MonoBehaviour
{
	static BattleManager _instance;
	public static BattleManager Instance
	{
		get { return _instance; }
		protected set { _instance = value; }
	}

	public static Camera UnitCamera;
	GridInfo g;
	public GameObject groundObject;
	
	List<UnitInfo> PlayerInfos = new List<UnitInfo>();
	List<UnitInfo> EnemyInfos = new List<UnitInfo>();
	Unit[] Players = new Unit[0];
	List<Unit> Enemys = new List<Unit>();

	StageInfo stage;

	UnitGenerater Generater;
	
	void Awake()
	{
		if (Instance != null)
		{
			Debug.LogError("重複的BattleManager元件");
			Destroy(this);
			return;
		}
		Instance = this;
		if (UnitCamera == null)
		{
			UnitCamera = new GameObject("UnitCamera").AddComponent<Camera>();
		}
		if(Camera.main)
		{
			Camera.main.cullingMask = 1 << GLOBALCONST.GameSetting.LAYER_BACKGROUND;
			UnitCamera.transform.position = Camera.main.transform.position;
			UnitCamera.transform.rotation = Camera.main.transform.rotation;
		}
		UnitCamera.orthographic = true;
		UnitCamera.cullingMask = ~(1 << GLOBALCONST.GameSetting.LAYER_BACKGROUND);
		UnitCamera.clearFlags = CameraClearFlags.Depth;
		UnitCamera.orthographicSize = GLOBALCONST.GameSetting.UNIT_CAMERA_SIZE;
	}
	void Start()
	{
		if (Application.isPlaying)
			TestDataBase.IniInstance();
		if (groundObject)
		{
			if (groundObject.GetComponent<ColiderPosReciver>())
				groundObject.GetComponent<ColiderPosReciver>().Regiester(this);
			g = new GridInfo(groundObject.transform.position, GLOBALCONST.GameSetting.GRID_SIZE, GLOBALCONST.GameSetting.GRID_COUNT_W, GLOBALCONST.GameSetting.GRID_COUNT_L);
		}
		Generater = new UnitGenerater();
	}

	void LateUpdate()
	{
		foreach (Unit eu in Enemys)
			if(eu!=null)
				eu.Run();
		foreach (Unit eu in Players)
			if (eu != null)
				eu.Run();
	}

	void BattleStart()
	{
		
		BattleIsStart = true;
		Unit_Clear();
		EnemyInfos.Clear();
		PlayerInfos.Clear();

		PlayerInfos.AddRange(TestDataBase.Instance.playerInfo);
		UnitInfo info = new UnitInfo();
		info.MoveSpeed = 9;
		info.MoveMode = 0;
		info.MaxLife = 100;
		info.AttackID = 0;
		EnemyInfos.Add(info);
		info = new UnitInfo();
		info.MoveSpeed = 2;
		info.MoveMode = 1;
		info.MaxLife = 50;
		info.AttackID = 0;
		EnemyInfos.Add(info);
		if (Application.isPlaying)
		{
			IniPlayers();
			RandomEnemy(5, 0);
		}
	}

	void AddPlayer(GridPos pos, int index)
	{
		if (g.CheckEmpty(pos))
		{
			Unit su = Generater.GenerateUnit(PlayerInfos[index]);
			su.Group = eGroup.Player;
			su.Pos = pos;
			su.WorldPos = Get_GridWorldPos(pos);
			if (su.Entity != null)
				su.Entity.name += index;
			if (Players.Length <= index)
				System.Array.Resize<Unit>(ref Players, index + 1);
			Players[index] = su;
			AppearUnit(su, su.Pos);
		}
	}
	void AddEnemy(GridPos pos, int index)
	{
		if (index < EnemyInfos.Count)
		{
			AddEnemy(pos, EnemyInfos[index]);
		}
	}
	void AddEnemy(GridPos pos, UnitInfo info)
	{

		if (g.CheckEmpty(pos))
		{
			//Test-------------
			info.spriteNames = new string[6];
			for(int i = 0 ;i < info.spriteNames.Length;i++)
			{
				info.spriteNames[i] = TestDataBase.TestAtlasName[Random.Range(0,TestDataBase.TestAtlasName.Length)];
			}
			//-----------------
			Unit su = Generater.GenerateUnit(info);
			su.Group = eGroup.Enemy;
			su.Pos = pos;
			su.WorldPos = Get_GridWorldPos(pos);
			
			Enemys.Add(su);
			AppearUnit(su, su.Pos);
		}
	}
	void AddEnemy(GridPos pos)
	{
		AddEnemy(pos, Random.Range(0, EnemyInfos.Count));
	}

	void RandomEnemy(int number, int mode)
	{

		switch (mode)
		{
			case 0:
				break;
			default:
				break;
		}
		if (EnemyInfos.Count == 0)
			return;
		List<GridPos> eg = Get_EmptyGrid();
		if (eg.Count > 0)
		{
			for (int i = 0; i < number; i++)
			{
				int rindex = Random.Range(0, eg.Count);
				AddEnemy(eg[rindex]);
				eg.RemoveAt(rindex);
			}
		}
	}
	void IniPlayers()
	{
		if (Players != null)
			System.Array.Resize<Unit>(ref Players, PlayerInfos.Count);
		else
			Players = new Unit[PlayerInfos.Count];
		List<GridPos> emptyGrid = Get_EmptyGrid();
		for (int i = 0; i < PlayerInfos.Count; i++)
		{
			if (Players[i] != null)
				continue;
			if (emptyGrid.Count == 0)
				break;
			int r = Random.Range(0, emptyGrid.Count);
			
			AddPlayer(emptyGrid[r], i);
			emptyGrid.RemoveAt(r);
		}
	}
	void AppearUnit(Unit unit, GridPos pos)
	{
		if (g.CheckEmpty(pos))
			g.Appear(pos, unit);
			
	}

	public static bool MoveUnit(Unit unit, GridPos targetPos)
	{
		if (Instance == null)
			return false;
		return Instance.g.Move(unit.Pos, targetPos);
	}

	public static void Unit_Dead(Unit unit)
	{
		if (Instance != null)
		{
			switch (unit.Group)
			{
				case eGroup.Player:
					bool allNull = true;
					for (int i = 0; i < Instance.Players.Length; i++)
					{
						if (Instance.Players[i] == unit)
							Instance.Players[i] = null;
						if (Instance.Players[i] != null)
							allNull = false;
					}
					Instance.g.Left(unit.Pos);
					if (allNull)
						Instance.AllDeadEvent(eGroup.Player);
					break;
				case eGroup.Enemy:
					if (Instance.Enemys.Remove(unit))
					{
						Instance.g.Left(unit.Pos);
						if (Instance.Enemys.Count == 0)
							Instance.AllDeadEvent(eGroup.Enemy);
					}
					break;
			}
			Instance.Generater.Recycle(unit);
		}
		else
		{
			if (unit != null && unit.Entity != null)
				Destroy(unit.Entity);
		}
	}
	void Unit_Clear()
	{
		Profiler.BeginSample("Unit_Clear");
		if(Generater != null)
		{
			foreach (Unit u in Enemys)
			{
				Generater.Recycle(u);
				g.Left(u.Pos);
			}
			foreach (Unit u in Players)
			{
				if (u == null)
					continue;
				Generater.Recycle(u);
				g.Left(u.Pos);
			}
			Enemys.Clear();
			Players = new Unit[Players.Length];
		}
		Profiler.EndSample();
	}
	
	void AllDeadEvent(eGroup group)
	{
		switch (group)
		{
			case eGroup.Enemy:
				//RandomEnemy(5, 0);
				BattleIsStart = false;
				break;
			case eGroup.Player:
				BattleIsStart = false;
				break;
		}
		
	}

	public static Vector3 GetRealWorldPos(Vector3 worldPos)
	{
		return TranslateCamera(UnitCamera, worldPos, Camera.main);
	}
	public static Vector3 TranslateCamera(Camera current, Vector3 currentPos, Camera targetCam)
	{
		if (current == null || targetCam == null)
			return currentPos;
		
		Vector3 result = current.ScreenToWorldPoint(targetCam.WorldToScreenPoint(currentPos));
		result += targetCam.transform.forward * (currentPos.z - current.transform.position.z) * 30;
		result.z += currentPos.x *0.1f;
		return result;
	}


	#region Grid Control
	public static bool Get_IsGridEmpty(GridPos pos)
	{
		if (Instance == null)
			return false;
		return Instance.g.CheckEmpty(pos);
	}
	public static Vector3 Get_GridWorldPos(GridPos pos)
	{
		if (Instance == null)
			return Vector3.zero;
		return (Instance.g.Get_GridWorldPos(pos));
	}
	public static List<GridPos> Get_EmptyGrid()
	{
		if (Instance == null)
			return new List<GridPos>();
		return Instance.g.GetEmptyGrid();
	}
	public static List<GridPos> Get_EmptyGrid(int mode, GridPos pos, eDirection dir, int range)
	{
		if (Instance == null)
			return new List<GridPos>();
		List<GridPos> result = Instance.g.Get_AreaEmptyGrid(mode, pos, dir, range);
		return result;
	}
	public static List<GridPos> Get_SurroundEmptyGrid(GridPos pos)
	{
		if (Instance == null)
			return new List<GridPos>();
		return Instance.g.Get_AreaEmptyGrid(1, pos, eDirection.Left, 1);
	}
	public static List<GridPos> Get_Grids(int mode, GridPos pos, eDirection dir, int range)
	{
		if (Instance == null)
			return new List<GridPos>();
		return Instance.g.Get_AreaGridPos(mode, pos, dir, range);
	}

	public static List<Unit> Get_UnitsInRange(int mode, GridPos pos, eDirection dir, int range)
	{
		if (Instance == null)
			return new List<Unit>();
		return Instance.g.Get_AreaUnit(mode, pos, dir, range);
	}
	public static List<Unit> Get_EnemyUnitsInRange(eGroup selfGroup, int mode, GridPos pos, eDirection dir, int range)
	{
		if (Instance == null)
			return new List<Unit>();

		List<Unit> result = new List<Unit>();
		foreach (Unit u in Instance.g.Get_AreaUnit(mode, pos, dir, range))
		{
			if (CheckIsEnemy(selfGroup, u.Group))
				result.Add(u);
		}
		return result;
	}
	public static List<Unit> Get_FriendUnitsInRange(eGroup selfGroup, int mode, GridPos pos, eDirection dir, int range)
	{
		if (Instance == null)
			return new List<Unit>();
		
		List<Unit> result = new List<Unit>();
		foreach (Unit u in Instance.g.Get_AreaUnit(mode, pos, dir, range))
		{
			if (CheckIsFriend(selfGroup, u.Group))
				result.Add(u);
		}
		return result;
	}
	static bool CheckIsEnemy(eGroup selfGroup, eGroup targetGroup)
	{
		return selfGroup != targetGroup;
	}
	static bool CheckIsFriend(eGroup selfGroup, eGroup targetGroup)
	{
		return selfGroup == targetGroup;
	}
	public static List<Unit> Get_AllEnemyUnits(eGroup selfGroup)
	{
		if (Instance == null)
			return new List<Unit>();
		switch (selfGroup)
		{
			case eGroup.Player:
				return Instance.Enemys;
			case eGroup.Enemy:
				return new List<Unit>(Instance.Players);
			default:
				return new List<Unit>();
		}
	}
	public static List<Unit> Get_AllFriendUnits(eGroup selfGroup)
	{
		if (Instance == null)
			return new List<Unit>();
		switch (selfGroup)
		{
			case eGroup.Player:
				return new List<Unit>(Instance.Players);
			case eGroup.Enemy:
				return Instance.Enemys;
			default:
				return new List<Unit>();
		}
	}
	public static bool CheckInRange(int mode, GridPos pos, eDirection dir, int range, GridPos targetPos)
	{
		if (Instance == null)
			return false;
		bool r = Instance.g.CheckInRange(mode,pos,dir,range,targetPos);
		return r;
	}
	#endregion

	#region TestCode

	Vector3 lastTouchPos;
	public int AreaMode = 0;
	public int AreaRange = 3;
	public bool AreaPreview = false;
	public bool NewPreview = false;
	public eDirection Dir = eDirection.Left;

	public Vector2 gridSize = new Vector2(1, 1);
	public int WCounts = 1;
	public int LCounts = 1;

	public bool BattleIsStart = false;

	void OnDrawGizmosSelected()
	{
		if(!Application.isPlaying)
			g = new GridInfo(groundObject.transform.position, gridSize, WCounts, LCounts);
	}
	void OnDrawGizmos()
	{
		if (g != null)
		{
			g.DrawGrid();
			if (AreaPreview)
			{
				if (NewPreview)
				{
					GridPos tempGP = g.Get_GridPos(lastTouchPos);
					foreach (GridPos gpi in g.GetEmptyGrid())
					{
						if (g.CheckInRange(AreaMode, tempGP, Dir, AreaRange, gpi))
							g.DrawGrid(gpi, new Color(0, 0, 1, 0.2f));
					}
				}
				else
				{
					foreach (GridPos gpi in g.Get_AreaGridPos(AreaMode, g.Get_GridPos(lastTouchPos), Dir, AreaRange))
						g.DrawGrid(gpi, new Color(0, 0, 1, 0.2f));
				}
				g.DrawGrid(g.Get_GridPos(lastTouchPos), new Color(1, 0, 0, 0.2f));
			}
			foreach (Unit eu in Enemys)
			{
				//eu.Draw(Color.red);
				g.DrawGrid(eu.Pos, Color.red);
			}
			foreach (Unit eu in Players)
			{
				if (eu == null)
					continue;
				//eu.Draw(Color.blue);
				g.DrawGrid(eu.Pos, Color.blue);
			}
			//switch (CurrentInfoGroup)
			//{
			//    case eGroup.Enemy:
			//        if (CurrentInfoIndex >= 0 && CurrentInfoIndex < Enemys.Count)
			//            Enemys[CurrentInfoIndex].Draw(Color.white);
			//        break;
			//    case eGroup.Player:
			//        if (CurrentInfoIndex >= 0 && CurrentInfoIndex < Players.Length && Players[CurrentInfoIndex] != null)
			//            Players[CurrentInfoIndex].Draw(Color.white);
			//        break;
			//}
		}
		else
			Start();
	}
	public static void DrawGrid(GridPos pos, Color color)
	{
		if (Instance == null || Instance.g == null)
			return;
		Instance.g.DrawGrid(pos, color);
	}
	void ColliderReciver(Vector3 pos)
	{

		lastTouchPos = pos;

		GridPos gp = g.Get_GridPos(lastTouchPos);
		if (!g.CheckEmpty(gp))
		{
			Unit ud = g.Get_GridUnit(gp);
			if (ud != null)
			{
				CurrentInfoGroup = ud.Group;
				switch (CurrentInfoGroup)
				{
					case eGroup.Enemy:
						CurrentInfoIndex = Enemys.IndexOf(ud);
						break;
					case eGroup.Player:
						CurrentInfoIndex = System.Array.IndexOf<Unit>(Players, ud);
						break;
				}
			}
		}
	}

	int CurrentInfoIndex = 0;
	eGroup CurrentInfoGroup = eGroup.Player;
	public bool DisplayGUI = false;
	void OnGUI()
	{
		if (Players != null)
		{
			Rect r = new Rect(10, Screen.height * 0.85f, Screen.width * 0.25f, Screen.height * 0.15f);
			for (int i = 0; i < Players.Length; i++)
			{
				if (Players[i] == null)
					continue;
				ActionUnit au = Players[i] as ActionUnit;
				r.x = i * r.width;
				
				if (au.ExtrimSkill != null)
				{
					string content = au.ExtrimSkill.Name;
					if (au.ExtrimSkill.Castable)
					{
						if (GUI.Button(r, content))
						{
							au.CastExtrimSkill();
							//if (au.MoveState == eMoveState.Closer)
							//    au.MoveState = eMoveState.KeepRange;
							//else
							//    au.MoveState = eMoveState.Closer;
						}
					}
					else
					{
						GUIStyle gs = GUI.skin.label;
						gs.alignment = TextAnchor.MiddleCenter;
						GUI.Label(r, content,gs);
					}
				}
			}
		}
		if (!BattleIsStart)
		{
			if (GUI.Button(new Rect(Screen.width / 2 - 30, Screen.height / 2 - 20, 60, 40), "Start"))
			{
				BattleStart();
				
			}
		}
		if (!DisplayGUI)
			return;
		//GUI.skin.box.alignment = TextAnchor.UpperRight;
		if (GUI.Button(new Rect(10, 10, 100, 40), "Clear All"))
		{

			foreach (Unit u in new List<Unit>(Players))
				Unit_Dead(u);
			foreach (Unit u in new List<Unit>(Enemys))
				Unit_Dead(u);
		}
		GUI.Box(new Rect(110, 10, 100, 40), string.Format("Enemys Number {0}", Enemys.Count));
		if (GUI.Button(new Rect(210, 10, 100, 40), Time.timeScale.ToString()))
		{
			//List<GridPos> eg = new List<GridPos>(Get_EmptyGrid());
			//for (int i = 0; i < 50; i++)
			//{
			//    int r = Random.Range(0, eg.Count);
			//    AddEnemy(eg[r]);
			//    eg.RemoveAt(r);
			//    r = Random.Range(0, eg.Count);
			//    AddPlayer(eg[r]);
			//    eg.RemoveAt(r);
			//}
			TimeMachine.SetTimeScale(Time.timeScale == 1 ? 10 : (Time.timeScale == 10 ? 0 : 1));

		}

		if (GUI.Button(new Rect(10, 50, 100, 40), "AddPlayer"))
		{
			IniPlayers();
		}

		if (GUI.Button(new Rect(110, 50, 100, 40), "AddEnemy"))
		{
			RandomEnemy(5, 0);
		}


		switch (CurrentInfoGroup)
		{
			case eGroup.Enemy:
				if (CurrentInfoIndex >= 0 && CurrentInfoIndex < Enemys.Count)
					GUI.Box(new Rect(210, 50, 200, GUI.skin.box.CalcHeight(new GUIContent(Enemys[CurrentInfoIndex].ToString()), 200)), Enemys[CurrentInfoIndex].ToString());
				break;
			case eGroup.Player:
				if (CurrentInfoIndex >= 0 && CurrentInfoIndex < Players.Length && Players[CurrentInfoIndex] != null)
					GUI.Box(new Rect(210, 50, 200, GUI.skin.box.CalcHeight(new GUIContent(Players[CurrentInfoIndex].ToString()), 200)), Players[CurrentInfoIndex].ToString());
				break;
		}
#if UNITY_EDITOR
		if (UnityEditor.Selection.activeGameObject)
		{
			GUI.Label(new Rect(610, 10, 100, 40), UnityEditor.Selection.activeGameObject.GetInstanceID().ToString());
		}
		if (UnityEditor.Selection.activeGameObject && UnityEditor.Selection.activeGameObject.GetComponent<SmoothMoves.BoneAnimation>() != null)
		{
			SmoothMoves.BoneAnimation anim = UnityEditor.Selection.activeGameObject.GetComponent<SmoothMoves.BoneAnimation>();
			anim.RegisterUserTriggerDelegate(UserTriggerDelegate);
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

				//UnityEditor.Selection.activeGameObject.animation.CrossFadeQueued("Attack");
				//UnityEditor.Selection.activeGameObject.animation.CrossFadeQueued("Attack");
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
	void UserTriggerDelegate(SmoothMoves.UserTriggerEvent triggerEvent)
	{
	}

	#endregion
}
public struct GridPos
{
	public static readonly GridPos Null = new GridPos(-100000, -100000); //本來是用int.MaxValue不過再拿去加減在abs會有意想不到的結果，先-100000就好
	public static bool operator ==(GridPos gp1, GridPos gp2)
	{
		return (gp1.x == gp2.x && gp1.y == gp2.y);
	}
	public static bool operator !=(GridPos gp1, GridPos gp2)
	{
		return (gp1.x != gp2.x || gp1.y != gp2.y);
	}
	public static float SimpleDistance(GridPos gp1, GridPos gp2)
	{
		int xd = Mathf.Abs(gp1.x - gp2.x);
		int yd = Mathf.Abs(gp1.y - gp2.y);
		int xyd = Mathf.Abs(xd-yd);
		return  xyd + Mathf.Min(xd,yd) * 1.4f;
	}
	public override bool Equals(object obj)
	{
		return base.Equals(obj);
	}
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}
	public override string ToString()
	{
		return string.Format("({0},{1})", x, y);
	}
	public GridPos(int posx, int posy)
	{
		x = posx;
		y = posy;
	}
	public int x;
	public int y;
}
public class GridInfo
{
	enum eGridState
	{
		Empty,
		Occupied,
	}
	Unit[,] Grids;
	int GridBoundX
	{
		get{ return Grids.GetUpperBound(0); }
	}
	int GridBoundY
	{
		get { return Grids.GetUpperBound(1); }
	}
	public Vector3 StartPoint;
	Vector2 GridSize;
	public GridInfo()
	{
		InitGrid(1, 1, 1, 1);
	}
	public GridInfo(float width, float length, int widthGrids, int lengthGrids)
	{
		InitGrid(width, length, widthGrids, lengthGrids);
	}
	public GridInfo(Vector3 startPoint, float width, float length, int widthGridCounts, int lengthGridCounts)
	{
		StartPoint = startPoint;
		InitGrid(width, length, widthGridCounts, lengthGridCounts);
	}
	public GridInfo(Vector3 centerPoint, Vector2 gridSize, int widthGridCounts, int lengthGridCounts)
	{

		GridSize = gridSize;
		Grids = new Unit[Mathf.Clamp(widthGridCounts, 0, int.MaxValue), Mathf.Clamp(lengthGridCounts, 0, int.MaxValue)];

		StartPoint = centerPoint;
		StartPoint.x -= (widthGridCounts / 2 + widthGridCounts % 2 / 2f) * gridSize.x;
		StartPoint.z -= (lengthGridCounts / 2 + lengthGridCounts % 2 / 2f) * gridSize.y;
	}
	public void InitGrid(float width, float length, int widthGrids, int lengthGrids)
	{
		GridSize = new Vector2(width / widthGrids, length / lengthGrids);
		Grids = new Unit[widthGrids, lengthGrids];
	}

	public bool Appear(GridPos pos, Unit unit)
	{
		if (CheckEmpty(pos))
		{
			Occupy(pos, unit);
			return true;
		}
		else
			return false;
	}
	public bool Move(GridPos curPos, GridPos targetPos)
	{
		if (CheckEmpty(targetPos))
		{
			Unit unit = Grids[curPos.x,curPos.y];
			Left(curPos);
			Occupy(targetPos, unit);
			return true;
		}
		else
			return false;
	}
	public bool CheckEmpty(GridPos pos)
	{
		if (PosCheck(pos))
		{
			return Grids[pos.x, pos.y] == null;
		}
		else
			return false;
	}
	public void Left(GridPos pos)
	{
		if (PosCheck(pos))
		{
			if (Grids[pos.x, pos.y] != null)
				Grids[pos.x, pos.y].Pos = GridPos.Null;
			Grids[pos.x, pos.y] = null;
		}
	}
	void Occupy(GridPos pos,Unit unit)
	{
		if (unit == null)
			return;
		if (PosCheck(pos))
		{
			Grids[pos.x, pos.y] = unit;
			unit.Pos = pos;
		}
	}

	public List<GridPos> GetEmptyGrid()
	{
		List<GridPos> result = new List<GridPos>();
		for (int x = 0; x <= GridBoundX; x++)
			for (int y = 0; y <= GridBoundY; y++)
				if (Grids[x, y] == null)
					result.Add(new GridPos(x, y));
		return result;
	}

	public Vector3 Get_GridWorldPos(GridPos pos)
	{
		return Get_GridWorldPos(pos.x, pos.y);
	}
	public Vector3 Get_GridWorldPos(int x, int y)
	{
		return StartPoint + new Vector3(x * GridSize.x + GridSize.x / 2, 0, y * GridSize.y + GridSize.y / 5);
	}
	public Vector3 Get_GridCenterWorldPos(GridPos pos)
	{
		return Get_GridCenterWorldPos(pos.x, pos.y);
	}
	public Vector3 Get_GridCenterWorldPos(int x, int y)
	{
		return StartPoint + new Vector3(x * GridSize.x + GridSize.x / 2, 0, y * GridSize.y + GridSize.y / 2);
	}
	public GridPos Get_GridPos(Vector3 worldPos)
	{
		if (GridSize.x == 0 || GridSize.y == 0)
			return new GridPos();
		Vector3 localPos = worldPos - StartPoint;
		if (localPos.x < 0 || localPos.y < 0)
			return new GridPos();
		return new GridPos(Mathf.FloorToInt(localPos.x/GridSize.x),Mathf.FloorToInt(localPos.z/GridSize.y));
	}

	public List<GridPos> Get_AreaGridPos(int mode,GridPos pos,eDirection dir, int range)
	{
		switch(mode % GLOBALCONST.BattleSettingValue.AllInRangeModeGroup)
		{
			case 0:
				return Get_AreaGrid_Mode0(pos, range);
			case 1:
				return Get_AreaGrid_Mode1(pos, range);
			case 2:
				return Get_AreaGrid_Mode2(pos, range);
			case 3:
				return Get_AreaGrid_Mode3(pos,dir, range);
			case 4:
				return Get_AreaGrid_Mode4(pos, dir, range);
			default:
				return new List<GridPos>();
		}
		
	}

	public List<Unit> Get_AreaUnit(int mode, GridPos pos, eDirection dir, int range)
	{
		List<Unit> result =new List<Unit>();
		foreach (GridPos aGridPos in Get_AreaGridPos(mode, pos, dir, range)) 
		{
			Unit tempUnit = Get_GridUnit(aGridPos);
			if (tempUnit != null)
				if (!result.Contains(tempUnit))
					result.Add(tempUnit);
		}
		return result;
	}
	public List<GridPos> Get_AreaEmptyGrid(int mode, GridPos pos, eDirection dir, int range)
	{
		List<GridPos> result = new List<GridPos>();
		foreach (GridPos aGridPos in Get_AreaGridPos(mode, pos, dir, range))
		{
			if (Get_GridUnit(aGridPos) == null)	
					result.Add(aGridPos);
		}
		return result;
	}

	public bool CheckInRange(int mode, GridPos pos, eDirection dir, int range, GridPos targetPos)
	{

		switch (mode % GLOBALCONST.BattleSettingValue.AllInRangeModeGroup)
		{
			case 0:
				return CheckInRange_AreaGrid_Mode0(pos, range,targetPos);
			case 1:
				return CheckInRange_AreaGrid_Mode1(pos, range, targetPos);
			case 2:
				return CheckInRange_AreaGrid_Mode2(pos, range, targetPos);
			case 3:
				return CheckInRange_AreaGrid_Mode3(pos, dir, range, targetPos);
			case 4:
				return CheckInRange_AreaGrid_Mode4(pos, dir, range, targetPos);
			default:
				return false;
		}
	}
	public Unit Get_GridUnit(GridPos pos)
	{
		if(!PosCheck(pos))
			return null;
		return Grids[pos.x, pos.y];
	}

	bool PosCheck(GridPos pos)
	{
		return pos.x >= 0 && pos.y >= 0 && pos.x <= GridBoundX && pos.y <= GridBoundY;
	}
	List<GridPos> Get_AreaGrid_Mode0(GridPos pos, int range)
	{
		/*	range = 1
		 *  -o-
		 *  oco
		 *  -o-
		 *  range = 2
		 *  --o--
		 *  -ooo-
		 *  oocoo
		 *  -ooo-
		 *  --o--
		 */
		List<GridPos> result = new List<GridPos>();
		for (int i = -range; i <= range;i++ )
		{
			for (int j = -(range - Mathf.Abs(i)); j <= (range - Mathf.Abs(i)); j++)
			{
				int x = pos.x +i;
				int y = pos.y + j;
				if(x >= 0 && x <=  GridBoundX && y >= 0 && y <=  GridBoundY)
					result.Add(new GridPos(x,y));
			}
		}
		return result;
	}
	bool CheckInRange_AreaGrid_Mode0(GridPos pos,int range,GridPos targetPos )
	{
		return Mathf.Abs(pos.x - targetPos.x) + Mathf.Abs(pos.y - targetPos.y) <= range;
	}
	List<GridPos> Get_AreaGrid_Mode1(GridPos pos, int range)
	{
		/*	range = 1
		 *  ooo
		 *  oco
		 *  ooo
		 *  range = 2
		 *  ooooo
		 *  ooooo
		 *  oocoo
		 *  ooooo
		 *  ooooo
		 */
		List<GridPos> result = new List<GridPos>();
		for (int i = -range; i <= range; i++)
		{
			for (int j = -range; j <= range; j++)
			{
				int x = pos.x + i;
				int y = pos.y + j;
				if (x >= 0 && x <= GridBoundX && y >= 0 && y <= GridBoundY)
					result.Add(new GridPos(x, y));
			}
		}
		return result;
	}
	bool CheckInRange_AreaGrid_Mode1(GridPos pos, int range, GridPos targetPos)
	{
		return Mathf.Max(Mathf.Abs(pos.x - targetPos.x), Mathf.Abs(pos.y - targetPos.y))<= range;
	}
	List<GridPos> Get_AreaGrid_Mode2(GridPos pos, int range)
	{
		/*	range = 1
		 *  -o-
		 *  oco
		 *  -o-
		 *  range = 2
		 *  --o--
		 *  --o--
		 *  oocoo
		 *  --o--
		 *  --o--
		 */
		List<GridPos> result = new List<GridPos>();
		for (int i = -range; i <= range; i++)
		{
			if (i == 0)
				continue;
			int x = pos.x + i;
			int y = pos.y;
			if (x >= 0 && x <= GridBoundX && y >= 0 && y <= GridBoundY)
				result.Add(new GridPos(x, y));
			x = pos.x;
			y = pos.y + i;
			if (x >= 0 && x <= GridBoundX && y >= 0 && y <= GridBoundY)
				result.Add(new GridPos(x, y));
		}
		result.Add(pos);
		return result;
	}
	bool CheckInRange_AreaGrid_Mode2(GridPos pos, int range, GridPos targetPos)
	{
		int xd = Mathf.Abs(pos.x - targetPos.x);
		int yd = Mathf.Abs(pos.y - targetPos.y);
		if(xd != 0 && yd != 0)
			return false;
		return Mathf.Max(xd,yd) <= range;
	}
	List<GridPos> Get_AreaGrid_Mode3(GridPos pos, eDirection dir, int range)
	{
		/*	range = 1 
		 *  co
		 *  range = 2
		 *  coo
		 */
		List<GridPos> result = new List<GridPos>();
		if (range == 0)
			return result;
		int d = 0;
		switch(dir)
		{
			case eDirection.Left:
				d = -1;
				break;
			case eDirection.Right:
				d = 1;
				break;
			case eDirection.Both:
				List<GridPos> tempSet = Get_AreaGrid_Mode3(pos, eDirection.Left, range);
				tempSet.AddRange(Get_AreaGrid_Mode3(pos,eDirection.Right,range));
				return tempSet;
		}
		for (int i = 1; i <= range; i++)
		{
			if (i == 0)
				continue;
			int x = pos.x + i * d;
			int y = pos.y;
			if (x >= 0 && x <= GridBoundX && y >= 0 && y <= GridBoundY)
				result.Add(new GridPos(x, y));
		}
		return result;
	}
	bool CheckInRange_AreaGrid_Mode3(GridPos pos, eDirection dir, int range, GridPos targetPos)
	{
		int xd = targetPos.x - pos.x;
		int yd = Mathf.Abs(pos.y - targetPos.y);
		switch (dir)
		{
			case eDirection.Left:
				xd *= -1;
				break;
			case eDirection.Right:
				break;
			case eDirection.Both:
				xd = Mathf.Abs(xd);
				break;
		}

		return yd == 0 && xd > 0 && xd <= range;
	}
	List<GridPos> Get_AreaGrid_Mode4(GridPos pos, eDirection dir, int range)
	{
		/*	range = 1 
		 *  -o
		 *  co
		 *  -o
		 *  range = 2
		 *  -oo
		 *  coo
		 *  -oo
		 */
		List<GridPos> result = new List<GridPos>();
		if (range == 0)
			return result;
		int d = 0;
		switch (dir)
		{
			case eDirection.Left:
				d = -1;
				break;
			case eDirection.Right:
				d = 1;
				break;
			case eDirection.Both:
				List<GridPos> tempSet = Get_AreaGrid_Mode4(pos, eDirection.Left, range);
				tempSet.AddRange(Get_AreaGrid_Mode4(pos, eDirection.Right, range));
				return tempSet;
		}
		for (int i = 1; i <= range; i++)
		{
			if (i == 0)
				continue;
			int x = pos.x + i * d;
			for (int j = -1; j <= 1; j++)
			{			
				int y = pos.y +j;
				if (x >= 0 && x <= GridBoundX && y >= 0 && y <= GridBoundY)
					result.Add(new GridPos(x, y));
			}
		}
		return result;
	}
	bool CheckInRange_AreaGrid_Mode4(GridPos pos, eDirection dir, int range, GridPos targetPos)
	{
		int xd = targetPos.x - pos.x;
		int yd = Mathf.Abs(pos.y - targetPos.y);
		switch (dir)
		{
			case eDirection.Left:
				xd *=-1;
				break;
			case eDirection.Right:
				break;
			case eDirection.Both:
				xd = Mathf.Abs(xd);
				break;
		}
		return yd <= 1 && xd > 0 && xd <= range;
	}

//#if UNITY_EDITOR
	public void DrawGrid()
	{
		DrawGrid(new Color(0, 0, 0, 0.2f));
	}
	public void DrawGrid(Color color)
	{
		int gx =  Grids.GetUpperBound(0) +1;
		int gy = Grids.GetUpperBound(1)+1;

		Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.2f);
		//Vector3 SizeV3 = new Vector3(GridSize.x * gx, 0, GridSize.y * gy);
		//Gizmos.DrawCube(StartPoint + new Vector3(gx / 2.0f * GridSize.x, 0, gy / 2.0f * GridSize.y), SizeV3);

		Gizmos.color = Color.black;
		for (int i = 0; i <= gx; i++)
			Gizmos.DrawLine(StartPoint + new Vector3(i * GridSize.x, 0, 0), StartPoint + new Vector3(i * GridSize.x, 0, gy * GridSize.y));
		for (int i = 0; i <= gy; i++)
			Gizmos.DrawLine(StartPoint + new Vector3(0, 0, i * GridSize.y), StartPoint + new Vector3(gx * GridSize.x, 0, i * GridSize.y));

	}
	public void DrawGrid(Vector3 pos, Color color)
	{
		Vector3 GridSizeV3 = new Vector3(GridSize.x, 0, GridSize.y);
		Gizmos.color = color;
		Gizmos.DrawCube(Get_GridCenterWorldPos(Get_GridPos(pos)), GridSizeV3);
	}
	public void DrawGrid(GridPos pos, Color color)
	{
		Vector3 GridSizeV3 = new Vector3(GridSize.x, 0, GridSize.y);
		Gizmos.color = color;
		Gizmos.DrawCube(Get_GridCenterWorldPos(pos), GridSizeV3);
		Gizmos.color = Color.black;
		Gizmos.DrawWireCube(Get_GridCenterWorldPos(pos), GridSizeV3);
	}
	public void DrawGrid(int x,int y, Color color)
	{
		Vector3 GridSizeV3 = new Vector3(GridSize.x, 0, GridSize.y);
		Gizmos.color = color;
		Gizmos.DrawCube(Get_GridWorldPos(x,y), GridSizeV3);
		Gizmos.color = Color.black;
		Gizmos.DrawWireCube(Get_GridWorldPos(x, y), GridSizeV3);
	}
//#endif
}