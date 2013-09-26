from django.http import HttpResponse
from django.template import RequestContext
from django.template import Template, Context
from django.views.decorators.csrf import csrf_exempt  # for post form CSRF 跨網站的偽造要求
from pymongo import Connection
from config import * 
from dbmanager import * 
from common_func import *  
import datetime
import json

@csrf_exempt # exempt Cross Site Request Forgery protection , 因為 C 端不用 http form 方式處理帳密， 每個 call stack 都要此標籤
def handle(request):
    protocol = Protocol()
    return protocol.HandleClientRequest(request)
        
#test 區
# http://127.0.0.1/protocol/?MainKind=1&SubKind=1&DeviceID=123

class Protocol:
    _database = None
    
    def ip_address_processor(request):
        return {'ip_address': request.META['REMOTE_ADDR']}
    
    def some_view(request):
        t = Template("My name is {{ my_name }}.")
        #print(t)
        c = RequestContext(request, {
            'foo': 'bar',
        }, [ip_address_processor])
        return HttpResponse(t.render(Context({"my_name": "Swings"})))
    
    def some_view(request):
        t = Template("My name is {{ my_name }}.")
        #print(t)
        c = RequestContext(request, {
            'foo': 'bar',
        }, [ip_address_processor])
        return HttpResponse(t.render(Context({"my_name": "Swings"})))
    
    @csrf_exempt  # exempt Cross Site Request Forgery protection , 因為 C 端不用 http form 方式處理帳密， 每個 call stack 都要此標籤
    def HandleClientRequest(self, request):
        responseText = ''
        mainkind = 0
        subkind = 0

        if PROTOCOL_ATTR_MAINKIND in request.POST :
            mainkind = int( request.POST[PROTOCOL_ATTR_MAINKIND])
            subkind = int( request.POST[PROTOCOL_ATTR_SUBKIND])
            return DispatchHandle(mainkind, subkind, request.POST)
        elif PROTOCOL_ATTR_MAINKIND in request.GET : # GET method only for debug
            mainkind = int( request.GET[PROTOCOL_ATTR_MAINKIND])
            subkind = int( request.GET[PROTOCOL_ATTR_SUBKIND])
            return self.DispatchHandle(mainkind, subkind, request.GET)
        else:
            return HttpResponse('error')
            # output error json

        #exception
        # 500 INTERNAL SERVER ERROR
        # 403
    
    # 指派處理 method
    def DispatchHandle(self, mainkind, subkind, postDatas):
        
        # 取得驗證登入需要的資訊
        deviceID = ''
        loginSession = ''
        if PROTOCOL_ATTR_DEVICE_ID in postDatas :
            deviceID = int( postDatas[PROTOCOL_ATTR_DEVICE_ID]) # 裝置唯一識別 id
        if PROTOCOL_ATTR_LOGIN_SESSION in postDatas :
            loginSession = int( request.POST[PROTOCOL_ATTR_LOGIN_SESSION])
        if not deviceID :
            return HttpResponse('error DispatchHandle')
        
        self._database = DBManager(deviceID, loginSession)
 
        if mainkind == 1 and subkind == 1:
            return self.Protocal_1_1_Login(postDatas, deviceID)
        elif mainkind == 1 and subkind == 2:
            return HttpResponse('error')
        elif mainkind == 1 and subkind == 3:
            return HttpResponse('error')
        else:
            return HttpResponse('error')
        
    # C:1-1 一般登入
    def Protocal_1_1_Login(self, postDatas, deviceID):
        newSession = self.GetNewLoginSession(deviceID)
        self._database.UpdateLoginSession(newSession);
        #return HttpResponse(newSession)
        return HttpResponse(self._database.UpdateLoginSession(newSession))
    
    # 取得一個新的 login 通行證
    def GetNewLoginSession(self, deviceID):
        return  GetHashByDict({PROTOCOL_ATTR_DEVICE_ID : deviceID})
