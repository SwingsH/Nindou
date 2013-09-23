# encoding: utf-8

from django.conf.urls import patterns, include, url
from config import * 
from structure import * 

# Uncomment the next two lines to enable the admin:
# from django.contrib import admin
# admin.autodiscover()

urlpatterns = patterns('',
    # Examples:
    # url(r'^$', 'nindou_game_server.views.home', name='home'),
    # url(r'^nindou_game_server/', include('nindou_game_server.foo.urls')),

    # Uncomment the admin/doc line below to enable admin documentation:
    # url(r'^admin/doc/', include('django.contrib.admindocs.urls')),

    # Uncomment the next line to enable the admin:
    # url(r'^admin/', include(admin.site.urls)),
    
    # For client communication interface
    url(r'^protocol/', DIR_PROJECT_ROOT + '.protocol.handle'),
)
