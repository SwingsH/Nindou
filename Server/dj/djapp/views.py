from django.http import HttpResponse
from django.template import RequestContext
from django.template import Template, Context

def ip_address_processor(request):
    return {'ip_address': request.META['REMOTE_ADDR']}

def some_view(request):
    t = Template("My name is {{ my_name }}.")
    #print(t)
    c = RequestContext(request, {
        'foo': 'bar',
    }, [ip_address_processor])
    return HttpResponse(t.render(Context({"my_name": "Swings"})))