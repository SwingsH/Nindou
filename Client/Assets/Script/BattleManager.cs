using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/*
 * 2013/8/16
 * 戰鬥單位出現跟消失和戰鬥結果管理 
 * 單位移動是個自移動，不過要透過這個改格子資料的位置
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

	/// <summary>
	/// 格子資料，防止重疊跟算攻擊距離用
	/// </summary>
	GridInfo BattleGridInfo;
	
	List<UnitInfo> PlayerInfos = new List<UnitInfo>();
	List<EnemyNumbers> EnemyInfos = new List<EnemyNumbers>();
	public Unit[] Players = new Unit[0];
	public List<Unit> Enemys = new List<Unit>();

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
	public HUDManager hudManager;
	//後製元件
	PostEffectManager postEffectManager;
	
	float StartCountDown;
	float SpawnDelay;

	void UnitRun()
	{
		foreach (Unit eu in Enemys)
			if(eu!=null)
				eu.Run();
		foreach (Unit eu in Players)
			if (eu != null)
				eu.Run();

        // 更新UI_Battle的顯示資訊
        UpdateUIBattle();
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

		//EffectCountDown = 3f;

		PlayerInfos.AddRange(InformalDataBase.Instance.playerInfo);
		
		if (Application.isPlaying)
		{
			IniPlayers();
			IniEnemyData(BattleID);
			IniEnemy();
		}
	}

	void IniPlayers()
	{
		//因為是初始化，應該不太可能會不是空的，不過安全起見還是處理一下
		if (Players != null)
		{
			foreach(Unit u in Players)
				Generater.Recycle(u);
		}
		Players = new Unit[PlayerInfos.Count];
		List<GridPos> emptyGrid = GetEmptyGrid(2,2);
		emptyGrid.RandomizeListOrder();
		for (int i = 0; i < PlayerInfos.Count; i++)
		{
			if (PlayerInfos[i] == null)
				continue;
			int egIndex = 0;
			int iniGridRangeSize = 2;
			while (true)
			{
				if (IsEmpty(emptyGrid[egIndex], PlayerInfos[i].Size))
				{
					AddPlayer(emptyGrid[egIndex], i);

					emptyGrid = GetEmptyGrid(2, 2);
					emptyGrid.RandomizeListOrder();
					break;
				}
				egIndex++;
				if (egIndex >= emptyGrid.Count)
				{
					iniGridRangeSize++;
					if (iniGridRangeSize >= BattleGridInfo.Cols)
					{
						CommonFunction.DebugError("格子不夠產生Unit");
						break;
					}
					egIndex = 0;
					emptyGrid = GetEmptyGrid(2, iniGridRangeSize);
					emptyGrid.RandomizeListOrder();
				}
			}			
		}
	}
	void AddPlayer(GridPos pos, int index)
	{
		if (IsEmpty(pos,PlayerInfos[index].Size))
		{
			Unit su = Generater.GenerateUnit(PlayerInfos[index]);
			if (su == null)
				return;
			su.Group = eGroup.Player;
			UnitOccupy(su, pos);
			su.WorldPos = Get_GridWorldPos(pos,su.Size);
			if (su.Entity != null)
				su.Entity.name += index;
			if (Players.Length <= index)
				System.Array.Resize<Unit>(ref Players, index + 1);
			Players[index] = su;
			Players[index].RefreshEntity();
			Players[index].Direction = eDirection.Left;
		}
	}

	void AddEnemy(GridPos pos, UnitInfo info)
	{
		if (IsEmpty(pos, info.Size))
		{
			Unit su = Generater.GenerateUnit(info);
			su.Group = eGroup.Enemy;
			UnitOccupy(su, pos);
			su.WorldPos = Get_GridWorldPos(pos,su.Size);
			su.RefreshEntity();
			su.Direction = eDirection.Right;
			Enemys.Add(su);
			return;
		}
		else
			return;
	}

	void IniEnemyData(uint battleID)
	{
		EnemyInfos.Clear();
		List<Battle> battleList = InformalDataBase.Instance.GetBattleData(battleID);

		foreach (Battle bt in battleList)
		{
			NPCData npcData = InformalDataBase.Instance.GetNPCData(bt.NPCID);
			if (npcData == null)
				continue;
			EnemyNumbers en = new EnemyNumbers();
			en.npcData = npcData;
			en.Numbers = bt.Numbers;
			EnemyInfos.Add(en);
		}
	}
	void IniEnemy()
	{
		RandomEnemy(GLOBALCONST.GameSetting.ENEMY_MAX_NUMBER, 1);
	}

	void RandomEnemy(int number, int mode)
	{
		if (EnemyInfos.Count == 0)
			return;
		List<GridPos> eg = GetEmptyGrid(mode, 2);
		eg.RandomizeListOrder();

		while (number > 0)
		{
			if (EnemyInfos.Count == 0)
				break;

			number--;
			EnemyNumbers eInfo = EnemyInfos[Random.Range(0, EnemyInfos.Count)];
			UnitInfo uInfo = eInfo.npcData.Info;

			int egIndex = 0;
			int iniGridRangeSize = 2;
			while (true)
			{
				//判斷有沒有足夠的空間
				if (IsEmpty(eg[egIndex], uInfo.Size))
				{
					AddEnemy(eg[egIndex], uInfo);
					eInfo.Numbers--;
					if (eInfo.Numbers <= 0)
						EnemyInfos.Remove(eInfo);
					eg = GetEmptyGrid(mode, 2);
					eg.RandomizeListOrder();
					break;
				}
				egIndex++;

				//目前的空格list中找不到足夠的空間，重新取更大的空間來判斷是否有空的格子
				if (egIndex >= eg.Count)
				{
					iniGridRangeSize++;
					
					//不過 0 是取全場，等於就沒格子了，其他模式如過超過欄寬也是沒格子了
					if (mode == 0 || iniGridRangeSize >= BattleGridInfo.Cols)
					{
						CommonFunction.DebugError("格子不夠產生Unit");
						break;
					}
					egIndex = 0;
					eg = GetEmptyGrid(mode, iniGridRangeSize);
					eg.RandomizeListOrder();
				}
			}		
		}
	}

	//取得空的格子，unit出現模式
	List<GridPos> GetEmptyGrid(int iniMode,int size)
	{
		List<GridPos> eg = new List<GridPos>();
		switch (iniMode)
		{
			
			case 0:
				eg = BattleGridInfo.GetEmptyGrid();
				break;
			case 1: //最左方n行
				eg = BattleGridInfo.GetEmptyGrid_LeftColumn(size);
				break;
			case 2: //最右方n行
				eg = BattleGridInfo.GetEmptyGrid_RightColumn(size);
				break;
			default:
				break;
		}
		return eg;
	}

	/// <summary>
	/// 移動unit的pos跟變更GridInfo中的資訊
	/// 移動的座標以Unit的BasePos為基準
	/// </summary>
	public static bool MoveUnit(Unit unit, GridPos targetPos)
	{
		if (Instance == null)
			return false;
		if (Movable(unit,targetPos))
		{
			Instance.UnitLeft(unit);
			Instance.UnitOccupy(unit, targetPos);
			
			unit.Pos[0, 0] = targetPos;
			return true;
		}
		else
			return false;
	}

	/// <summary>
	/// 判斷是否可移動到該處
	/// </summary>
	public static bool Movable(Unit unit, GridPos targetpos)
	{
		if(Instance == null || Instance.BattleGridInfo == null)
			return false;
		if(unit == null)
			return false;
		GridInfo gInfo = Instance.BattleGridInfo;
		for(int i = 0 ; i < unit.Size; i++)
		{
			for(int j = 0 ; j < unit.Size;j++)
			{
				if (gInfo.PosCheck(targetpos.x + i, targetpos.y + j))
				{
					Unit temp = gInfo.Get_GridUnit(targetpos.x + i, targetpos.y + j);
					if (temp != null && temp != unit)
						return false;
				}
				else
					return false;
			}
		}
		return true;
	}

	/// <summary>
	/// 判斷是否為空
	/// </summary>
	public static bool IsEmpty(GridPos targetpos, int size)
	{
		if (Instance == null || Instance.BattleGridInfo == null)
			return false;

		size = Mathf.Max(1, size);

		GridInfo gInfo = Instance.BattleGridInfo;
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				if (gInfo.PosCheck(targetpos.x + i, targetpos.y + j))
				{
					Unit temp = gInfo.Get_GridUnit(targetpos.x + i, targetpos.y + j);
					if (temp != null)
						return false;
				}
				else
					return false;
			}
		}
		return true;
	}

	//將單位從格子資料中移除
	void UnitLeft(Unit unit)
	{
		if (BattleGridInfo != null)
		{
			foreach (GridPos pos in unit.Pos)
				BattleGridInfo.Left(pos);
		}
	}

	/// <summary>
	/// 設定unit的gridpos並存放到gridinfo中
	/// 是假設流程一切正常的情況下，故不做任何檢查跟防呆
	/// </summary>
	void UnitOccupy(Unit unit, GridPos basePos)
	{
		if (BattleGridInfo != null)
		{
			if (unit.Pos.Length < unit.Size * unit.Size)
				unit.Pos = new GridPos[unit.Size, unit.Size];

			for (int i = 0; i <= unit.Pos.GetUpperBound(0); i++)
			{
				for (int j = 0; j <= unit.Pos.GetUpperBound(1); j++)
				{
					unit.Pos[i, j] = new GridPos(basePos.x + i, basePos.y + j);
					if (!BattleGridInfo.CheckEmpty(unit.Pos[i, j]))
					{
						Debug.LogError("重複使用到同一個格子");
					}
					BattleGridInfo.Occupy(unit.Pos[i, j], unit);
				}
			}
		}
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
					foreach(GridPos pos in unit.Pos)
						Instance.BattleGridInfo.Left(pos);
					if (allNull)
						Instance.AllDeadEvent(eGroup.Player);
					break;
				case eGroup.Enemy:
					if (Instance.Enemys.Remove(unit))
					{
						foreach (GridPos pos in unit.Pos)
							Instance.BattleGridInfo.Left(pos);
                        // fs: 刪除對應UI資訊
                        DeleteEnemyInfoUI(unit.Name);
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

	public static void Play_ExtrimSkillPreEffect(Unit unit)
	{
		if (Instance != null)
		{
			Instance.postEffectManager.ExtrimSkillEffectStart(unit);
		}
	}

	void AllDeadEvent(eGroup group)
	{
		switch (group)
		{
			case eGroup.Enemy:
				if (EnemyInfos.Count != 0)
					//RandomEnemy(GLOBALCONST.GameSetting.ENEMY_MAX_NUMBER, 0);
					SpawnDelay = 1;
				else
				{
					IsBattleStart = false;
					BattleResult = eBattleResult.Win;
				}
				break;
			case eGroup.Player:
				{
					IsBattleStart = false;
					BattleResult = eBattleResult.Lose;
				}
				break;
		}
        // fs: 戰鬥結束時的UI處理
        BattleEndProcessUIBattle(group == eGroup.Enemy);
		if (!IsBattleStart)
			GameControl.Instance.ChangeGameState(BattleLeaving.instance);

	}

	void Unit_Clear()
	{
		if(Generater != null)
		{
			foreach (Unit u in Enemys)
			{
				Generater.Recycle(u);
				foreach (GridPos pos in u.Pos)
					BattleGridInfo.Left(pos);
			}
			foreach (Unit u in Players)
			{
				if (u == null)
					continue;
				Generater.Recycle(u);
				foreach (GridPos pos in u.Pos)
					BattleGridInfo.Left(pos);
			}
			Enemys.Clear();
			Players = new Unit[Players.Length];
		}
	}

	public static Vector3 Get_RealWorldPos(GridPos gridPos)
	{
		return Get_RealWorldPos(Get_GridWorldPos(gridPos));
	}
	public static Vector3 Get_RealWorldPos(Vector3 worldPos)
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
		return Instance.BattleGridInfo.CheckEmpty(pos);
	}
	public static Vector3 Get_GridWorldPos(GridPos pos)
	{
		if (Instance == null)
			return Vector3.zero;
		return (Instance.BattleGridInfo.Get_GridWorldPos(pos));
	}
	public static Vector3 Get_GridWorldPos(GridPos pos,int GridGroupSize)
	{
		if (Instance == null)
			return Vector3.zero;
		return (Instance.BattleGridInfo.Get_GridWorldPos(pos, GridGroupSize));
	}
	public static List<GridPos> Get_EmptyGrid()
	{
		if (Instance == null)
			return new List<GridPos>();
		return Instance.BattleGridInfo.GetEmptyGrid();
	}

	public static List<GridPos> Get_EmptyGrid(int mode, GridPos pos, eDirection dir, int range)
	{
		if (Instance == null)
			return new List<GridPos>();
		List<GridPos> result = Instance.BattleGridInfo.Get_AreaEmptyGrid(mode, pos, dir, range);
		return result;
	}

	public static List<GridPos> Get_SurroundEmptyGrid(GridPos pos)
	{
		if (Instance == null)
			return new List<GridPos>();
		return Instance.BattleGridInfo.Get_AreaEmptyGrid(1, pos, eDirection.Both, 1);
	}
	public static List<GridPos> Get_SurroundGrid(GridPos pos)
	{
		if (Instance == null)
			return new List<GridPos>();
		return Instance.BattleGridInfo.Get_AreaGridPos(1, pos, eDirection.Both, 1);
	}
	public static List<GridPos> Get_Grids(int mode, GridPos pos, eDirection dir, int range)
	{
		if (Instance == null)
			return new List<GridPos>();
		return Instance.BattleGridInfo.Get_AreaGridPos(mode, pos, dir, range);
	}

	public static List<Unit> Get_UnitsInRange(int mode, GridPos pos, eDirection dir, int range)
	{
		if (Instance == null)
			return new List<Unit>();
		return Instance.BattleGridInfo.Get_AreaUnit(mode, pos, dir, range);
	}

	public static List<Unit> Get_EnemyUnitsInRange(Unit selfUnit, int mode, eDirection dir, int range)
	{
		if (Instance == null)
			return new List<Unit>();

		List<Unit> sourceList = Get_AllEnemyUnits(selfUnit.Group);
		return Get_UnitsInRange(selfUnit,sourceList,mode,dir,range);
	}
	public static List<Unit> Get_FriendUnitsInRange(Unit selfUnit, int mode, eDirection dir, int range)
	{
		if (Instance == null)
			return new List<Unit>();

		List<Unit> sourceList = Get_AllFriendUnits(selfUnit.Group);
		return Get_UnitsInRange(selfUnit, sourceList, mode, dir, range);
	}

	static List<Unit> Get_UnitsInRange(Unit selfUnit ,List<Unit> targetList,int mode,eDirection dir,int range)
	{
		List<Unit> result = new List<Unit>();
		foreach (Unit u in targetList)
		{
			if (CheckInRange(mode, selfUnit, dir, range, u))
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
				return new List<Unit>(Instance.Enemys);
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
		bool r = Instance.BattleGridInfo.CheckInRange(mode,pos,dir,range,targetPos);
		return r;
	}
	public static bool CheckInRange(int mode, Unit unit, eDirection dir, int range, Unit targetUnit)
	{
		if (unit == null || targetUnit == null)
			return false;
		GridPos closestPos = GridPos.Null;
		GridPos closestTargetPos = GridPos.Null;
		if (GetClosestPos(unit, targetUnit,out closestPos, out closestTargetPos))
			return CheckInRange(mode, closestPos, dir, range, closestTargetPos);
		else
			return false;
	}

	/*
	 * 多格單位可能會有一格以上有最近距離的格子，目前覺得還不需要這多格的資訊，先取一格就好 2013.10.28
	 */
	/// <summary>
	/// 找出兩組格子中距離對方最近的一個格子
	/// </summary>
	/// <param name="basePos">基準格1(最左下)</param>
	/// <param name="size">格子組1大小</param>
	/// <param name="targetBasePos">基準格2(最左下)</param>
	/// <param name="targetSize">格子組2大小</param>
	/// <param name="pos">輸出1</param>
	/// <param name="targetPos">輸出2</param>
	public static void GetClosestPos(GridPos basePos, int size, GridPos targetBasePos, int targetSize, out GridPos pos, out GridPos targetPos)
	{
		//用位置差來判斷最近的距離是哪個
		//0會有問題，防呆
		size = Mathf.Max(1, size);
		targetSize = Mathf.Max(1, targetSize);

		GridPos posDelta = basePos - targetBasePos;
		pos = basePos;
		targetPos = targetBasePos;
		switch (System.Math.Sign(posDelta.x))
		{
			//case 0: //0正上或正下，就都用basepos就好
			case -1:
				//target在右邊
				//超過size就+size不然+posDelta
				pos.x = basePos.x + Mathf.Min(Mathf.Abs(posDelta.x),size - 1);
				break;
			case 1:
				//target在左邊
				targetPos.x = targetBasePos.x + Mathf.Min(Mathf.Abs(posDelta.x),targetSize - 1);
				break;	
		}

		switch (System.Math.Sign(posDelta.y))
		{
			//case 0: 正左或正右，就都用basepos就好
			case -1://target在上
				//超過size就+size不然+posDelta
				pos.y = basePos.y + Mathf.Min(Mathf.Abs(posDelta.y), size - 1);
				break;
			case 1://target在下
				targetPos.y = targetBasePos.y + Mathf.Min(Mathf.Abs(posDelta.y),targetSize - 1);
				break;
		}
	}
	public static bool GetClosestPos(Unit unit, Unit targetUnit, out GridPos pos, out GridPos targetPos)
	{
		if (unit == null || targetUnit == null)
		{
			pos = GridPos.Null;
			targetPos = GridPos.Null;
			return false;
		}
		GetClosestPos(unit.BasePos, unit.Size, targetUnit.BasePos, targetUnit.Size, out pos, out targetPos);
		return true;
	}

	public static List<GridPos> GetBaseposOfClosestPos(GridPos closestPos, int size, GridPos targetBasePos)
	{
		List<GridPos> result = new List<GridPos>();

		size = Mathf.Max(1, size);

		GridPos posDelta = closestPos - targetBasePos;
		if (posDelta.x == 0 && posDelta.y == 0)
			return result;
		GridPos basePos = new GridPos();
		switch (System.Math.Sign(posDelta.x))
		{
			case -1:
				//target在右邊
				basePos.x = closestPos.x - (size - 1);
				break;
			case 1:
				//target在左邊
				basePos.x = closestPos.x;
				break;
		}

		switch (System.Math.Sign(posDelta.y))
		{
			case -1://target在上
				//超過size就+size不然+posDelta
				basePos.y = closestPos.y - (size - 1);
				break;
			case 1://target在下
				basePos.y = closestPos.y;
				break;
		}

		if (posDelta.x == 0)//正上或正下方，可能會有複數basepos最近的格子都是同一個
		{
			for (int i = -(size - 1); i <= 0; i++)
			{
				basePos.x = closestPos.x + i;
				result.Add(basePos);
			}
		}
		else if (posDelta.y == 0) //正左或正右，可能會有複數basepos最近的格子都是同一個
		{
			for (int i = -(size - 1); i <= 0; i++)
			{
				basePos.y = closestPos.y + i;
				result.Add(basePos);
			}
		}
		else
			result.Add(basePos);

		return result;
	}

	/// <summary>
	/// 傳回目前可以移動且可以打到目標的所有位置（BasePos）
	/// </summary>
	/// <param name="self">攻擊方單位</param>
	/// <param name="target">目標單位</param>
	/// <param name="RangeMode">攻擊射程種類</param>
	/// <param name="RangeSize">攻擊射程</param>
	public static List<GridPos> Get_MovablePos_AroundTarget_InAttackRange(Unit self, Unit target, int RangeMode, int RangeSize)
	{
		if (self == null || target == null)
			return new List<GridPos>();

		return Get_MovablePos_AroundTarget_InAttackRange(self, target.BasePos, target.Size, RangeMode, RangeSize);
	}

	/*
	 * 測試的時候發現結果的格子跟，目標座標往左下位移攻擊方大小-1個單位，大小為攻擊方大小加目標大小-1，這個座標資料的攻擊範圍的格子是一樣的
	 * 所以以下是用這個樣子的方式去取可以移動座標
	 */
	/// <summary>
	/// 傳回目前可以移動且可以打到目標的所有位置（BasePos）
	/// </summary>
	/// <param name="selfSize">攻擊方單位所佔格子大小</param>
	/// <param name="targetPos">目標單位位置</param>
	/// <param name="targetSize">目標單位所佔格子大小</param>
	/// <param name="RangeMode">攻擊射程種類</param>
	/// <param name="RangeSize">攻擊射程</param>
	public static List<GridPos> Get_MovablePos_AroundTarget_InAttackRange(Unit self, GridPos targetPos, int targetSize, int RangeMode, int RangeSize)
	{
		if (Instance == null)
			return new List<GridPos>();
		GridPos basePos = new GridPos(targetPos.x - (self.Size - 1), targetPos.y - (self.Size - 1));
		int size = self.Size + targetSize - 1;
		GridPos[,] Pos = new GridPos[size,size];//basePos大小為size的格子區塊，這裡不會在可移動範圍中（會跟目標重疊），要在結果中先移除這些位置
		IEnumerable<GridPos> tempResult = new List<GridPos>();
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				Pos[i,j] = new GridPos(basePos.x + i, basePos.y + j);
				//只做四周的格子
				if (i == 0 || j == 0 || i == size - 1 || j == size - 1)
				{
					tempResult = tempResult.Union(Get_EmptyGrid(RangeMode, Pos[i, j], eDirection.Both, RangeSize));
				}
			}
		}
		tempResult = tempResult.Except(Pos.Cast<GridPos>());
		List<GridPos> result = new List<GridPos>();
		foreach (GridPos gpi in tempResult)
		{
			if (Movable(self, gpi))
				result.Add(gpi);
		}
		return result;

	}
	#endregion

	#region HUD
	/// <summary>
	/// 顯示傷害數字
	/// </summary>
	/// <param name="damageType">傷害類型</param>
	/// <param name="value">數值</param>
	/// <param name="displayPosition">顯示的位置</param>
	public static void ShowDamageText(eGroup unitGroup, int value, Vector3 displayPosition)
	{
		if (Instance != null && Instance.hudManager != null)
			Instance.hudManager.ShowDamageText(unitGroup, value, displayPosition);
	}
	/// <summary>
	/// 顯示回復數字
	/// </summary>
	/// <param name="damageType">傷害類型</param>
	/// <param name="value">數值</param>
	/// <param name="displayPosition">顯示的位置</param>
	public static void ShowHealText(int value, Vector3 displayPosition)
	{
		if (Instance != null && Instance.hudManager != null)
			Instance.hudManager.ShowHealText(value, displayPosition);
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
			if (info != null && attackee != null)
			{
				long callerID = ((long)info.GetHashCode() << 32) ^ (long)attackee.GetHashCode();
				Instance.hudManager.ShowDamageGroupText(callerID, value, displayPosition);
			}
		}
	}

	/// <summary>
	/// 顯示未擊中
	/// </summary>
	/// <param name="displayPosition">顯示的位置</param>
	public static void ShowMissText(Vector3 displayPosition)
	{
		if (Instance != null && Instance.hudManager != null)
		{
			Instance.hudManager.ShowText(GLOBAL_STRING.HUD_MISS, displayPosition, Vector3.left, Color.red, 0.5f,GLOBALCONST.GameSetting.LAYER_UNIT);
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
		UnitCamera.cullingMask = ~(1 << GLOBALCONST.GameSetting.LAYER_BACKGROUND | 1<<GLOBALCONST.GameSetting.LAYER_EXTRAEFFECT | 1<<GLOBALCONST.LAYER_UI_BASE);
		UnitCamera.clearFlags = CameraClearFlags.Depth;
		UnitCamera.orthographicSize = GLOBALCONST.GameSetting.UNIT_CAMERA_SIZE;

		postEffectManager = UnitCamera.gameObject.AddComponent<PostEffectManager>();
		postEffectManager.TargetCamera = UnitCamera;
		//
		hudManager = HUDManager.Create(UnitCamera);
		HUDManager.DisplayLayer = GLOBALCONST.GameSetting.LAYER_UNIT;

		IsBattleStart = false;

		//讀取測試資料
		if (Application.isPlaying)
			InformalDataBase.IniInstance();

		//戰鬥格子資料
		BattleGridInfo = new GridInfo(Vector3.one, GLOBALCONST.GameSetting.GRID_SIZE, GLOBALCONST.GameSetting.GRID_COUNT_W, GLOBALCONST.GameSetting.GRID_COUNT_L);

		//角色產生器
		Generater = new UnitGenerater();

		BattleStart();
        // fs: 切換完畢後，設定UI_Battle資訊
        SetUIBattle(control);
        
        StartCountDown = GLOBALCONST.BATTLE_START_COOL_DOWN;
    }

    /// <summary>
    /// 刷新所有 Unit 類別
    /// </summary>
    public void UpdateUnits()
    {
        foreach (Unit player in Players)
        {
            if (player == null)
                continue;

            player.Update();
        }

        foreach (Unit enemy in Enemys)
        {
            if (enemy == null)
                continue;
            enemy.Update();
        }
    }

	public override void Update(GameControl control)
	{
		if (StartCountDown > 0)
		{
			StartCountDown -= Time.deltaTime;
			return;
		}
		if (SpawnDelay > 0)
		{
			SpawnDelay -= Time.deltaTime;
			if (SpawnDelay <= 0)
			{
				if (EnemyInfos.Count != 0)
					RandomEnemy(GLOBALCONST.GameSetting.ENEMY_MAX_NUMBER, 0);
			}
		}

        UpdateUICDTime(Time.deltaTime);
		UnitRun();
        UpdateUnits();
	}

	public override void OnChangeOut(GameControl control)
	{
		//Unit_Clear();
		Generater.ClearGrave();
		TimeMachine.SetTimeScale(1);
		if(BattleResult == eBattleResult.Lose)
			postEffectManager.SetDefaultEffect_1(BattleState.instance);
	}

	#if UNITY_EDITOR
	public Unit AddUnit(GridPos pos)
	{
		if (IsEmpty(pos, 2))
		{
			Unit u = new ActionUnit();
			u.Size = 3;
			UnitOccupy(u, pos);
			return u;
		}
		else
			return null;
	}
	public void DrawRealUnitGrid()
	{

		int gx = BattleGridInfo.Cols;
		int gy = BattleGridInfo.Rows;
		Gizmos.color = Color.black;
		for (int i = 0; i <= gx; i++)
			Gizmos.DrawLine(Get_RealWorldPos(BattleGridInfo.StartPoint + new Vector3(i * BattleGridInfo.GridSize.x, 0, 0)),
							Get_RealWorldPos(BattleGridInfo.StartPoint + new Vector3(i * BattleGridInfo.GridSize.x, 0, gy * BattleGridInfo.GridSize.y)));
		for (int i = 0; i <= gy; i++)
			Gizmos.DrawLine(Get_RealWorldPos(BattleGridInfo.StartPoint + new Vector3(0, 0, i * BattleGridInfo.GridSize.y)),
							Get_RealWorldPos(BattleGridInfo.StartPoint + new Vector3(gx * BattleGridInfo.GridSize.x, 0, i * BattleGridInfo.GridSize.y)));
	}
	public void DrawAllGrid()
	{
		BattleGridInfo.DrawGrid();
	}
	public void DrawGrid(GridPos basePos, int size, Color color)
	{
		for (int i = 0; i < size; i++)
			for (int j = 0; j < size; j++)
				DrawGrid(new GridPos(basePos.x + i, basePos.y + j), color);
	}
	public void DrawGrid(GridPos pos, Color color)
	{
		BattleGridInfo.DrawGrid(pos, color);
	}
	public void DrawLine(GridPos pos1, GridPos pos2, Color color)
	{
		Gizmos.color = color;
		Gizmos.DrawLine(BattleGridInfo.Get_GridCenterWorldPos(pos1), BattleGridInfo.Get_GridCenterWorldPos(pos2));
	}
	#endif

    #region UI_Battle顯示設定相關
    /// <summary>
    /// 設定UI_Battle
    /// </summary>
    void SetUIBattle(GameControl control)
    {
        // TODO: 此處應該根據是否有Boss來決定是否顯示
        control.GUIStation.Form<UI_Battle>().SetBossMessageVisible(false);
        // fs: 設定敵方血條UI
        foreach (AnimUnit enemyUnit in Enemys)
        {
            control.GUIStation.Form<UI_Battle>().AddEnemyInfoUI(enemyUnit.Name, BattleManager.UnitCamera.WorldToScreenPoint(enemyUnit.WorldUpperCenter),
                (int)enemyUnit.Life, (int)enemyUnit.MaxLife); // TODO: 傳入屬性（陰、陽、體、無）資訊
        }
        // fs: 設定我方血條UI
        for (int i = 0; i < GLOBALCONST.MAX_BATTLE_ROLE_COUNT; ++i)
        {
            if (i < Players.Length && Players[i] != null) { control.GUIStation.Form<UI_Battle>().SetPlayerIcon(i, true, Players[i].Life, Players[i].MaxLife); }
            else { control.GUIStation.Form<UI_Battle>().SetPlayerIcon(i, false); }
        }
        // fs: 設定我方角色屬性
        control.GUIStation.Form<UI_Battle>().SetAllRoleGameAttribute();
        // fs: 設定我方角色CD時間
        control.GUIStation.Form<UI_Battle>().InitPlayerRoleCD(Players.ToList().AsReadOnly());
    }

    /// <summary>
    /// 更新UI_Battle顯示資訊
    /// </summary>
    void UpdateUIBattle()
    {
        // TODO: 更新BOSS血條
        // fs: 將敵方角色HP變動更新到UI上
        GameControl.Instance.GUIStation.Form<UI_Battle>().SetAllEnemyInfoUI(Enemys.AsReadOnly());
        // fs: 將玩家角色HP變動更新到UI上
        for (int i = 0; i < Players.Length; ++i)
        {
            if (Players[i] == null || Players[i].Life <= 0) { GameControl.Instance.GUIStation.Form<UI_Battle>().SetPlayerIcon(i, true); }
            else { GameControl.Instance.GUIStation.Form<UI_Battle>().SetPlayerIcon(i, true, Players[i].Life, Players[i].MaxLife); }
        }

    }

    /// <summary>
    /// 更新各角色的CD時間的UI顯示
    /// </summary>
    /// <param name="deltaTime">經過的時間</param>
    void UpdateUICDTime(float deltaTime)
    {
        GameControl.Instance.GUIStation.Form<UI_Battle>().UpdatePlayerRoleCD(deltaTime);
    }

    /// <summary>
    /// 刪除敵方角色對應的UI資訊（血條、屬性圖示...等）
    /// </summary>
    static void DeleteEnemyInfoUI(string enemyName)
    {
        GameControl.Instance.GUIStation.Form<UI_Battle>().DeleteEnemyInfoUI(enemyName);
    }

    /// <summary>
    /// 戰鬥結束時對UI_Battle的處置
    /// </summary>
    /// <param name="isWin">是否勝利</param>
    void BattleEndProcessUIBattle(bool isWin)
    {
        // 清除所有敵方資訊
        GameControl.Instance.GUIStation.Form<UI_Battle>().ClearEnemyInfoUI();
        if (!isWin)
        {
            // fs : 強制將所有玩家角色UI的HP顯示歸0
            for (int i = 0; i < Players.Length; ++i)
            {
                GameControl.Instance.GUIStation.Form<UI_Battle>().SetPlayerIcon(i, true);
            }
        }
    }
    #endregion
}
/*
 * BattleManager.GetBaseposOfClosestPos ←這裡用了同一個變數重複改值再加入list，如果打算要改成class這裡記得要一起改
 */
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
	public static GridPos operator +(GridPos gp1, GridPos gp2)
	{
		gp1.x += gp2.x;
		gp1.y += gp2.y;
		return gp1;
	}
	public static GridPos operator -(GridPos gp1, GridPos gp2)
	{
		gp1.x -= gp2.x;
		gp1.y -= gp2.y;
		return gp1;
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

/// <summary>
/// 戰鬥格子的資料,最左下為0,0;
/// 因應多格unit，移除防呆檢查，操作簡化只有佔格子跟移除
/// 所有相關防呆跟unit移動部份移到battlemanager中來處理
/// </summary>
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
	public int Cols
	{
		get { return GridColBound + 1; }
	}
	int GridRowBound
	{
		get { return Grids.GetUpperBound(1); }
	}
	public int Rows
	{
		get { return GridRowBound + 1; }
	}
	public Vector3 StartPoint;
	public Vector2 GridSize;
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
			Grids[pos.x, pos.y] = null;
	}
	public void Occupy(GridPos pos,Unit unit)
	{
		if (unit == null)
			return;
		if (PosCheck(pos))
			Grids[pos.x, pos.y] = unit;
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
	public List<GridPos> GetEmptyGrid_RightColumn(int numbers)
	{
		List<GridPos> result = new List<GridPos>();
		for (int i = 0; i < numbers && i <= GridColBound; i++)
			for (int j = 0; j <= GridRowBound; j++)
				if (Grids[GridColBound - i, j] == null)
					result.Add(new GridPos(GridColBound - i, j));
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

	/// <summary>
	/// 取得一組格子的世界座標
	/// （這個是調整過的，放unit的實際圖看起來比較好看一點，中心點請取Get_GridCenterWorldPos）
	/// </summary>
	public Vector3 Get_GridWorldPos(GridPos pos, int GridGroupSize)
	{
		return Get_GridWorldPos(pos.x, pos.y, GridGroupSize);
	}
	/// <summary>
	/// 取得一組格子的世界座標
	/// （這個是調整過的，放unit的實際圖看起來比較好看一點，中心點請取Get_GridCenterWorldPos）
	/// </summary>
	public Vector3 Get_GridWorldPos(int x, int y,int GridGroupSize)
	{
		return StartPoint + new Vector3(x * GridSize.x + (GridSize.x * GridGroupSize) / 2, 0, y * GridSize.y + GridSize.y / 5);
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
	public Unit Get_GridUnit(int x, int y)
	{
		if (!PosCheck(x,y))
			return null;
		return Grids[x, y];
	}

	public bool PosCheck(GridPos pos)
	{
		return pos.x >= 0 && pos.y >= 0 && pos.x <= GridColBound && pos.y <= GridRowBound;
	}
	public bool PosCheck(int x, int y)
	{
		return x >= 0 && y >= 0 && x <= GridColBound && y <= GridRowBound;
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
	public static eBattleResult BattleResult = eBattleResult.None;
	
	public abstract void OnChangeIn(GameControl control);

	public abstract void Update(GameControl control);

	public abstract void OnChangeOut(GameControl control);

	public enum eBattleResult
	{
		None,
		Lose,
		Win,
	}
}


public class BattleEntering : BattleState
{
	private static BattleEntering _instance;

    AsyncOperation loadSceneOp = null;
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
        //Application.LoadLevel("BattleField");
        control.GUIStation.ShowAndHideOther(typeof(UI_Loading_Before_Battle));
        // 為了處理最後換場會跑出格線的問題 GSTAR先暫時這樣處理============
        control.GUIStation.GUICamera.clearFlags = CameraClearFlags.SolidColor;
        control.GUIStation.GUICamera.backgroundColor = Color.black;
        // ================================================================
        control.StartCoroutine(LoadBattleScene());
        TimeMachine.SetTimeScale(1);
		BattleResult = eBattleResult.None;
	}

    IEnumerator LoadBattleScene()
    {
        loadSceneOp = Application.LoadLevelAsync("BattleField");
        yield return loadSceneOp;
    }

	public override void Update(GameControl control)
	{
        if (loadSceneOp != null)
        {
            // 理論上ProgressPercent >= 100 時已經換完場，不過以防萬一多檢查isDone
            if (loadSceneOp.isDone && control.GUIStation.Form<UI_Loading_Before_Battle>().ProgressPercent >= 100.0f)
            {
                loadSceneOp = null;
                control.GUIStation.ResetAllCamera(); // 換場完畢，重設定Camera深度
                // 為了處理最後換場會跑出格線的問題 GSTAR先暫時這樣處理============
                control.GUIStation.GUICamera.clearFlags = CameraClearFlags.Depth;
                // ================================================================
                control.GUIStation.ShowAndHideOther(typeof(UI_Battle));
				SetBackGround();
                GameControl.Instance.ChangeGameState(BattleManager.Instance);
            }
            else
            {
                control.GUIStation.Form<UI_Loading_Before_Battle>().ProgressPercent = loadSceneOp.progress * 100.0f;
            }
        }
        //if (Application.loadedLevelName == "BattleField")
        //{
        //    GameControl.Instance.ChangeGameState(BattleManager.Instance);
        //}
	}
	public void SetBackGround()
	{
		string[] tempBackGround = new string[] { "BambooForest", "GhostIsland" };
		int index = Random.Range(0, tempBackGround.Length);
		GameObject bggo = GameObject.FindGameObjectWithTag(GLOBALCONST.TAG_BACKGROUND);
		if (bggo != null && bggo.renderer != null)
			bggo.renderer.sharedMaterial = ResourceStation.LoadBackGroundMaterial(tempBackGround[index]);
		GameObject ggo = GameObject.FindGameObjectWithTag(GLOBALCONST.TAG_GROUND);
		if (ggo != null && ggo.renderer != null)
			ggo.renderer.sharedMaterial = ResourceStation.LoadGroundMaterial(tempBackGround[index]);
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

	float ChangeTime;
	bool DisplayResult;
	public override void OnChangeIn(GameControl control)
	{
		BattleID = uint.MinValue;
		if (BattleResult != eBattleResult.None)
		{
			ChangeTime = Time.realtimeSinceStartup + 5;
			DisplayResult = true;

            control.GUIStation.Form<UI_Battle>().ShowEnd(BattleResult == eBattleResult.Win);
            #region 臨時顯示結果文字
			//產生顯示結果的TextMesh
            //TextMesh tm = new GameObject("HUDText").AddComponent<TextMesh>();
            //UIFont uifont = UIFontManager.GetUIDynamicFont(UIFontName.MSJH);
            //if (uifont != null)
            //    tm.font = uifont.dynamicFont;
            //else
            //{
            //    Object[] obj = Resources.FindObjectsOfTypeAll(typeof(Font));
            //    if (obj.Length > 0)
            //        tm.font = obj[0] as Font;
            //    else
            //        CommonFunction.DebugError("找不到可用的font");
            //}
            //tm.fontSize = 100;
            //tm.characterSize = 80;
            //tm.lineSpacing = 0.8f;
            //if(tm.font != null)
            //    tm.renderer.sharedMaterial = tm.font.material;
            //tm.anchor = TextAnchor.LowerCenter;
            //tm.text = BattleState.BattleResult.ToString();
            //if (BattleManager.UnitCamera != null)
            //{
            //    tm.gameObject.layer = GLOBALCONST.GameSetting.LAYER_UNIT;
            //    tm.transform.position =BattleManager.UnitCamera.transform.InverseTransformPoint(BattleManager.UnitCamera.transform.forward * (BattleManager.UnitCamera.nearClipPlane + 0.1f));
            //    tm.transform.rotation = BattleManager.UnitCamera.transform.rotation;
            //}


            //TextMesh tm1 = GameObject.Instantiate(tm) as TextMesh;
            //tm1.characterSize = 10;
            //tm1.text = "Tab To Continue";
            //tm1.anchor = TextAnchor.UpperLeft;

			#endregion
		}
		else
		{
			DisplayResult = false; 
			ChangeTime = 0;
			CommonFunction.DebugError("無結果的進入戰鬥結果錯誤");
			Application.LoadLevel("Empty");
		}
	}

	public override void Update(GameControl control)
	{
		if (Input.anyKey)
		{
			SkipResult();
		}
		if (DisplayResult)
		{
			if (Time.realtimeSinceStartup > ChangeTime)
			{
				Application.LoadLevel("Empty");
				DisplayResult = false;
			}
		}
		else
		{
			// fs : 改變GameState到GameStageSelect時，會關閉除了UI_Main_StageSelect以外的介面，
			//      此時會使用DestroyImmediate刪除UIDrawCall，如果在LoadLevel()尚未完成時呼叫，
			//      會出現「Destroying GameObjects immediately is not permitted during physics trigger/contact or animation event callbacks...」的錯誤訊息，
			//      故要在Update中持續等待LoadLevel()完成後才呼叫。
			if (Application.loadedLevelName == "Empty")
			{
                control.GUIStation.ResetAllCamera(); // 換場完畢，重設定Camera深度
				GameControl.Instance.ChangeGameState(GameStageSelect.Instance);
			}
		}
	}

	public static void SkipResult()
	{
		if(_instance != null)
			_instance.ChangeTime = 0;
	}

	public override void OnChangeOut(GameControl control)
	{
		BattleResult = eBattleResult.None;
	}
}
