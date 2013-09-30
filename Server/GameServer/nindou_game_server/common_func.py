# encoding: utf-8
import os
import os.path
import hashlib
import logging
import time
import datetime
import json
from config import * 

def get_upload_path(name):
    return os.path.join(os.path.dirname(__file__), 'upload', name)

# 取得一個唯一 hash key
def GetHashByDict(dict):
    unique_str = ''.join(['%s%s'%(hashlib.sha1(str(key)), hashlib.sha1(str(val))) for (key, val) in sorted(dict.items())])
    return hashlib.sha1(unique_str).hexdigest()

logger = None # 使用 global 變數 in Django, 即使不同的 http request, 還是能共用同一個 var, diff with PHP, asp 
def DebugLog(message):
    global logger
    global DIR_PROJECT_PARENT_PATH 
    
    if logger == None:
        DIR_PROJECT_PARENT_PATH = GetProjectParentPath()
        now = time.time()
        timsStamp = datetime.datetime.fromtimestamp(now).strftime('%Y%m%d_%H%M%S_')
        logger = logging.getLogger('myapp')
        hdlr = logging.FileHandler(DIR_PROJECT_PARENT_PATH + "\\" + timsStamp  + 'debug.log')
        formatter = logging.Formatter('%(asctime)s %(levelname)s %(message)s')
        hdlr.setFormatter(formatter)
        logger.addHandler(hdlr) 
        logger.setLevel(logging.WARNING)
        
    logger.warning(message)

def ReadFile(fileName):
    if os.path.exists(fileName) == False:
        DebugLog('ReadFile ' + fileName + ' not exist.')
        return ''
    f = open (fileName,"r")
    data = f.read()
    f.close()
    
    return data

# todo: 改成用 jsonrpc 的 jsonclass.load, 要研究看看會不會比較好
#def loads(data):
#This differs from the Python implementation, in that it returns
#the request structure in Dict format instead of the method, params.
#It will return a list in the case of a batch request / response.
def LoadJsonString(jstring):
    if jstring == '':
        DebugLog('Jsonstring empty ')
        return None
    decoded = json.loads(jstring)
    return decoded
    
def GetProjectPath():
    return os.path.dirname(os.path.realpath(__file__))

def GetProjectParentPath():
    path = GetProjectPath()
    parent = path.replace(GetProjectRootDir(), '')
    return parent

def GetProjectRootDir():
    fullPath = GetProjectPath()
    str2 = fullPath.split('\\')
    n = len(str2)
    return str2[n-1]
