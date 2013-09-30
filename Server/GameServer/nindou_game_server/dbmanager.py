# encoding: utf-8

from config import * 
from pymongo import Connection

g_DataBaseManager = None
class DBManager:
    _deviceID = '' #存取 http 者的 device id    
    _loginSession = '' #存取 http 者的  登入 session  
    
    def TestInsert(self):
        con = Connection();
        db = con.test;
        posts = db.post;
        result = ""
        
        post1 = {"title":"I Love Python",
        "slug":"i-love-python",
        "author":"SErHo",
        "content":"I Love Python....",
        "tags":["Love","Python"],
        "time":datetime.datetime.now()};
        
        post2 = {"title":"Python and MongoDB",
         "slug":"python-mongodb",
          "author":"SErHo",
         "content":"Python and MongoDB....",
         "tags":["Python","MongoDB"],
         "time":datetime.datetime.now()};
        
        # posts.insert(post1);
        # posts.insert(post2);
        
        posts = posts.find();
        count = posts.count();
        for post in posts:
            result = result + post['title']
        
        d = {
        'a': 1,
        'b': 'b',
        'list': [1,2,3],
        'dict': {'x':1, 'y':2},
        'tuple': (3, 'a')
        }
        
    def ConnectToMongo(self):
        return Connection();
  
    def GetDatabase(self):
        connection = Connection()
        return connection[DATABASE_NAME]; # 若 DATABASE_NAME 不存在, 此時就會自動建立 new db 了
      
    def UpdateLoginSession(self, newSession):
        #return self.PrintAllDatabaseName()
        
        db  = self.GetDatabase()
    
        #if db.Account is None : # new player login !!
        account = db[COLL_ACCOUNT].find_one({PROTOCOL_ATTR_DEVICE_ID: self._deviceID })
        if account is None: # 該帳號第一次登入
            newAccount = {PROTOCOL_ATTR_DEVICE_ID : self._deviceID, 'session': newSession}
            db[COLL_ACCOUNT].save(newAccount)
        else: # 該帳號非第一次登入, update session
            db[COLL_ACCOUNT].update({PROTOCOL_ATTR_DEVICE_ID: account[PROTOCOL_ATTR_DEVICE_ID]}, {"$set":{"session":newSession}})
        account = db[COLL_ACCOUNT].find_one({PROTOCOL_ATTR_DEVICE_ID: self._deviceID })
        return newSession

    def GetAllDatabaseName(self):
        connection   = self.ConnectToMongo()
        return connection.database_names()
    
    def GetAllCollectionName(self, database):
        return self.GetDatabase().collection_names()
    
    def PrintAllDatabaseName(self):
        allNames = self.GetAllDatabaseName()
        out = 'PrintAllDatabaseName : '
        for name in allNames:
            out = out + name + ','
        return out
    
    def PrintAllCollectionName(self):
        db = self.GetDatabase()
        allNames = self.GetAllCollectionName(db)
        out = 'PrintAllCollectionName : '
        for name in allNames:
            out = out + name + ','
        return out
    
    def IsCollectionExist(self, collectName):
        db = self.GetDatabase()
        allNames = self.GetAllCollectionName(db)
        if collectName in allNames and connection[collectName].count() != 0 : #Check if collection named 'posts' is empty
            return True
        return False
    
    def IsCollectionEmpty(self, collectName):
        if not self.IsCollectionExist(collectName):
            return True
        db = self.GetDatabase()
        if db[collectName].find({}).count() == 0:
            return True
    
    #切換目前登入的使用者資料
    def SwitchUser(self, deviceID, loginSession):
        self._deviceID = str(deviceID)
        self._loginSession = loginSession
    
    def DumpAll(self):
        mon_con = Connection('localhost', 27017)
        mon_db = mon_con.dh
        
        cols = mon_db.collection_names()
        for c in cols:
            print c
        col = raw_input('Input a collection from the list above to show its field names: ')
        
        collection = mon_db[col].find()
        
        keylist = []
        for item in collection:
            for key in item.keys():
                if key not in keylist:
                    keylist.append(key)
                if isinstance(item[key], dict):
                    for subkey in item[key]:
                        subkey_annotated = key + "." + subkey
                        if subkey_annotated not in keylist:
                            keylist.append(subkey_annotated)
                            if isinstance(item[key][subkey], dict):
                                for subkey2 in item[subkey]:
                                    subkey2_annotated = subkey_annotated + "." + subkey2
                                    if subkey2_annotated not in keylist:
                                        keylist.append(subkey2_annotated)
                if isinstance(item[key], list):
                    for l in item[key]:
                        if isinstance(l, dict):
                            for lkey in l.keys():
                                lkey_annotated = key + ".[" + lkey + "]"
                                if lkey_annotated not in keylist:
                                    keylist.append(lkey_annotated)
        keylist.sort()
        for key in keylist:
            keycnt = mon_db[col].find({key:{'$exists':1}}).count()
            print "%-5d\t%s" % (keycnt, key)
          