# encoding: utf-8

# Database field name will only apeears here
# ��Ʈw���W�٥u��b�o�� .py �X�{


class Enum(set):
    def __getattr__(self, name):
        if name in self:
            return name
        raise AttributeError
    
class Account:
    Sequence = 0                # �b����Ƭy����
    DeviceUniqueID = 0          # �˸m ID
    FacebookUID = 0             # fb user ID
    PlayerName = ''             # ���a�ۨ��W��
    CurrentLoginSession = ''    # �ثe�n�J���q����
    CreateTime = 0              # �b���إ߮ɶ�
    LastLoginTime = 0           # �W���n�J�ɶ�
    
    def __init__():
        self.UserID = 0
        self.DeviceUniqueID = 0

# �@�ӥd�����
class Card:
    ID = 0          # �d�� ID
    Type = 0        # �d������
    Rare = 1        # �}����
    Name = 0        # �d���W��
    SkillID = 0     # �ޯ����d�����ި쪺 �ޯ� ID
    EquipID = 0     # �˳����d�����ި쪺 �˳� ID
    
    def __init__(self):
        self.ID = 0
        self.Type = 0
    
# �@�Ө�����
class Character:
    ID = 0          # ���� ID
    Rare = 1        # �}����
    BornGiftID = 0  # �ѽ� ID, ���ި�ѽ��
    Name = 0        # ����W��
    MaxHealth = 0   # �̤j HP
    Attack = 0      # �����O
    Defense = 0     # ���m�O
        
    def __init__(self):
        self.ID = 0
        self.Rare = 0
          
# �@�Ӫ��a�֦����d��
class PlayerCard:
    ReadIndex = 0   # �u�� index
    SortIndex = 0   # �Ƨ���ܥ� index
    CardID = 0      # �ѷӨ쪺 card ���
    Quantity = 0    # ���a�֦������i�d�P���ƶq
    
    def __init__(self):
        self.ReadIndex = 0
        self.SortIndex = 0

# �@�Ӫ��a�֦����@�s�d��
class PlayerCardsBox:
    AccountSequence = 0 # �b����Ƭy����
    Cards = {}          # ���a�֦����Ҧ��d��
    
    def __init__(self):
        self.ReadIndex = 0
        self.SortIndex = 0  

# �@�Ӫ��a�֦�������
class PlayerCharacter:
    ReadIndex = 0       # �u�� index
    SortIndex = 0       # �Ƨ���ܥ� index
    CharacterID = 0     # ���� ID
        
    def __init__(self):
        self.ReadIndex = 0
        self.SortIndex = 0
          