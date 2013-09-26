# encoding: utf-8
import os
import hashlib

def get_upload_path(name):
    return os.path.join(os.path.dirname(__file__), 'upload', name)

# 取得一個唯一 hash key
def GetHashByDict(dict):
    unique_str = ''.join(['%s%s'%(hashlib.sha1(str(key)), hashlib.sha1(str(val))) for (key, val) in sorted(dict.items())])
    return hashlib.sha1(unique_str).hexdigest()