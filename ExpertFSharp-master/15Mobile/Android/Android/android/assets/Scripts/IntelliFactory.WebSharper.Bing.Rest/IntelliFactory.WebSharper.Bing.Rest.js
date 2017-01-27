(function()
{
 var Global=this,Runtime=this.IntelliFactory.Runtime,WebSharper,Unchecked,Bing,Rest,Arrays,Strings,Html,Default,List,document,Seq;
 Runtime.Define(Global,{
  IntelliFactory:{
   WebSharper:{
    Bing:{
     Rest:{
      IsUndefined:function(x)
      {
       return Unchecked.Equals(typeof x,"undefined");
      },
      OptionalFields:function(request,arr)
      {
       var f,chooser;
       f=(chooser=function(name)
       {
        var value;
        value=request[name];
        if(Rest.IsUndefined(value))
         {
          return{
           $:0
          };
         }
        else
         {
          return{
           $:1,
           $0:name+"="+Global.String(value)
          };
         }
       },function(array)
       {
        return Arrays.choose(chooser,array);
       });
       return f(arr);
      },
      RequestCallbackName:Runtime.Field(function()
      {
       return"BingOnReceive";
      }),
      RequestImageryMetadata:function(credentials,request,callback)
      {
       var fields,req,fullReq,self;
       Global[Rest.RequestCallbackName()]=callback;
       fields=Rest.OptionalFields(request,["include","mapVersion","orientation","zoomLevel"]);
       req=Strings.concat("&",fields);
       fullReq=Rest.restApiUri()+"Imagery/Metadata/"+Global.String(request.imagerySet)+(!Rest.IsUndefined(request.centerPoint)?"/"+(self=request.centerPoint,self.x+","+self.y):"")+"?"+req+"&"+Rest.RequestStringBoilerplate(credentials);
       return Rest.SendRequest(fullReq);
      },
      RequestLocationByAddress:function(credentials,address,callback)
      {
       var fields,req,fullReq;
       Global[Rest.RequestCallbackName()]=callback;
       fields=Rest.OptionalFields(address,["adminDistrict","locality","addressLine","countryRegion","postalCode"]);
       req=Strings.concat("&",fields);
       fullReq=Rest.restApiUri()+"Locations?"+req+"&"+Rest.RequestStringBoilerplate(credentials);
       return Rest.SendRequest(fullReq);
      },
      RequestLocationByPoint:function(credentials,x,y,entities,callback)
      {
       var retrieveEntities,req;
       Global[Rest.RequestCallbackName()]=callback;
       retrieveEntities=function(_arg1)
       {
        if(_arg1.$==0)
         {
          return"";
         }
        else
         {
          return"&includeEntityTypes="+Strings.concat(",",_arg1);
         }
       };
       req=Rest.restApiUri()+"Locations/"+Global.String(x)+","+Global.String(y)+"?"+Rest.RequestStringBoilerplate(credentials)+retrieveEntities(entities);
       return Rest.SendRequest(req);
      },
      RequestLocationByQuery:function(credentials,query,callback)
      {
       var req;
       Global[Rest.RequestCallbackName()]=callback;
       req=Rest.restApiUri()+"Locations?query="+query+"&"+Rest.RequestStringBoilerplate(credentials);
       return Rest.SendRequest(req);
      },
      RequestRoute:function(credentials,request,callback)
      {
       var fields,x,f,array1,req,fullReq;
       Global[Rest.RequestCallbackName()]=callback;
       fields=(x=Rest.OptionalFields(request,["avoid","heading","optimize","routePathOutput","distanceUnit","dateTime","timeType","maxSolutions","travelMode"]),(f=(array1=Rest.StringifyWaypoints(request.waypoints),function(array2)
       {
        return array1.concat(array2);
       }),f(x)));
       req=Strings.concat("&",fields);
       fullReq=Rest.restApiUri()+"/Routes?"+req+"&"+Rest.RequestStringBoilerplate(credentials);
       return Rest.SendRequest(fullReq);
      },
      RequestRouteFromMajorRoads:function(credentials,request,callback)
      {
       var fields,req,fullReq;
       Global[Rest.RequestCallbackName()]=callback;
       fields=Rest.OptionalFields(request,["destination","exclude","routePathOutput","distanceUnit"]);
       req=Strings.concat("&",fields);
       fullReq=Rest.restApiUri()+"/Routes/FromMajorRoads?"+req+"&"+Rest.RequestStringBoilerplate(credentials);
       return Rest.SendRequest(fullReq);
      },
      RequestStringBoilerplate:function(credentials)
      {
       return"output=json&jsonp="+Rest.RequestCallbackName()+"&key="+credentials;
      },
      SendRequest:function(req)
      {
       var script,_this,_this1,x,f;
       script=Default.Script(List.ofArray([(_this=Default.Attr(),_this.NewAttr("type","text/javascript")),(_this1=Default.Attr(),_this1.NewAttr("src",req))]));
       x=document.documentElement.appendChild(script.Body);
       f=function(value)
       {
        value;
       };
       return f(x);
      },
      StaticMap:function(credentials,request)
      {
       var _this,x;
       return Default.Img(List.ofArray([(_this=Default.Attr(),(x=Rest.StaticMapUrl(credentials,request),_this.NewAttr("src",x)))]));
      },
      StaticMapUrl:function(credentials,request)
      {
       var fields,query,self2,hasRoute,req,fullReq;
       fields=Seq.toList(Seq.delay(function()
       {
        return Seq.append(Rest.OptionalFields(request,["avoid","dateTime","mapLayer","mapVersion","maxSolutions","optimize","timeType","travelMode","zoomLevel"]),Seq.delay(function()
        {
         var self,self1;
         return Seq.append(!Rest.IsUndefined(request.mapArea)?[(self=request.mapArea[0],self.x+","+self.y)+","+(self1=request.mapArea[1],self1.x+","+self1.y)]:Seq.empty(),Seq.delay(function()
         {
          return Seq.append(!Rest.IsUndefined(request.mapSize)?[Global.String(request.mapSize[0])+","+Global.String(request.mapSize[1])]:Seq.empty(),Seq.delay(function()
          {
           var pushpinToUrlString,x,f,mapping;
           return Seq.append(!Rest.IsUndefined(request.pushpin)?(pushpinToUrlString=function(pin)
           {
            var coords,icstyle,label;
            coords=Global.String(pin.x)+","+Global.String(pin.y);
            icstyle=Rest.IsUndefined(pin.iconStyle)?"":Global.String(pin.iconStyle);
            label=Rest.IsUndefined(pin.label)?"":pin.label;
            return coords+";"+icstyle+";"+label;
           },(x=request.pushpin,(f=(mapping=function(pin)
           {
            return"pp="+pushpinToUrlString(pin);
           },function(array)
           {
            return Arrays.map(mapping,array);
           }),f(x)))):Seq.empty(),Seq.delay(function()
           {
            return Seq.append(!Rest.IsUndefined(request.waypoints)?Rest.StringifyWaypoints(request.waypoints):Seq.empty(),Seq.delay(function()
            {
             return Seq.append(!Rest.IsUndefined(request.declutterPins)?["dcl="+(request.declutterPins?"1":"0")]:Seq.empty(),Seq.delay(function()
             {
              if(!Rest.IsUndefined(request.distanceBeforeFirstTurn))
               {
                return["dbft="+Global.String(request.distanceBeforeFirstTurn)];
               }
              else
               {
                return Seq.empty();
               }
             }));
            }));
           }));
          }));
         }));
        }));
       }));
       query=!Rest.IsUndefined(request.query)?request.query:!Rest.IsUndefined(request.centerPoint)?(self2=request.centerPoint,self2.x+","+self2.y)+"/"+Global.String(request.zoomLevel):"";
       hasRoute=!Rest.IsUndefined(request.waypoints);
       req=Strings.concat("&",fields);
       fullReq=Rest.restApiUri()+"Imagery/Map/"+Global.String(request.imagerySet)+"/"+(hasRoute?"Route/":"")+query+"?"+req+"&key="+credentials;
       return fullReq;
      },
      StringifyWaypoints:function(waypoints)
      {
       return Arrays.mapi(function(i)
       {
        return function(w)
        {
         return"wp."+Global.String(i)+"="+Global.String(w);
        };
       },waypoints);
      },
      credentials:Runtime.Field(function()
      {
       return"Ai6uQaKEyZbUvd33y5HU41hvoov_piUMn6t78Qzg7L1DWY4MFZqhjZdgEmCpQlbe";
      }),
      restApiUri:Runtime.Field(function()
      {
       return"http://dev.virtualearth.net/REST/v1/";
      })
     }
    }
   }
  }
 });
 Runtime.OnInit(function()
 {
  WebSharper=Runtime.Safe(Global.IntelliFactory.WebSharper);
  Unchecked=Runtime.Safe(WebSharper.Unchecked);
  Bing=Runtime.Safe(WebSharper.Bing);
  Rest=Runtime.Safe(Bing.Rest);
  Arrays=Runtime.Safe(WebSharper.Arrays);
  Strings=Runtime.Safe(WebSharper.Strings);
  Html=Runtime.Safe(WebSharper.Html);
  Default=Runtime.Safe(Html.Default);
  List=Runtime.Safe(WebSharper.List);
  document=Runtime.Safe(Global.document);
  return Seq=Runtime.Safe(WebSharper.Seq);
 });
 Runtime.OnLoad(function()
 {
  Rest.restApiUri();
  Rest.credentials();
  Rest.RequestCallbackName();
 });
}());
