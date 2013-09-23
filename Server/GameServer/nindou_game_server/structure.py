# encoding: utf-8

# Database field name will only apeears here
# 資料庫欄位名稱只能在這個 .py 出現


class Enum(set):
    def __getattr__(self, name):
        if name in self:
            return name
        raise AttributeError
    
class Account:
    Sequence = 0                # 帳號資料流水號
    DeviceUniqueID = 0          # 裝置 ID
    FacebookUID = 0             # fb user ID
    PlayerName = ''             # 玩家自取名稱
    CurrentLoginSession = ''    # 目前登入的通行證
    CreateTime = 0              # 帳號建立時間
    LastLoginTime = 0           # 上次登入時間
    
    def __init__():
        self.UserID = 0
        self.DeviceUniqueID = 0

# 一個卡片資料
class Card:
    ID = 0          # 卡片 ID
    Type = 0        # 卡片類型
    Rare = 1        # 稀有度
    Name = 0        # 卡片名稱
    SkillID = 0     # 技能類卡片索引到的 技能 ID
    EquipID = 0     # 裝備類卡片索引到的 裝備 ID
    
    def __init__(self):
        self.ID = 0
        self.Type = 0
    
# 一個角色資料
class Character:
    ID = 0          # 角色 ID
    Rare = 1        # 稀有度
    BornGiftID = 0  # 天賦 ID, 索引到天賦表
    Name = 0        # 角色名稱
    MaxHealth = 0   # 最大 HP
    Attack = 0      # 攻擊力
    Defense = 0     # 防禦力
        
    def __init__(self):
        self.ID = 0
        self.Rare = 0
          
# 一個玩家擁有的卡片
class PlayerCard:
    ReadIndex = 0   # 真實 index
    SortIndex = 0   # 排序顯示用 index
    CardID = 0      # 參照到的 card 資料
    Quantity = 0    # 玩家擁有的此張卡牌的數量
    
    def __init__(self):
        self.ReadIndex = 0
        self.SortIndex = 0

# 一個玩家擁有的一群卡片
class PlayerCardsBox:
    AccountSequence = 0 # 帳號資料流水號
    Cards = {}          # 玩家擁有的所有卡片
    
    def __init__(self):
        self.ReadIndex = 0
        self.SortIndex = 0  

# 一個玩家擁有的角色
class PlayerCharacter:
    ReadIndex = 0       # 真實 index
    SortIndex = 0       # 排序顯示用 index
    CharacterID = 0     # 角色 ID
        
    def __init__(self):
        self.ReadIndex = 0
        self.SortIndex = 0
          