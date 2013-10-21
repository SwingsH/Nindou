using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * 2013/8/16
 * 戰鬥單位出現跟消失和戰鬥結果管理 
 * 單位移動是個自移動，不過要透
 */
public class BattleManager : BattleState
{
	static BattleManager _instance;
	public new static BattleState instance
	{
		get
		{
			if (_instance == null)
				_instance = new BattleManager();
			return _instance;
		}
	}
	public static BattleManager Instance
	{
		get
		{
			if (_instance == null)
				_instance = new BattleManager();
			return _instance;
		}
		protected set { _instance = value; }
	}

	public static Camera UnitCamera;
	GridInfo g;
	
	List<UnitInfo> PlayerInfos = new List<UnitInfo>();
	List<EnemyNumbers> EnemyInfos = new List<EnemyNumbers>();
	public Unit[] Players = new Unit[0];
	List<Unit> Enemys = new List<Unit>();

	static bool _isBattleStart;
	public static bool IsBattleStart
	{
		get
		{
			if (Instance == null)
			{
				return false;
			}
			return _isBattleStart;
		}
		protected set
		{
			_isBattleStart = value;
		}
	}
	UnitGenerater Generater;

	//跳血元件
	HUDManager hudManager;

	float EffectCountDown;

	void UnitRun()
	{
		foreach (Unit eu in Enemys)
			if(eu!=null)
				eu.Run();
		foreach (Unit eu in Players)
			if (eu != null)
				eu.Run();

        // 將玩家角色HP變動更新到UI上
        for (int i = 0; i < Players.Length; ++i)
        {
            if (Players[i] == null || Players[i].Life <= 0) { GameControl.Instance.GUIStation.Form<UI_Battle>().SetPlayerIcon(i, true, 0, 1); }
            else { GameControl.Instance.GUIStation.Form<UI_Battle>().SetPlayerIcon(i, true, Players[i].Life, Players[i].MaxLife); }
        }
	}

	public void BattleStart()
	{
		if (BattleID == 0)
			return;
		if (IsBattleStart)
			return;
		IsBattleStart = true;
		Unit_Clear();
		EnemyInfos.Clear();
		PlayerInfos.Clear();

		EffectCountDown = 3f;

		PlayerInfos.AddRange(TestDataBase.Instance.playerInfo);
		
		if (Application.isPlaying)
		{
			IniPlayers();
			IniEnemyData(BattleID);
			IniEnemy();
		}
	}
	void IniEnemyData(uint battleID)
	{
		EnemyInfos.Clear();
		List<Battle> battleList = TestDataBase.Instance.GetBattleData(battleID);

		foreach (Battle bt in battleList)
		{
			NPCData npcData = TestDataBase.Instance.GetNPCData(bt.NPCID);
			if (npcData == null)
				continue;
			EnemyNumbers en = new EnemyNumbers();
			en.npcData = npcData;
			en.Numbers = bt.Numbers;
			EnemyInfos.Add(en);
		}
	}
	void AddPlayer(GridPos pos, int index)
	{
		if (g.CheckEmpty(pos))
		{
			Unit su = Generater.GenerateUnit(PlayerInfos[index]);
			if (su == null)
				return;
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
			AddEnemy(pos, EnemyInfos[index].npcData.Info);
			EnemyInfos[index].Numbers--;
			if (EnemyInfos[index].Numbers == 0)
				EnemyInfos.RemoveAt(index);
		}
	}
	void AddEnemy(GridPos pos, UnitInfo info)
	{

		if (g.CheckEmpty(pos))
		{
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
		List<GridPos> eg= new List<GridPos>();
		switch (mode)
		{
			case 0:
				eg = g.GetEmptyGrid_LeftColumn(2);
				break;
			case 1:
				eg = g.GetEmptyGrid();
				break;
			default:
				break;
		}
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
	void IniEnemy()
	{
		RandomEnemy(3, 0);
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
				GameObject.Destroy(unit.Entity);
		}
	}
	void Unit_Clear()
	{
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
	}
	
	void AllDeadEvent(eGroup group)
	{
		switch (group)
		{
			case eGroup.Enemy:
				if(EnemyInfos.Count != 0)
					RandomEnemy(3, 0);
				else
					IsBattleStart = false;
				break;
			case eGroup.Player:
				IsBattleStart = false;
				break;
		}
		if(!IsBattleStart)
			GameControl.Instance.ChangeGameState(BattleLeaving.instance);

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

	public void BattleResult(bool Win)
	{
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

	#region HUD
	/// <summary>
	/// 顯示傷害數字
	/// </summary>
	/// <param name="damageType">傷害類型</param>
	/// <param name="value">數值</param>
	/// <param name="displayPosition">顯示的位置</param>
	public static void ShowDamageText(SkillDamageType damageType, int value, Vector3 displayPosition)
	{
		if (Instance != null && Instance.hudManager != null)
			Instance.hudManager.ShowDamageText(damageType, value, displayPosition);
	}
	/// <summary>
	/// 顯示多段攻擊技能傷害數字
	/// </summary>
	/// <param name="attacker">攻擊方</param>
	/// <param name="attackee">受擊方</param>
	/// <param name="value">數值</param>
	/// <param name="displayPosition">顯示的位置</param>
	public static void ShowDamageGroupText(DamageInfo info, Unit attackee, int value, Vector3 displayPosition)
	{
		if (Instance != null && Instance.hudManager != null)
		{
			long callerID = ((long)info.GetHashCode() >> 32) ^ (long)attackee.GetHashCode();
			Instance.hudManager.ShowDamageGroupText(callerID, value, displayPosition);
		}
	}
	#endregion

	public override void OnChangeIn(GameControl control)
	{

		if (UnitCamera == null)
		{
			UnitCamera = new GameObject("UnitCamera").AddComponent<Camera>();
		}
		if (Camera.main)
		{
			Camera.main.cullingMask = 1 << GLOBALCONST.GameSetting.LAYER_BACKGROUND;
			UnitCamera.transform.position = Camera.main.transform.position;
			UnitCamera.transform.rotation = Camera.main.transform.rotation;
		}
		UnitCamera.orthographic = true;
		UnitCamera.cullingMask = ~(1 << GLOBALCONST.GameSetting.LAYER_BACKGROUND);
		UnitCamera.clearFlags = CameraClearFlags.Depth;
		UnitCamera.orthographicSize = GLOBALCONST.GameSetting.UNIT_CAMERA_SIZE;

		hudManager = HUDManager.Create(UnitCamera);
		HUDManager.DisplayLayer = GLOBALCONST.GameSetting.LAYER_UNIT;
		IsBattleStart = false;

		if (Application.isPlaying)
			TestDataBase.IniInstance();
		g = new GridInfo(Vector3.one, GLOBALCONST.GameSetting.GRID_SIZE, GLOBALCONST.GameSetting.GRID_COUNT_W, GLOBALCONST.GameSetting.GRID_COUNT_L);
		Generater = new UnitGenerater();

		BattleStart();
        // fs: 切換完畢後關閉選擇關卡UI，開啟戰鬥UI
        control.GUIStation.ShowAndHideOther(typeof(UI_Battle));
        control.GUIStation.Form<UI_Battle>().SetBossMessageVisible(false);
        for (int i = 0; i <  GLOBALCONST.UI_BATTLE_ROLE_ICON_COUNT; ++i)
        {
            if (i < Players.Length) { control.GUIStation.Form<UI_Battle>().SetPlayerIcon(i, true, Players[i].Life, Players[i].MaxLife); }
            else { control.GUIStation.Form<UI_Battle>().SetPlayerIcon(i, false);} // , 0, 1); }
        }
    }

	public override void Update(GameControl control)
	{
		UnitRun();
	}

	public override void OnChangeOut(GameControl control)
	{
		Unit_Clear();
		Generater.ClearGrave();
		TimeMachine.SetTimeScale(1);
	}
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
	int GridColBound
	{
		get{ return Grids.GetUpperBound(0); }
	}
	int GridRowBound
	{
		get { return Grids.GetUpperBound(1); }
	}
	public Vector3 StartPoint;
	Vector2 GridSize;
	public GridInfo()
	{
		InitGrid(1, 1, 1, 1);
	}
	public GridInfo(float width, float length, int cols, int rows)
	{
		InitGrid(width, length, cols, rows);
	}
	public GridInfo(Vector3 startPoint, float width, float length, int cols, int rows)
	{
		StartPoint = startPoint;
		InitGrid(width, length, cols, rows);
	}
	public GridInfo(Vector3 centerPoint, Vector2 gridSize, int cols, int rows)
	{

		GridSize = gridSize;
		Grids = new Unit[Mathf.Clamp(cols, 0, int.MaxValue), Mathf.Clamp(rows, 0, int.MaxValue)];

		StartPoint = centerPoint;
		StartPoint.x -= (cols / 2 + cols % 2 / 2f) * gridSize.x;
		StartPoint.z -= (rows / 2 + rows % 2 / 2f) * gridSize.y;
	}
	public void InitGrid(float width, float length, int cols, int rows)
	{
		GridSize = new Vector2(width / cols, length / rows);
		Grids = new Unit[cols, rows];
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
		for (int x = 0; x <= GridColBound; x++)
			for (int y = 0; y <= GridRowBound; y++)
				if (Grids[x, y] == null)
					result.Add(new GridPos(x, y));
		return result;
	}
	public List<GridPos> GetEmptyGrid_LeftColumn(int numbers)
	{
		List<GridPos> result = new List<GridPos>();
		for (int i = 0; i < numbers && i <= GridColBound; i++)
			for (int j = 0; j <= GridRowBound; j++)
				if (Grids[i, j] == null)
					result.Add(new GridPos(i, j));
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
		return pos.x >= 0 && pos.y >= 0 && pos.x <= GridColBound && pos.y <= GridRowBound;
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
				if(x >= 0 && x <=  GridColBound && y >= 0 && y <=  GridRowBound)
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
				if (x >= 0 && x <= GridColBound && y >= 0 && y <= GridRowBound)
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
			if (x >= 0 && x <= GridColBound && y >= 0 && y <= GridRowBound)
				result.Add(new GridPos(x, y));
			x = pos.x;
			y = pos.y + i;
			if (x >= 0 && x <= GridColBound && y >= 0 && y <= GridRowBound)
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
			if (x >= 0 && x <= GridColBound && y >= 0 && y <= GridRowBound)
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
				if (x >= 0 && x <= GridColBound && y >= 0 && y <= GridRowBound)
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

public class EnemyNumbers
{
	public NPCData npcData;
	public int Numbers;
}

/*
 * 戰鬥分成進入、戰鬥中、離開戰鬥三種
 * 為了方便使用，進入戰鬥只要用BattleState的instance就好了
 */
public abstract class BattleState : IGameState
{
	public static BattleState instance
	{
		get
		{
			return BattleEntering.instance;
		}
	}
	public static uint BattleID;
	public abstract void OnChangeIn(GameControl control);

	public abstract void Update(GameControl control);

	public abstract void OnChangeOut(GameControl control);
}


public class BattleEntering : BattleState
{
	private static BattleEntering _instance;

	/*
	 * 因為static無法override所以用new
	 * 雖然new會有變數型別不同會有不同的結果的情況
	 * 可是static不用變數直接用型別來存取的，所以應該是不會有誤用的問題
	 */
	public new static BattleState instance
	{
		get
		{
			if (_instance == null)
				_instance = new BattleEntering();
			return _instance;
		}
	}
	public override void OnChangeIn(GameControl control)
	{
		Application.LoadLevel("BattleField");
		TimeMachine.SetTimeScale(1);
	}

	public override void Update(GameControl control)
	{
		if (Application.loadedLevelName == "BattleField")
		{
			GameControl.Instance.ChangeGameState(BattleManager.Instance);
		}
	}
	public override void OnChangeOut(GameControl control)
	{
	}
}

public class BattleLeaving :BattleState
{
	private static BattleLeaving _instance;
	public new static BattleState instance
	{
		get
		{
			if (_instance == null)
				_instance = new BattleLeaving();
			return _instance;
		}
	}
	public override void OnChangeIn(GameControl control)
	{
		BattleID = uint.MinValue;
		Application.LoadLevel("Empty");
	}

	public override void Update(GameControl control)
	{
        // fs : 改變GameState到GameStageSelect時，會關閉除了UI_Main_StageSelect以外的介面，
        //      此時會使用DestroyImmediate刪除UIDrawCall，如果在LoadLevel()尚未完成時呼叫，
        //      會出現「Destroying GameObjects immediately is not permitted during physics trigger/contact or animation event callbacks...」的錯誤訊息，
        //      故要在Update中持續等待LoadLevel()完成後才呼叫。
        if (Application.loadedLevelName == "Empty")
        {
            GameControl.Instance.ChangeGameState(GameStageSelect.Instance);
        }
	}

	public override void OnChangeOut(GameControl control)
	{

	}
}
