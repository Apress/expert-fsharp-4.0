(function()
{
 var Global=this,Runtime=this.IntelliFactory.Runtime,WebSharper,Html,Default,List,MyApplication,MySite,Client,jQuery,T,Android,Context,Bing,Rest,Microsoft,Concurrency,setInterval,Operators;
 Runtime.Define(Global,{
  MyApplication:{
   MySite:{
    Client:{
     ApplicationControl:Runtime.Class({
      get_Body:function()
      {
       return Default.Div(List.ofArray([Client.LoginSequence()]));
      }
     }),
     BingMapsKey:Runtime.Field(function()
     {
      return"Ai8HLRl46mKReJbOmUPHJpKlShDoPxwhLEySwowiX-KCnbPGaD9fYWSF8-Xz-WpQ";
     }),
     LoginSequence:function()
     {
      return Client.ShowMap();
     },
     ShowMap:function()
     {
      var screenWidth,MapOptions,returnVal,label,setMap,map1,x3,f4,f5;
      screenWidth=jQuery("body").width();
      MapOptions=(returnVal=[{}],(null,returnVal[0].credentials=Client.BingMapsKey(),returnVal[0].width=screenWidth-20,returnVal[0].height=screenWidth-40,returnVal[0].zoom=16,returnVal[0]));
      label=Default.Span(Runtime.New(T,{
       $:0
      }));
      setMap=function(map)
      {
       var updateLocation,x2,f3;
       updateLocation=function()
       {
        var matchValue,ctx,x,f,f2;
        matchValue=Context.Get();
        if(matchValue.$==0)
         {
          return null;
         }
        else
         {
          ctx=matchValue.$0;
          if(ctx.get_Geolocator().$==1)
           {
            x=(f=function()
            {
             var x1,f1;
             x1=ctx.get_Geolocator().$0.GetLocation();
             f1=function(_arg1)
             {
              var loc,pin,returnVal1;
              Rest.RequestLocationByPoint(Client.BingMapsKey(),_arg1.Latitude,_arg1.Longitude,List.ofArray(["Address"]),function(result)
              {
               var locInfo;
               locInfo=result.resourceSets[0].resources[0];
               return label.set_Text("You are currently at "+locInfo.name);
              });
              loc=new Microsoft.Maps.Location(_arg1.Latitude,_arg1.Longitude);
              pin=new Microsoft.Maps.Pushpin(loc);
              map.entities.clear();
              map.entities.push(pin);
              map.setView((returnVal1=[{}],(null,returnVal1[0].center=loc,returnVal1[0])));
              return Concurrency.Return(null);
             };
             return Concurrency.Bind(x1,f1);
            },Concurrency.Delay(f));
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
         }
       };
       x2=setInterval(updateLocation,1000);
       f3=function(value)
       {
        value;
       };
       return f3(x2);
      };
      map1=(x3=Default.Div(Runtime.New(T,{
       $:0
      })),(f4=(f5=function(_this)
      {
       var map;
       map=new Microsoft.Maps.Map(_this.Body,MapOptions);
       map.setMapType(Microsoft.Maps.MapTypeId.road);
       return setMap(map);
      },function(w)
      {
       return Operators.OnAfterRender(f5,w);
      }),(f4(x3),x3)));
      return Default.Div(List.ofArray([label,Default.Br(Runtime.New(T,{
       $:0
      })),map1]));
     }
    }
   }
  }
 });
 Runtime.OnInit(function()
 {
  WebSharper=Runtime.Safe(Global.IntelliFactory.WebSharper);
  Html=Runtime.Safe(WebSharper.Html);
  Default=Runtime.Safe(Html.Default);
  List=Runtime.Safe(WebSharper.List);
  MyApplication=Runtime.Safe(Global.MyApplication);
  MySite=Runtime.Safe(MyApplication.MySite);
  Client=Runtime.Safe(MySite.Client);
  jQuery=Runtime.Safe(Global.jQuery);
  T=Runtime.Safe(List.T);
  Android=Runtime.Safe(WebSharper.Android);
  Context=Runtime.Safe(Android.Context);
  Bing=Runtime.Safe(WebSharper.Bing);
  Rest=Runtime.Safe(Bing.Rest);
  Microsoft=Runtime.Safe(Global.Microsoft);
  Concurrency=Runtime.Safe(WebSharper.Concurrency);
  setInterval=Runtime.Safe(Global.setInterval);
  return Operators=Runtime.Safe(Html.Operators);
 });
 Runtime.OnLoad(function()
 {
  Client.BingMapsKey();
 });
}());
