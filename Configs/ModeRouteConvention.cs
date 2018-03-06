using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace dnc2.Configs{
    public class ModeRouteConvention : IApplicationModelConvention
    {
        public delegate bool tDelegate(string s);

        private tDelegate exception;
        public ModeRouteConvention(tDelegate del){
            //del("wat");
            exception = del;
        }
         public ModeRouteConvention():this(null){}

        public void Apply(ApplicationModel application)
        {     
            var globalPrefix = new AttributeRouteModel(new RouteAttribute("api/"));

            foreach( var controller in application.Controllers ){
                
                /* 
                var routeSelector = controller.Selectors.FirstOrDefault(x => x.AttributeRouteModel != null);
                if( routeSelector != null ){

                    if( exception != null && exception(routeSelector.AttributeRouteModel.Template) ){
                        Console.WriteLine("contains: "+routeSelector.AttributeRouteModel.Template);
                    }else{
                          Console.WriteLine("not contains: "+routeSelector.AttributeRouteModel.Template);
                    }

                    Console.WriteLine( "--------------------------controller = "+ routeSelector.AttributeRouteModel.Template );
                    Console.WriteLine( "--------------------------controller = "+ routeSelector.AttributeRouteModel.Attribute.Template );       
                    Console.WriteLine(  );

                    routeSelector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(globalPrefix, routeSelector.AttributeRouteModel);
                }else{
                    controller.Selectors.Add( new SelectorModel(){ AttributeRouteModel = globalPrefix } );
                }
                */
                //edit later loop all, not only first
                var routeSelectors = controller.Selectors.Where(x => x.AttributeRouteModel != null);
                if( routeSelectors != null ){
                    foreach( var sel in routeSelectors ){

                            //check exc

                            Console.WriteLine( "----------------------sel = "+ sel.AttributeRouteModel.Template );
                            Console.WriteLine( "----------------------sel = "+ sel.AttributeRouteModel.Attribute.Template );       
                                    
                            sel.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(globalPrefix, sel.AttributeRouteModel);
                    }
                    Console.WriteLine(  ); 
                }else{
                    controller.Selectors.Add( new SelectorModel(){ AttributeRouteModel = globalPrefix } );
                }
            }
            



        }
    }
    
}