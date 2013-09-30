# encoding: utf-8

import os.path
from common_func import *

DIR_PROJECT_PARENT_PATH = GetProjectParentPath()
DIR_PROJECT_PATH = GetProjectPath()
DIR_PROJECT_ROOT_DIR = 'nindou_game_server'
DIR_PROJECT_DATA_DIR = 'nindou_game_data'
DIR_PROJECT_SAVE_DIR = 'nindou_game_save'

DATABASE_NAME = 'nindou'

COLL_ACCOUNT = 'account'
COLL_CARD = 'card'
COLL_PLAYERCARD = 'player_card'

# ��w���q POST ������
PROTOCOL_ATTR_MAINKIND = 'MainKind'
PROTOCOL_ATTR_SUBKIND = 'SubKind'
PROTOCOL_ATTR_DEVICE_ID = 'DeviceID' # �˸m�ߤ@�ѧO id
PROTOCOL_ATTR_LOGIN_SESSION = 'LoginSession'