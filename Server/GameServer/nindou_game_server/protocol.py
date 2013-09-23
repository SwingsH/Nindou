from django.http import HttpResponse
from django.template import RequestContext
from django.template import Template, Context
from pymongo import Connection;
import datetime
import json

def handle(request):
    return Protocal.HandleClientRequest(request)
        
class Protocal:
    
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
    
    @staticmethod
    def HandleClientRequest(request):
    
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
        
        result = result + request.GET.get('q', '1235') + request.GET.get('k', 'no')
        
        count = count + 2
        
        #return HttpResponse(result)
        return HttpResponse( count )
