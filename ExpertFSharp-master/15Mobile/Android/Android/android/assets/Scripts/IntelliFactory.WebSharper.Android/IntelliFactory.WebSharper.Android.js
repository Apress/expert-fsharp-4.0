(function()
{
 var Global=this,Runtime=this.IntelliFactory.Runtime,WebSharper,Android,Receiver,Bluetooth,Socket,Arrays,Device,Server,Context,Collections,Dictionary,Connection,Concurrency,Util,Util1,Geolocator,Context1,Control,_FSharpEvent_1,Error,Operators;
 Runtime.Define(Global,{
  IntelliFactory:{
   WebSharper:{
    Android:{
     Bluetooth:{
      Connection:Runtime.Class({
       Dispose:function()
       {
        this.device.Dispose();
        return this.socket.Dispose();
       },
       get_Device:function()
       {
        return this.device;
       },
       get_ServerId:function()
       {
        return this.serverId;
       },
       get_Socket:function()
       {
        return this.socket;
       }
      },{
       New:function(serverId,device,socket)
       {
        var r;
        r={};
        r.serverId=serverId;
        r.device=device;
        r.socket=socket;
        return Runtime.New(this,r);
       }
      }),
      Context:Runtime.Class({
       CancelDiscovery:function()
       {
        return this.bridge.cancelDiscovery();
       },
       ConnectToDevice:function(device,uuid)
       {
        var _this=this;
        return Receiver.MakeAsync(function(uid)
        {
         return _this.bridge.connectToDevice(device.get_Id(),uuid,uid);
        },function(msg)
        {
         return Socket.New(msg.socket,_this.bridge);
        });
       },
       Enable:function()
       {
        var objectArg;
        return Receiver.MakeAsync((objectArg=this.bridge,function(arg00)
        {
         return objectArg.enable(arg00);
        }),function(value)
        {
         value;
        });
       },
       GetBondedDevices:function()
       {
        var devices,r,_this=this;
        devices=this.bridge.getBondedDevices();
        r=Arrays.init(devices.length,function(i)
        {
         return Device.New(devices[i],_this.bridge);
        });
        return r;
       },
       MakeDiscoverable:function(durationSeconds)
       {
        var _this=this;
        return Receiver.MakeAsync(function(uid)
        {
         return _this.bridge.makeDiscoverable(durationSeconds,uid);
        },function(value)
        {
         value;
        });
       },
       Serve:function(name,uuid,handle)
       {
        var serverId,server,_this=this;
        serverId=this.bridge.makeServer(name,uuid);
        server=Server.New(serverId,this.bridge,handle,function()
        {
         var x,f;
         x=_this.servers.Remove(serverId);
         f=function(value)
         {
          value;
         };
         return f(x);
        });
        _this.servers.set_Item(serverId,server);
        _this.bridge.startServer(serverId);
        return server;
       },
       StartDiscovery:function()
       {
        return this.bridge.startDiscovery();
       },
       get_Discovery:function()
       {
        return this.onDiscover;
       },
       get_IsDiscoverable:function()
       {
        return this.bridge.isDiscoverable();
       },
       get_IsDiscovering:function()
       {
        return this.bridge.isDiscovering();
       },
       get_IsEnabled:function()
       {
        return this.bridge.isEnabled();
       }
      },{
       Get:function()
       {
        var b;
        b=Global.AndroidWebSharperBluetoothBridge;
        if(b)
         {
          return{
           $:1,
           $0:Context.New(b)
          };
         }
        else
         {
          return{
           $:0
          };
         }
       },
       New:function(bridge)
       {
        var r,recognize,onAccept,recognize1,f,callback;
        r={};
        r.bridge=bridge;
        r.servers=Dictionary.New2();
        r.onDiscover=(recognize=function(msg)
        {
         return Device.New(msg.device,r.bridge);
        },Receiver.GetEvent("onDiscover",recognize));
        onAccept=(recognize1=function(msg)
        {
         return Connection.New(msg.server,Device.New(msg.device,r.bridge),Socket.New(msg.socket,r.bridge));
        },Receiver.GetEvent("onAccept",recognize1));
        f=(callback=function(conn)
        {
         var server,x,a,f1,f2;
         if(r.servers.ContainsKey(conn.get_ServerId()))
          {
           server=r.servers.get_Item(conn.get_ServerId());
           x=(a=server.Handle(conn),(f1=function()
           {
            return conn.Dispose();
           },Concurrency.TryFinally(a,f1)));
           f2=function(arg00)
           {
            var t;
            t={
             $:0
            };
            return Concurrency.Start(arg00);
           };
           return f2(x);
          }
         else
          {
           return null;
          }
        },function(sourceEvent)
        {
         return Util.addListener(sourceEvent,callback);
        });
        f(onAccept);
        return Runtime.New(this,r);
       }
      }),
      Device:Runtime.Class({
       Dispose:function()
       {
        return this.bridge.disposeDevice(this.id);
       },
       get_Address:function()
       {
        return this.address;
       },
       get_Id:function()
       {
        return this.id;
       },
       get_Name:function()
       {
        return this.name;
       }
      },{
       New:function(id,bridge)
       {
        var r;
        r={};
        r.id=id;
        r.bridge=bridge;
        r.name=r.bridge.deviceName(r.id);
        r.address=r.bridge.deviceAddress(r.id);
        return Runtime.New(this,r);
       }
      }),
      Server:Runtime.Class({
       Dispose:function()
       {
        this.dispose.call(null,null);
        return this.bridge.disposeServer(this.id);
       },
       Handle:function(c)
       {
        return this.handle.call(null,c);
       }
      },{
       New:function(id,bridge,handle,dispose)
       {
        var r;
        r={};
        r.id=id;
        r.bridge=bridge;
        r.handle=handle;
        r.dispose=dispose;
        return Runtime.New(this,r);
       }
      }),
      Socket:Runtime.Class({
       Dispose:function()
       {
        return this.bridge.disposeSocket(this.id);
       },
       Read:function()
       {
        var _this=this;
        return Receiver.MakeAsync(function(uid)
        {
         return _this.bridge.socketRead(_this.id,uid);
        },function(msg)
        {
         return msg.data;
        });
       },
       Write:function(data)
       {
        var _this=this;
        return Receiver.MakeAsync(function(uid)
        {
         return _this.bridge.socketWrite(_this.id,uid,data);
        },function()
        {
         return null;
        });
       }
      },{
       New:function(id,bridge)
       {
        var r;
        r={};
        r.id=id;
        r.bridge=bridge;
        return Runtime.New(this,r);
       }
      })
     },
     Context:Runtime.Class({
      TakePicture:function()
      {
       var _this=this;
       return Receiver.MakeAsync(function(uid)
       {
        return _this.bridge.takePicture(uid);
       },function(msg)
       {
        return msg.jpeg;
       });
      },
      Trace:function(priority,category,message)
      {
       var pr;
       pr=priority.$==1?"info":priority.$==2?"warn":priority.$==3?"error":"debug";
       return this.bridge.trace(pr,category,message);
      },
      get_AccelerationChange:function()
      {
       return Util1.AccelerationChange();
      },
      get_Geolocator:function()
      {
       if(this.bridge.canLocate())
        {
         return{
          $:1,
          $0:Geolocator.New(this.bridge)
         };
        }
       else
        {
         return{
          $:0
         };
        }
      },
      get_IsMeasuringAcceleration:function()
      {
       return this.bridge.accelerometerStarted();
      },
      set_IsMeasuringAcceleration:function(on)
      {
       if(on)
        {
         return this.bridge.accelerometerStart();
        }
       else
        {
         return this.bridge.accelerometerStop();
        }
      }
     },{
      Get:function()
      {
       var b;
       b=Global.AndroidWebSharperBridge;
       if(b)
        {
         return{
          $:1,
          $0:Context1.New(b)
         };
        }
       else
        {
         return{
          $:0
         };
        }
      },
      New:function(bridge)
      {
       var r;
       r={};
       r.bridge=bridge;
       return Runtime.New(this,r);
      }
     }),
     Geolocator:Runtime.Class({
      GetLocation:function()
      {
       var _this=this;
       return Receiver.MakeAsync(function(uid)
       {
        return _this.bridge.getLocation(uid);
       },function(msg)
       {
        return{
         Latitude:msg.lat,
         Longitude:msg.lng
        };
       });
      }
     },{
      New:function(bridge)
      {
       var r;
       r={};
       r.bridge=bridge;
       return Runtime.New(this,r);
      }
     }),
     Receiver:{
      GetEvent:function(name,recognize)
      {
       var e,ev;
       if(Receiver.events().ContainsKey(name))
        {
         return Receiver.events().get_Item(name);
        }
       else
        {
         e=_FSharpEvent_1.New();
         (Receiver.receiver())[name]=function(msg)
         {
          var x;
          x=recognize(msg);
          return e.event.Trigger(x);
         };
         ev=e.event;
         Receiver.events().set_Item(name,ev);
         return ev;
        }
      },
      MakeAsync:function(start,recognize)
      {
       var callback;
       callback=Runtime.Tupled(function(tupledArg)
       {
        var ok,error,_arg1,uid;
        ok=tupledArg[0];
        error=tupledArg[1];
        _arg1=tupledArg[2];
        uid=Receiver.fresh();
        Receiver.handlers().set_Item(uid,function(msg)
        {
         var x,f;
         x=Receiver.handlers().Remove(uid);
         f=function(value)
         {
          value;
         };
         f(x);
         if(msg.error)
          {
           return error(new Error(msg.error));
          }
         else
          {
           return ok(recognize(msg));
          }
        });
        return start(uid);
       });
       return Concurrency.FromContinuations(function(ok)
       {
        return function(no)
        {
         return callback([ok,no,function(value)
         {
          value;
         }]);
        };
       });
      },
      events:Runtime.Field(function()
      {
       return Dictionary.New2();
      }),
      fresh:function()
      {
       Operators.Increment(Receiver.uid());
       return Receiver.uid().contents;
      },
      handlers:Runtime.Field(function()
      {
       var handlers;
       handlers=Dictionary.New2();
       Receiver.receiver().onAsync=function(msg)
       {
        return(handlers.get_Item(msg.uid))(msg);
       };
       return handlers;
      }),
      receiver:Runtime.Field(function()
      {
       var receiver;
       receiver={};
       Global.AndroidWebSharperReceiver=receiver;
       return receiver;
      }),
      uid:Runtime.Field(function()
      {
       return{
        contents:0
       };
      })
     },
     Util:{
      AccelerationChange:Runtime.Field(function()
      {
       var recognize;
       recognize=function(msg)
       {
        return{
         X:msg.x,
         Y:msg.y,
         Z:msg.z
        };
       };
       return Receiver.GetEvent("onAccelerationChange",recognize);
      })
     }
    }
   }
  }
 });
 Runtime.OnInit(function()
 {
  WebSharper=Runtime.Safe(Global.IntelliFactory.WebSharper);
  Android=Runtime.Safe(WebSharper.Android);
  Receiver=Runtime.Safe(Android.Receiver);
  Bluetooth=Runtime.Safe(Android.Bluetooth);
  Socket=Runtime.Safe(Bluetooth.Socket);
  Arrays=Runtime.Safe(WebSharper.Arrays);
  Device=Runtime.Safe(Bluetooth.Device);
  Server=Runtime.Safe(Bluetooth.Server);
  Context=Runtime.Safe(Bluetooth.Context);
  Collections=Runtime.Safe(WebSharper.Collections);
  Dictionary=Runtime.Safe(Collections.Dictionary);
  Connection=Runtime.Safe(Bluetooth.Connection);
  Concurrency=Runtime.Safe(WebSharper.Concurrency);
  Util=Runtime.Safe(WebSharper.Util);
  Util1=Runtime.Safe(Android.Util);
  Geolocator=Runtime.Safe(Android.Geolocator);
  Context1=Runtime.Safe(Android.Context);
  Control=Runtime.Safe(WebSharper.Control);
  _FSharpEvent_1=Runtime.Safe(Control["FSharpEvent`1"]);
  Error=Runtime.Safe(Global.Error);
  return Operators=Runtime.Safe(WebSharper.Operators);
 });
 Runtime.OnLoad(function()
 {
  Util1.AccelerationChange();
  Receiver.uid();
  Receiver.receiver();
  Receiver.handlers();
  Receiver.events();
 });
}());
