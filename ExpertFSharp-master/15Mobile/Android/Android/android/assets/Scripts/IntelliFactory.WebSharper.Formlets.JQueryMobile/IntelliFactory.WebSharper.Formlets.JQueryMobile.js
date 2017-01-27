(function()
{
 var Global=this,Runtime=this.IntelliFactory.Runtime,WebSharper,Formlets,JQueryMobile,ControlConfigurations,ButtonConfiguration,JQuery,Mobile,Enums,Transition,_SelectConfiguration_1,List,T,Controls,Arrays,Seq,Utils,_State_1,Html,Default,Math,Operators,HTML5,Formlet,Base,_Result_1,EventsPervasives,Option,jQuery,Operators1,Unchecked,Collections,Dictionary,Formlet1,Formlet2,IconPos,Enhance,Layout,_ElementStore_1,LayoutUtils,Reactive,Reactive1,Data,setTimeout,Enumerator,Body,Util,Control,_FSharpEvent_1;
 Runtime.Define(Global,{
  IntelliFactory:{
   WebSharper:{
    Formlets:{
     JQueryMobile:{
      ControlConfigurations:{
       ButtonConfiguration:Runtime.Class({},{
        get_Default:function()
        {
         return Runtime.New(ButtonConfiguration,{
          Text:"Click",
          Theme:"c",
          Icon:{
           $:0
          },
          Inline:false,
          Link:"#",
          Transition:Transition.get_Slide()
         });
        }
       }),
       "SelectConfiguration`1":Runtime.Class({},{
        Basic:function(values,def)
        {
         return Runtime.New(_SelectConfiguration_1,{
          NativeSelect:false,
          Placeholder:{
           $:0
          },
          MultipleChoices:false,
          Inline:false,
          Icon:{
           $:0
          },
          Values:{
           $:1,
           $0:values
          },
          DefaultValue:{
           $:0,
           $0:def
          },
          Disabled:Runtime.New(T,{
           $:0
          })
         });
        }
       })
      },
      Controls:{
       Button:function(text,theme)
       {
        var inputRecord;
        return Controls.CustomButton((inputRecord=ButtonConfiguration.get_Default(),Runtime.New(ButtonConfiguration,{
         Text:text,
         Theme:theme,
         Icon:inputRecord.Icon,
         Inline:inputRecord.Inline,
         Link:inputRecord.Link,
         Transition:inputRecord.Transition
        })));
       },
       CheckboxGroup:function(values,horizontal,theme)
       {
        var x;
        x=function()
        {
         var current,f,g,f1,state,f2,opts,x2,f3,mapping,f6,containerId,container,x7,_this7,arg001,_this8,_this9,arg002,reset;
         current=Arrays.init(Seq.length(values),(f=Runtime.Tupled(function(tuple)
         {
          return tuple[1];
         }),(g=(f1=function(index)
         {
          return function(source)
          {
           return Seq.nth(index,source);
          };
         },function(x1)
         {
          return Utils.swap(f1,values,x1);
         }),function(x1)
         {
          return f(g(x1));
         })));
         state=(f2=function(arg00)
         {
          return _State_1.NewSuccess(arg00);
         },f2(current));
         opts=(x2=(f3=(mapping=function(i)
         {
          return Runtime.Tupled(function(tupledArg)
          {
           var label,value,check,_this,id,x1,x3,_this1,_this2,_this3,arg10,_this4,arg00,f4,x4,f5,x5,x6,_this5,_this6;
           label=tupledArg[0];
           value=tupledArg[1];
           check=value?List.ofArray([(_this=Default.Attr(),_this.NewAttr("checked","checked"))]):Runtime.New(T,{
            $:0
           });
           id="id"+Math.round(Math.random()*100000000);
           return List.ofArray([(x1=(x3=Operators.add(Default.Input(List.ofArray([(_this1=Default.Attr(),_this1.NewAttr("type","checkbox")),(_this2=Default.Attr(),_this2.NewAttr("id",id)),(_this3=Default.Attr(),_this3.NewAttr("value",id)),(arg10=Global.String(theme),(_this4=HTML5.Attr(),(arg00="data-"+"theme",_this4.NewAttr(arg00,arg10))))])),check),(f4=(x4=function()
           {
            return function()
            {
             current[i]=!current[i];
             return state.Trigger(Runtime.New(_Result_1,{
              $:0,
              $0:current
             }));
            };
           },function(arg101)
           {
            return EventsPervasives.Events().OnClick(x4,arg101);
           }),(f4(x3),x3))),(f5=(x5=function()
           {
            current[i]=!current[i];
            return state.Trigger(Runtime.New(_Result_1,{
             $:0,
             $0:current
            }));
           },function(arg101)
           {
            return EventsPervasives.Events().OnChange(x5,arg101);
           }),(f5(x1),x1))),(x6=List.ofArray([(_this5=Default.Attr(),_this5.NewAttr("for",id)),Default.Text(label)]),(_this6=Default.Tags(),_this6.NewTag("label",x6)))]);
          });
         },function(source)
         {
          return Seq.mapi(mapping,source);
         }),f3(values)),(f6=function(lists)
         {
          return List.concat(lists);
         },f6(x2)));
         containerId="id"+Math.round(Math.random()*100000000);
         container=Operators.add(Operators.add((x7=List.ofArray([(_this7=HTML5.Attr(),(arg001="data-"+"role",_this7.NewAttr(arg001,"controlgroup")))]),(_this8=Default.Tags(),_this8.NewTag("fieldset",x7))),horizontal?List.ofArray([(_this9=HTML5.Attr(),(arg002="data-"+"type",_this9.NewAttr(arg002,"horizontal")))]):Runtime.New(T,{
          $:0
         })),opts);
         reset=function()
         {
          var f4,action,f5,action1,x1,f7,f8;
          container["HtmlProvider@32"].Clear(container.Body);
          f4=(action=function(arg00)
          {
           return container.AppendI(arg00);
          },function(list)
          {
           return Seq.iter(action,list);
          });
          f4(opts);
          f5=(action1=function(i)
          {
           return Runtime.Tupled(function(tupledArg)
           {
            var _arg4,d;
            _arg4=tupledArg[0];
            d=tupledArg[1];
            current[i]=d;
           });
          },function(source)
          {
           return Seq.iteri(action1,source);
          });
          f5(values);
          x1=(f7=function(arg0)
          {
           return Runtime.New(_Result_1,{
            $:0,
            $0:arg0
           });
          },f7(current));
          f8=function(arg00)
          {
           return state.Trigger(arg00);
          };
          return f8(x1);
         };
         return[container,reset,state];
        };
        return Utils.MkFormlet(x);
       },
       CustomButton:function(config)
       {
        var x;
        x=function()
        {
         var icon,x1,x2,f,mapping,f1,f2,g,g1,f3,iconpos,x4,x5,f4,mapping1,f5,f6,g2,g3,f7,button,arg101,_this1,arg001,_this2,arg002,arg102,_this3,arg003,_this4,arg004,_this5,arg005,state,count,reset;
         icon=(x1=(x2=config.Icon,(f=(mapping=(f1=(f2=Runtime.Tupled(function(tuple)
         {
          return tuple[0];
         }),(g=function(value)
         {
          return Global.String(value);
         },function(x3)
         {
          return g(f2(x3));
         })),(g1=function(arg10)
         {
          var _this,arg00;
          _this=HTML5.Attr();
          arg00="data-"+"icon";
          return _this.NewAttr(arg00,arg10);
         },function(x3)
         {
          return g1(f1(x3));
         })),function(option)
         {
          return Option.map(mapping,option);
         }),f(x2))),(f3=function(option)
         {
          return Option.toList(option);
         },f3(x1)));
         iconpos=(x4=(x5=config.Icon,(f4=(mapping1=(f5=(f6=Runtime.Tupled(function(tuple)
         {
          return tuple[1];
         }),(g2=function(x3)
         {
          return x3.get_Value();
         },function(x3)
         {
          return g2(f6(x3));
         })),(g3=function(arg10)
         {
          var _this,arg00;
          _this=HTML5.Attr();
          arg00="data-"+"iconpos";
          return _this.NewAttr(arg00,arg10);
         },function(x3)
         {
          return g3(f5(x3));
         })),function(option)
         {
          return Option.map(mapping1,option);
         }),f4(x5))),(f7=function(option)
         {
          return Option.toList(option);
         },f7(x4)));
         button=Operators.add(Operators.add(Operators.add(Operators.add(Default.A(List.ofArray([(arg101=Global.String(config.Theme),(_this1=HTML5.Attr(),(arg001="data-"+"theme",_this1.NewAttr(arg001,arg101)))),(_this2=HTML5.Attr(),(arg002="data-"+"role",_this2.NewAttr(arg002,"button"))),(arg102=config.Transition.get_Value(),(_this3=HTML5.Attr(),(arg003="data-"+"transition",_this3.NewAttr(arg003,arg102)))),Default.HRef(config.Link),Default.Text(config.Text)])),config.Inline?List.ofArray([(_this4=HTML5.Attr(),(arg004="data-"+"inline",_this4.NewAttr(arg004,"true")))]):Runtime.New(T,{
          $:0
         })),config.Transition.get_Reverse()?List.ofArray([(_this5=HTML5.Attr(),(arg005="data-"+"direction",_this5.NewAttr(arg005,"reverse")))]):Runtime.New(T,{
          $:0
         })),icon),iconpos);
         state=_State_1.NewFail();
         count={
          contents:0
         };
         jQuery(button.Body).click(function()
         {
          state.Trigger(Runtime.New(_Result_1,{
           $:0,
           $0:count.contents
          }));
          return Operators1.Increment(count);
         });
         reset=function()
         {
          count.contents=0;
         };
         return[button,reset,state];
        };
        return Utils.MkFormlet(x);
       },
       CustomField:function(fieldType,def,theme)
       {
        var x;
        x=function()
        {
         var id,input,_this,_this1,_this2,arg10,_this3,arg00,state,reset;
         id="id"+Math.round(Math.random()*100000000);
         input=Default.Input(List.ofArray([(_this=Default.Attr(),_this.NewAttr("type",fieldType)),(_this1=Default.Attr(),_this1.NewAttr("name",id)),(_this2=Default.Attr(),_this2.NewAttr("value",def)),(arg10=Global.String(theme),(_this3=HTML5.Attr(),(arg00="data-"+"theme",_this3.NewAttr(arg00,arg10))))]));
         state=_State_1.NewSuccess(def);
         jQuery(input.Body).keyup(function()
         {
          return state.Trigger(Runtime.New(_Result_1,{
           $:0,
           $0:input.get_Value()
          }));
         });
         reset=function()
         {
          input.set_Value(def);
          return state.Trigger(Runtime.New(_Result_1,{
           $:0,
           $0:def
          }));
         };
         return[input,reset,state];
        };
        return Utils.MkFormlet(x);
       },
       Email:Runtime.Field(function()
       {
        return function(def)
        {
         return function(theme)
         {
          return Controls.CustomField("email",def,theme);
         };
        };
       }),
       FlipToggle:function(ontext,offtext,def,theme)
       {
        var x;
        x=function()
        {
         var id,state,current,select,x1,_this,arg00,_this1,_this2,arg001,arg10,_this3,arg002,_this4,x2,_this5,_this6,_this7,x3,_this8,_this9,f,f1,reset;
         id="id"+Math.round(Math.random()*100000000);
         state=_State_1.NewSuccess(def);
         current={
          contents:def
         };
         select=(x1=Default.Select(List.ofArray([(_this=Default.Attr(),_this.NewAttr("name",id)),(arg00=Global.String(def),(_this1=Default.Attr(),_this1.NewAttr("value",arg00))),(_this2=HTML5.Attr(),(arg001="data-"+"role",_this2.NewAttr(arg001,"slider"))),(arg10=Global.String(theme),(_this3=HTML5.Attr(),(arg002="data-"+"theme",_this3.NewAttr(arg002,arg10)))),Operators.add((_this4=Default.Tags(),(x2=List.ofArray([(_this5=Default.Attr(),_this5.NewAttr("value","off")),Default.Text(offtext)]),_this4.NewTag("option",x2))),!def?List.ofArray([(_this6=Default.Attr(),_this6.NewAttr("selected","selected"))]):Runtime.New(T,{
          $:0
         })),Operators.add((_this7=Default.Tags(),(x3=List.ofArray([(_this8=Default.Attr(),_this8.NewAttr("value","on")),Default.Text(ontext)]),_this7.NewTag("option",x3))),def?List.ofArray([(_this9=Default.Attr(),_this9.NewAttr("selected","selected"))]):Runtime.New(T,{
          $:0
         }))])),(f=(f1=function(_thisa)
         {
          Controls["JQuery.get_JQMobileSlider"](jQuery(_thisa.Body));
          return jQuery(_thisa.Body).siblings(".ui-slider").bind("vmouseup",function()
          {
           current.contents=!current.contents;
           return state.Trigger(Runtime.New(_Result_1,{
            $:0,
            $0:current.contents
           }));
          });
         },function(w)
         {
          return Operators.OnAfterRender(f1,w);
         }),(f(x1),x1)));
         reset=function()
         {
          var value,objectArg,arg003;
          value=def?1:0;
          objectArg=select["HtmlProvider@32"];
          ((arg003=select.Body,function(arg101)
          {
           return function(arg20)
           {
            return objectArg.SetProperty(arg003,arg101,arg20);
           };
          })("selectedIndex"))(value);
          Controls["JQuery.get_JQMobileSliderRefresh"](jQuery(select.Body));
          return state.Trigger(Runtime.New(_Result_1,{
           $:0,
           $0:def
          }));
         };
         return[select,reset,state];
        };
        return Utils.MkFormlet(x);
       },
       "JQuery.get_JQMobileSelectMenu":function(_)
       {
        var unitVar1,f;
        unitVar1=[];
        f=function(elem)
        {
         return elem.selectmenu();
        };
        f(_);
        return _;
       },
       "JQuery.get_JQMobileSelectMenuRefresh":function(_)
       {
        var unitVar1,f;
        unitVar1=[];
        f=function(elem)
        {
         return elem.selectmenu("refresh");
        };
        f(_);
        return _;
       },
       "JQuery.get_JQMobileSelectMenuRefreshForce":function(_)
       {
        var unitVar1,f;
        unitVar1=[];
        f=function(elem)
        {
         return elem.selectmenu("refresh",true);
        };
        f(_);
        return _;
       },
       "JQuery.get_JQMobileSlider":function(_)
       {
        var unitVar1,f;
        unitVar1=[];
        f=function(elem)
        {
         return elem.slider();
        };
        f(_);
        return _;
       },
       "JQuery.get_JQMobileSliderRefresh":function(_)
       {
        var unitVar1,f;
        unitVar1=[];
        f=function(elem)
        {
         return elem.slider("refresh");
        };
        f(_);
        return _;
       },
       Number:Runtime.Field(function()
       {
        return function(def)
        {
         return function(theme)
         {
          return Controls.CustomField("number",def,theme);
         };
        };
       }),
       Password:Runtime.Field(function()
       {
        return function(def)
        {
         return function(theme)
         {
          return Controls.CustomField("password",def,theme);
         };
        };
       }),
       RadioGroup:function(values,def,horizontal,theme)
       {
        var x;
        x=function()
        {
         var state,opts,radioId,x1,f,mapping,f3,containerId,container,x7,_this8,arg001,_this9,_thisa,arg002,reset;
         state=_State_1.NewSuccess(def);
         opts=(radioId="id"+Math.round(Math.random()*100000000),(x1=(f=(mapping=Runtime.Tupled(function(tupledArg)
         {
          var label,value,check,_this,id,x2,x3,_this1,_this2,_this3,_this4,arg10,_this5,arg00,f1,x4,f2,x5,x6,_this6,_this7;
          label=tupledArg[0];
          value=tupledArg[1];
          check=Unchecked.Equals(value,def)?List.ofArray([(_this=Default.Attr(),_this.NewAttr("checked","checked"))]):Runtime.New(T,{
           $:0
          });
          id="id"+Math.round(Math.random()*100000000);
          return List.ofArray([(x2=(x3=Operators.add(Default.Input(List.ofArray([(_this1=Default.Attr(),_this1.NewAttr("type","radio")),(_this2=Default.Attr(),_this2.NewAttr("name",radioId)),(_this3=Default.Attr(),_this3.NewAttr("id",id)),(_this4=Default.Attr(),_this4.NewAttr("value",id)),(arg10=Global.String(theme),(_this5=HTML5.Attr(),(arg00="data-"+"theme",_this5.NewAttr(arg00,arg10))))])),check),(f1=(x4=function()
          {
           return function()
           {
            return state.Trigger(Runtime.New(_Result_1,{
             $:0,
             $0:value
            }));
           };
          },function(arg101)
          {
           return EventsPervasives.Events().OnClick(x4,arg101);
          }),(f1(x3),x3))),(f2=(x5=function()
          {
           return state.Trigger(Runtime.New(_Result_1,{
            $:0,
            $0:value
           }));
          },function(arg101)
          {
           return EventsPervasives.Events().OnChange(x5,arg101);
          }),(f2(x2),x2))),(x6=List.ofArray([(_this6=Default.Attr(),_this6.NewAttr("for",id)),Default.Text(label)]),(_this7=Default.Tags(),_this7.NewTag("label",x6)))]);
         }),function(source)
         {
          return Seq.map(mapping,source);
         }),f(values)),(f3=function(lists)
         {
          return List.concat(lists);
         },f3(x1))));
         containerId="id"+Math.round(Math.random()*100000000);
         container=Operators.add(Operators.add((x7=List.ofArray([(_this8=HTML5.Attr(),(arg001="data-"+"role",_this8.NewAttr(arg001,"controlgroup")))]),(_this9=Default.Tags(),_this9.NewTag("fieldset",x7))),horizontal?List.ofArray([(_thisa=HTML5.Attr(),(arg002="data-"+"type",_thisa.NewAttr(arg002,"horizontal")))]):Runtime.New(T,{
          $:0
         })),opts);
         reset=function()
         {
          var f1,action,x2,f2,f4;
          container["HtmlProvider@32"].Clear(container.Body);
          f1=(action=function(arg00)
          {
           return container.AppendI(arg00);
          },function(list)
          {
           return Seq.iter(action,list);
          });
          f1(opts);
          x2=(f2=function(arg0)
          {
           return Runtime.New(_Result_1,{
            $:0,
            $0:arg0
           });
          },f2(def));
          f4=function(arg00)
          {
           return state.Trigger(arg00);
          };
          return f4(x2);
         };
         return[container,reset,state];
        };
        return Utils.MkFormlet(x);
       },
       Search:Runtime.Field(function()
       {
        return function(def)
        {
         return function(theme)
         {
          return Controls.CustomField("search",def,theme);
         };
        };
       }),
       SelectMenu:function(config,theme)
       {
        var x;
        x=function()
        {
         var dict,defaults,matchValue,c,c1,state,f,genOption,genOptGroup,select,x3,x4,arg10,_this3,arg001,matchValue1,ph,_this4,x5,matchValue2,values,f3,groups,f4,arg101,_this5,arg002,_this6,arg003,matchValue3,icon,arg102,_this7,arg004,f5,f6,f7,x6,reset;
         dict=Dictionary.New2();
         defaults=(matchValue=config.DefaultValue,matchValue.$==1?(c=matchValue.$0,List.ofSeq(c)):(c1=matchValue.$0,List.ofArray([c1])));
         state=(f=function(arg00)
         {
          return _State_1.NewSuccess(arg00);
         },f(defaults));
         genOption=Runtime.Tupled(function(tupledArg)
         {
          var label,value,id,_this,x1,_this1,x2,f1,predicate,_this2,f2,predicate1;
          label=tupledArg[0];
          value=tupledArg[1];
          id="id"+Math.round(Math.random()*100000000);
          dict.Add(id,value);
          return Operators.add(Operators.add((_this=Default.Tags(),(x1=List.ofArray([(_this1=Default.Attr(),_this1.NewAttr("value",id)),Default.Text(label)]),_this.NewTag("option",x1))),(x2=config.Disabled,(f1=(predicate=function(y)
          {
           return Unchecked.Equals(value,y);
          },function(list)
          {
           return Seq.exists(predicate,list);
          }),f1(x2)))?List.ofArray([(_this2=Default.Attr(),_this2.NewAttr("disabled","disabled"))]):Runtime.New(T,{
           $:0
          })),(f2=(predicate1=function(y)
          {
           return Unchecked.Equals(value,y);
          },function(source)
          {
           return Seq.exists(predicate1,source);
          }),f2(defaults))?List.ofArray([Default.Selected("selected")]):Runtime.New(T,{
           $:0
          }));
         });
         genOptGroup=Runtime.Tupled(function(tupledArg)
         {
          var label,opts,x1,_this,_this1,f1;
          label=tupledArg[0];
          opts=tupledArg[1];
          return Operators.add((x1=List.ofArray([(_this=Default.Attr(),_this.NewAttr("label",label))]),(_this1=Default.Tags(),_this1.NewTag("optgroup",x1))),(f1=function(source)
          {
           return Seq.map(genOption,source);
          },f1(opts)));
         });
         select=(x3=(x4=Operators.add(Operators.add(Operators.add(Operators.add(Operators.add(Default.Select(List.ofArray([(arg10=Global.String(theme),(_this3=HTML5.Attr(),(arg001="data-"+"theme",_this3.NewAttr(arg001,arg10))))])),(matchValue1=config.Placeholder,matchValue1.$==1?(ph=matchValue1.$0,List.ofArray([(_this4=Default.Tags(),(x5=List.ofArray([Default.Text(ph)]),_this4.NewTag("option",x5)))])):Runtime.New(T,{
          $:0
         }))),(matchValue2=config.Values,matchValue2.$==1?(values=matchValue2.$0,(f3=function(source)
         {
          return Seq.map(genOption,source);
         },f3(values))):(groups=matchValue2.$0,(f4=function(source)
         {
          return Seq.map(genOptGroup,source);
         },f4(groups))))),List.ofArray([(arg101=Global.String(config.NativeSelect),(_this5=HTML5.Attr(),(arg002="data-"+"native-menu",_this5.NewAttr(arg002,arg101))))])),config.Inline?List.ofArray([(_this6=HTML5.Attr(),(arg003="data-"+"inline",_this6.NewAttr(arg003,"true")))]):Runtime.New(T,{
          $:0
         })),(matchValue3=config.Icon,matchValue3.$==1?(icon=matchValue3.$0,List.ofArray([(arg102=Global.String(icon),(_this7=HTML5.Attr(),(arg004="data-"+"icon",_this7.NewAttr(arg004,arg102))))])):Runtime.New(T,{
          $:0
         }))),(f5=(f6=function(select1)
         {
          return Controls["JQuery.get_JQMobileSelectMenu"](jQuery(select1.Body).bind("create",function()
          {
           return Controls["JQuery.get_JQMobileSelectMenuRefresh"](jQuery(select1.Body));
          }));
         },function(w)
         {
          return Operators.OnAfterRender(f6,w);
         }),(f5(x4),x4))),(f7=(x6=function(select1)
         {
          var value,x1,x2,x7,x8,f1,f2,f8,f9,x9,xa,xb,fa,mapping,fb,fc,fd;
          value=jQuery(select1.Body).val();
          if(Unchecked.Equals(typeof value,"string"))
           {
            if(value==="")
             {
              return state.Trigger(Runtime.New(_Result_1,{
               $:1,
               $0:Runtime.New(T,{
                $:0
               })
              }));
             }
            else
             {
              x1=(x2=(x7=(x8=dict.get_Item(value),(f1=function(value1)
              {
               return[value1];
              },f1(x8))),(f2=function(source)
              {
               return List.ofSeq(source);
              },f2(x7))),(f8=function(arg0)
              {
               return Runtime.New(_Result_1,{
                $:0,
                $0:arg0
               });
              },f8(x2)));
              f9=function(arg00)
              {
               return state.Trigger(arg00);
              };
              return f9(x1);
             }
           }
          else
           {
            x9=(xa=(xb=(fa=(mapping=function(xc)
            {
             return dict.get_Item(xc);
            },function(source)
            {
             return Seq.map(mapping,source);
            }),fa(value)),(fb=function(source)
            {
             return List.ofSeq(source);
            },fb(xb))),(fc=function(arg0)
            {
             return Runtime.New(_Result_1,{
              $:0,
              $0:arg0
             });
            },fc(xa)));
            fd=function(arg00)
            {
             return state.Trigger(arg00);
            };
            return fd(x9);
           }
         },function(arg103)
         {
          return EventsPervasives.Events().OnChange(x6,arg103);
         }),(f7(x3),x3)));
         reset=function()
         {
          return Controls["JQuery.get_JQMobileSelectMenuRefresh"](jQuery(select.Body).val(defaults));
         };
         return[select,reset,state];
        };
        return Utils.MkFormlet(x);
       },
       Slider:function(min,max,def,theme)
       {
        var x;
        x=function()
        {
         var id,state,slider,x1,x2,arg10,_this,arg00,arg101,_this1,arg001,_this2,arg002,_this3,arg003,_this4,arg004,_this5,f,f1,f4,reset;
         id="id"+Math.round(Math.random()*100000000);
         state=_State_1.NewSuccess(def);
         slider=(x1=(x2=Default.Input(List.ofArray([(arg10=Global.String(theme),(_this=HTML5.Attr(),(arg00="data-"+"theme",_this.NewAttr(arg00,arg10)))),(arg101=Global.String(theme),(_this1=HTML5.Attr(),(arg001="data-"+"track-theme",_this1.NewAttr(arg001,arg101)))),(_this2=Default.Attr(),_this2.NewAttr("id",id)),(arg002=Global.String(def),(_this3=Default.Attr(),_this3.NewAttr("value",arg002))),(arg003=Global.String(min),(_this4=HTML5.Attr(),_this4.NewAttr("min",arg003))),(arg004=Global.String(max),(_this5=HTML5.Attr(),_this5.NewAttr("max",arg004)))])),(f=(f1=function(_this6)
         {
          Controls["JQuery.get_JQMobileSlider"](jQuery(_this6.Body));
          return jQuery("#"+id).live("change",function()
          {
           var x3,x4,f2,f3;
           x3=(x4=jQuery("#"+id).val(),(f2=function(arg0)
           {
            return Runtime.New(_Result_1,{
             $:0,
             $0:arg0
            });
           },f2(x4)));
           f3=function(arg005)
           {
            return state.Trigger(arg005);
           };
           return f3(x3);
          });
         },function(w)
         {
          return Operators.OnAfterRender(f1,w);
         }),(f(x2),x2))),(f4=function(x3)
         {
          return Default.Div(List.ofArray([x3]));
         },f4(x1)));
         reset=function()
         {
          slider.set_Value(def);
          Controls["JQuery.get_JQMobileSliderRefresh"](jQuery(slider.Body));
          return state.Trigger(Runtime.New(_Result_1,{
           $:0,
           $0:def
          }));
         };
         return[slider,reset,state];
        };
        return Utils.MkFormlet(x);
       },
       Tel:Runtime.Field(function()
       {
        return function(def)
        {
         return function(theme)
         {
          return Controls.CustomField("tel",def,theme);
         };
        };
       }),
       TextArea:function(def,theme)
       {
        var x;
        x=function()
        {
         var id,input,arg10,_this,arg00,_this1,_this2,state,reset;
         id="id"+Math.round(Math.random()*100000000);
         input=Default.TextArea(List.ofArray([(arg10=Global.String(theme),(_this=HTML5.Attr(),(arg00="data-"+"theme",_this.NewAttr(arg00,arg10)))),(_this1=Default.Attr(),_this1.NewAttr("name",id)),(_this2=Default.Attr(),_this2.NewAttr("value",def))]));
         state=_State_1.NewSuccess(def);
         jQuery(input.Body).keyup(function()
         {
          return state.Trigger(Runtime.New(_Result_1,{
           $:0,
           $0:input.get_Value()
          }));
         });
         reset=function()
         {
          input.set_Value(def);
          return state.Trigger(Runtime.New(_Result_1,{
           $:0,
           $0:def
          }));
         };
         return[input,reset,state];
        };
        return Utils.MkFormlet(x);
       },
       TextField:Runtime.Field(function()
       {
        return function(def)
        {
         return function(theme)
         {
          return Controls.CustomField("text",def,theme);
         };
        };
       }),
       Url:Runtime.Field(function()
       {
        return function(def)
        {
         return function(theme)
         {
          return Controls.CustomField("url",def,theme);
         };
        };
       })
      },
      Enhance:{
       WithCustomSubmitButton:function(config,formlet)
       {
        var x,_builder_,f2;
        x=(_builder_=Formlet2.Do(),_builder_.Delay(function()
        {
         var x1,f,f1;
         return _builder_.Bind((x1=(f=function(formlet1)
         {
          return Formlet2.LiftResult(formlet1);
         },f(formlet)),(f1=function(formlet1)
         {
          return Formlet2.InitWithFailure(formlet1);
         },f1(x1))),function(_arg2)
         {
          return _builder_.Bind(Controls.CustomButton(config),function()
          {
           var value;
           if(_arg2.$==0)
            {
             value=_arg2.$0;
             return _builder_.Return(value);
            }
           else
            {
             return _builder_.ReturnFrom(Formlet2.Never());
            }
          });
         });
        }));
        f2=function(formlet1)
        {
         return Formlet2.Vertical(formlet1);
        };
        return f2(x);
       },
       WithSubmitButton:function(text,theme)
       {
        var config,inputRecord;
        config=(inputRecord=ButtonConfiguration.get_Default(),Runtime.New(ButtonConfiguration,{
         Text:text,
         Theme:theme,
         Icon:{
          $:1,
          $0:["check",IconPos.get_Left()]
         },
         Inline:inputRecord.Inline,
         Link:inputRecord.Link,
         Transition:inputRecord.Transition
        }));
        return function(formlet)
        {
         return Enhance.WithCustomSubmitButton(config,formlet);
        };
       }
      },
      Layout:{
       "ElementStore`1":Runtime.Class({
        Init:function()
        {
         this.store=Dictionary.New2();
        },
        RegisterElement:function(key,f)
        {
         var x,f1;
         if(x=this.store.ContainsKey(key),(f1=function(value)
         {
          return!value;
         },f1(x)))
          {
           return this.store.set_Item(key,f);
          }
         else
          {
           return null;
          }
        },
        Remove:function(key)
        {
         var x,f;
         if(this.store.ContainsKey(key))
          {
           (this.store.get_Item(key))(null);
           x=this.store.Remove(key);
           f=function(value)
           {
            value;
           };
           return f(x);
          }
         else
          {
           return null;
          }
        }
       },{
        New:function()
        {
         var r;
         r={};
         return Runtime.New(this,r);
        },
        NewElementStore:function()
        {
         var store;
         store=_ElementStore_1.New();
         store.Init();
         return store;
        }
       }),
       GroupControls:function(horizontal,elems)
       {
        var _this,arg00,_this1,arg001,x,x1,f,f1,f2;
        return Operators.add(Operators.add(Default.Div(List.ofArray([(_this=HTML5.Attr(),(arg00="data-"+"role",_this.NewAttr(arg00,"controlgroup")))])),horizontal?List.ofArray([(_this1=HTML5.Attr(),(arg001="data-"+"type",_this1.NewAttr(arg001,"horizontal")))]):Runtime.New(T,{
         $:0
        })),(x=(x1=(f=function(fs)
        {
         return Formlet2.Sequence(fs);
        },f(elems)),(f1=function(formlet)
        {
         return Formlet2.WithLayout(Layout.WithNoLayout(),formlet);
        },f1(x1))),(f2=function(value)
        {
         return[value];
        },f2(x))));
       },
       LayoutUtils:Runtime.Field(function()
       {
        return LayoutUtils.New({
         Reactive:Reactive1.Default()
        });
       }),
       Utils:Runtime.Field(function()
       {
        return function()
        {
         return Data.UtilsProvider();
        };
       }),
       WithJQueryMobileLayout:function(theme)
       {
        var layout,x,arg00,f1;
        layout=(x=(arg00=function()
        {
         var panel,_this,arg001,arg10,_this1,arg002,formletLabelClass,formletElementClass,refreshBody,refresh,store,insert,remove,Insert,g,Remove;
         panel=Default.Div(List.ofArray([(_this=HTML5.Attr(),(arg001="data-"+"role",_this.NewAttr(arg001,"fieldcontain"))),(arg10=Global.String(theme),(_this1=HTML5.Attr(),(arg002="data-"+"theme",_this1.NewAttr(arg002,arg10))))]));
         formletLabelClass="formletLabel"+("id"+Math.round(Math.random()*100000000));
         formletElementClass="formletElement"+("id"+Math.round(Math.random()*100000000));
         refreshBody=function()
         {
          jQuery("."+formletLabelClass);
          return jQuery("."+formletElementClass).css("margin-bottom","10px");
         };
         refresh=function()
         {
          var x1,f;
          x1=setTimeout(refreshBody,1);
          f=function(value)
          {
           value;
          };
          f(x1);
          return jQuery(panel.Body).trigger("create");
         };
         store=_ElementStore_1.NewElementStore();
         insert=function(rowIx)
         {
          return function(body)
          {
           var row,jqPanel,index,inserted;
           row=Default.Div(Seq.toList(Seq.delay(function()
           {
            var matchValue,l,_this2,x1;
            return Seq.append((matchValue=body.Label,matchValue.$==0?(null,Seq.empty()):(l=matchValue.$0,[Operators.add(l(null),List.ofArray([Default.Attr().Class(formletLabelClass),(_this2=Default.Attr(),(x1=body.Element.get_Id(),_this2.NewAttr("for",x1)))]))])),Seq.delay(function()
            {
             return[Operators.add(body.Element,List.ofArray([Default.Attr().Class(formletElementClass)]))];
            }));
           })));
           jqPanel=jQuery(panel.Body);
           index={
            contents:0
           };
           inserted={
            contents:false
           };
           jqPanel.children().each(function()
           {
            var jqRow;
            jqRow=jQuery(this);
            if(rowIx===index.contents)
             {
              jQuery(row.Body).insertBefore(jqRow);
              row.Render();
              inserted.contents=true;
             }
            return Operators1.Increment(index);
           });
           if(!inserted.contents)
            {
             panel.AppendI(row);
            }
           return store.RegisterElement(body.Element.get_Id(),function()
           {
            return row["HtmlProvider@32"].Remove(row.Body);
           });
          };
         };
         remove=function(elems)
         {
          var enumerator,b;
          enumerator=Enumerator.Get(elems);
          while(enumerator.MoveNext())
           {
            b=enumerator.get_Current();
            store.Remove(b.Element.get_Id());
           }
         };
         Insert=(g=function(func1)
         {
          return function(x1)
          {
           return refresh(func1(x1));
          };
         },function(x1)
         {
          return g(insert(x1));
         });
         Remove=function(x1)
         {
          return refresh(remove(x1));
         };
         return{
          Body:Runtime.New(Body,{
           Element:panel,
           Label:{
            $:0
           }
          }),
          SyncRoot:null,
          Insert:Insert,
          Remove:Remove
         };
        },Layout.LayoutUtils().New(arg00)),(f1=function(l)
        {
         return function(formlet)
         {
          return Formlet2.WithLayout(l,formlet);
         };
        },f1(x)));
        return function(v)
        {
         Layout.setDefaultLayout();
         return layout(v);
        };
       },
       WithNoLayout:Runtime.Field(function()
       {
        var arg00;
        arg00=function()
        {
         var container,store,insert,remove;
         container=Default.Div(Runtime.New(T,{
          $:0
         }));
         store=_ElementStore_1.NewElementStore();
         insert=function(rowIx)
         {
          return function(body)
          {
           var jqPanel,index,inserted;
           jqPanel=jQuery(container.Body);
           index={
            contents:0
           };
           inserted={
            contents:false
           };
           jqPanel.children().each(function()
           {
            var jqRow;
            jqRow=jQuery(this);
            if(rowIx===index.contents)
             {
              jQuery(body.Element.Body).insertBefore(jqRow);
              body.Element.Render();
              inserted.contents=true;
             }
            return Operators1.Increment(index);
           });
           if(!inserted.contents)
            {
             container.AppendI(body.Element);
            }
           return store.RegisterElement(body.Element.get_Id(),function()
           {
            var _this;
            _this=body.Element;
            return _this["HtmlProvider@32"].Remove(_this.Body);
           });
          };
         };
         remove=function(elems)
         {
          var enumerator,b;
          enumerator=Enumerator.Get(elems);
          while(enumerator.MoveNext())
           {
            b=enumerator.get_Current();
            store.Remove(b.Element.get_Id());
           }
         };
         return{
          Body:Runtime.New(Body,{
           Element:container,
           Label:{
            $:0
           }
          }),
          SyncRoot:null,
          Insert:insert,
          Remove:remove
         };
        };
        return Layout.LayoutUtils().New(arg00);
       }),
       setDefaultLayout:function()
       {
        var _;
        _=Layout.WithNoLayout();
        Data.DefaultLayout=function()
        {
         return _;
        };
       }
      },
      Utils:{
       FormAndElement:function(formlet)
       {
        var formlet1,form,matchValue,body;
        formlet1=Formlet2.WithLayoutOrDefault(formlet);
        form=Formlet2.BuildForm(formlet1);
        matchValue=formlet1.get_Layout().Apply.call(null,form.Body);
        if(matchValue.$==0)
         {
          return[form,Default.Div(Runtime.New(T,{
           $:0
          }))];
         }
        else
         {
          body=matchValue.$0[0];
          return[form,body.Element];
         }
       },
       MkFormlet:function(x)
       {
        return Formlet2.BuildFormlet(function()
        {
         var patternInput,state,reset,elt,f,f1;
         patternInput=x(null);
         state=patternInput[2];
         reset=patternInput[1];
         elt=patternInput[0];
         f=(f1=function(e)
         {
          return jQuery(e.get_Body()).parent().trigger("create");
         },function(w)
         {
          return Operators.OnAfterRender(f1,w);
         });
         f(elt);
         return[elt,reset,state];
        });
       },
       "State`1":Runtime.Class({
        Subscribe:function(o)
        {
         var disp,_this;
         o.OnNext(this.Initial);
         disp=Util.subscribeTo((_this=this.Event,_this.event),function(v)
         {
          return o.OnNext(v);
         });
         return disp;
        },
        Trigger:function(v)
        {
         var _this;
         _this=this.Event;
         return _this.event.Trigger(v);
        }
       },{
        FromResult:function(res)
        {
         return Runtime.New(_State_1,{
          Initial:res,
          Event:_FSharpEvent_1.New()
         });
        },
        NewFail:function()
        {
         return Runtime.New(_State_1,{
          Initial:Runtime.New(_Result_1,{
           $:1,
           $0:Runtime.New(T,{
            $:0
           })
          }),
          Event:_FSharpEvent_1.New()
         });
        },
        NewSuccess:function(v)
        {
         return Runtime.New(_State_1,{
          Initial:Runtime.New(_Result_1,{
           $:0,
           $0:v
          }),
          Event:_FSharpEvent_1.New()
         });
        }
       }),
       swap:function(f,y,x)
       {
        return(f(x))(y);
       }
      }
     }
    },
    JQuery:{
     Mobile:{
      Enums:{
       IconPos:Runtime.Class({
        get_Value:function()
        {
         return this.value;
        }
       },{
        New:function(value)
        {
         var r;
         r={};
         r.value=value;
         return Runtime.New(this,r);
        },
        get_Bottom:function()
        {
         return IconPos.New("bottom");
        },
        get_Left:function()
        {
         return IconPos.New("left");
        },
        get_NoText:function()
        {
         return IconPos.New("notext");
        },
        get_Right:function()
        {
         return IconPos.New("right");
        },
        get_Top:function()
        {
         return IconPos.New("top");
        }
       }),
       Transition:Runtime.Class({
        get_Reverse:function()
        {
         return this.reverse.contents;
        },
        get_Value:function()
        {
         return this.value;
        },
        set_Reverse:function(v)
        {
         this.reverse.contents=v;
        }
       },{
        New:function(value)
        {
         var r;
         r={};
         r.value=value;
         r.reverse={
          contents:false
         };
         return Runtime.New(this,r);
        },
        get_Fade:function()
        {
         return Transition.New("fade");
        },
        get_Flip:function()
        {
         return Transition.New("flip");
        },
        get_Pop:function()
        {
         return Transition.New("pop");
        },
        get_Slide:function()
        {
         return Transition.New("slide");
        },
        get_SlideDown:function()
        {
         return Transition.New("slidedown");
        },
        get_SlideUp:function()
        {
         return Transition.New("slideup");
        }
       })
      }
     }
    }
   }
  }
 });
 Runtime.OnInit(function()
 {
  WebSharper=Runtime.Safe(Global.IntelliFactory.WebSharper);
  Formlets=Runtime.Safe(WebSharper.Formlets);
  JQueryMobile=Runtime.Safe(Formlets.JQueryMobile);
  ControlConfigurations=Runtime.Safe(JQueryMobile.ControlConfigurations);
  ButtonConfiguration=Runtime.Safe(ControlConfigurations.ButtonConfiguration);
  JQuery=Runtime.Safe(WebSharper.JQuery);
  Mobile=Runtime.Safe(JQuery.Mobile);
  Enums=Runtime.Safe(Mobile.Enums);
  Transition=Runtime.Safe(Enums.Transition);
  _SelectConfiguration_1=Runtime.Safe(ControlConfigurations["SelectConfiguration`1"]);
  List=Runtime.Safe(WebSharper.List);
  T=Runtime.Safe(List.T);
  Controls=Runtime.Safe(JQueryMobile.Controls);
  Arrays=Runtime.Safe(WebSharper.Arrays);
  Seq=Runtime.Safe(WebSharper.Seq);
  Utils=Runtime.Safe(JQueryMobile.Utils);
  _State_1=Runtime.Safe(Utils["State`1"]);
  Html=Runtime.Safe(WebSharper.Html);
  Default=Runtime.Safe(Html.Default);
  Math=Runtime.Safe(Global.Math);
  Operators=Runtime.Safe(Html.Operators);
  HTML5=Runtime.Safe(Default.HTML5);
  Formlet=Runtime.Safe(Global.IntelliFactory.Formlet);
  Base=Runtime.Safe(Formlet.Base);
  _Result_1=Runtime.Safe(Base["Result`1"]);
  EventsPervasives=Runtime.Safe(Html.EventsPervasives);
  Option=Runtime.Safe(WebSharper.Option);
  jQuery=Runtime.Safe(Global.jQuery);
  Operators1=Runtime.Safe(WebSharper.Operators);
  Unchecked=Runtime.Safe(WebSharper.Unchecked);
  Collections=Runtime.Safe(WebSharper.Collections);
  Dictionary=Runtime.Safe(Collections.Dictionary);
  Formlet1=Runtime.Safe(WebSharper.Formlet);
  Formlet2=Runtime.Safe(Formlet1.Formlet);
  IconPos=Runtime.Safe(Enums.IconPos);
  Enhance=Runtime.Safe(JQueryMobile.Enhance);
  Layout=Runtime.Safe(JQueryMobile.Layout);
  _ElementStore_1=Runtime.Safe(Layout["ElementStore`1"]);
  LayoutUtils=Runtime.Safe(Base.LayoutUtils);
  Reactive=Runtime.Safe(Global.IntelliFactory.Reactive);
  Reactive1=Runtime.Safe(Reactive.Reactive);
  Data=Runtime.Safe(Formlet1.Data);
  setTimeout=Runtime.Safe(Global.setTimeout);
  Enumerator=Runtime.Safe(WebSharper.Enumerator);
  Body=Runtime.Safe(Formlet1.Body);
  Util=Runtime.Safe(WebSharper.Util);
  Control=Runtime.Safe(WebSharper.Control);
  return _FSharpEvent_1=Runtime.Safe(Control["FSharpEvent`1"]);
 });
 Runtime.OnLoad(function()
 {
  Layout.WithNoLayout();
  Layout.Utils();
  Layout.LayoutUtils();
  Controls.Url();
  Controls.TextField();
  Controls.Tel();
  Controls.Search();
  Controls.Password();
  Controls.Number();
  Controls.Email();
 });
}());
