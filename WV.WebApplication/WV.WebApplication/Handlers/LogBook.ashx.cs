using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WV.WebApplication.Utils;
using DataLayer;
using Repository;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using System.Globalization;

namespace WV.WebApplication.Handlers
{
    /// <summary>
    /// Summary description for LogBook
    /// </summary>
    public class LogBook : ActionTemplate, IHttpHandler, IRequiresSessionState
    {
        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
        IAWContext _context1;
        IDataRepository<Bitacora> _bitacora;
        IDataRepository<BitacoraDetalle> _bitacoraDetalle;
        IAWContext _context2;
        IDataRepository<Persona> _persona;
        IAWContext _lazyContext;
        public void ProcessRequest(HttpContext context)
        {
            
            InitializeObjects();
            MethodName = context.Request.Params["method"];
            CallBackMethodName = context.Request.Params["callbackmethod"];
            Parameter = context.Request.Params["parameter"];

            switch (MethodName.ToLower())
            {
                case "getall":
                    context.Response.Write(GetAllLogBooks(context));
                    break;
                case "delete":
                    context.Response.Write(DeleteRecord(context));
                    break;
                case "getsingle":
                    context.Response.Write(GetSingleRecord(context));
                    break;
                case "add":
                    context.Response.Write(AddRecord(context));
                    break;
                case "edit":
                    context.Response.Write(EditRecord(context));
                    break;
            }
       }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public override void InitializeObjects()
        {
            _context = new AWContext(Connection);

           _bitacora = new DataRepository<IAWContext, Bitacora>(_context);

           _context1 = new AWContext(Connection);

           _bitacoraDetalle = new DataRepository<IAWContext, BitacoraDetalle>(_context1);

           _context2 = new AWContext(Connection);

           _persona = new DataRepository<IAWContext, Persona>(_context2);

           _lazyContext = new AWContext();
            
        }

        public override string GetAllRecords()
        {
            throw new NotImplementedException();
        }

        public string GetAllLogBooks(HttpContext context)
        {
            int ID_Persona = Int32.Parse(context.Request.Params["ID_Persona"].ToString());
            
            string htmlElements = "";
            string nombre = "";
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var bitacoras = _bitacora.Where(p => p.ID_Persona == ID_Persona);
                var persona = _persona.GetFirst(p => p.ID_Persona == ID_Persona);
                nombre = persona.Nombre + " " + persona.Apellido;
                foreach (var bitacora in bitacoras)
                {
                    htmlElements += "<li data-logbook-header='" + bitacora.ID_Bitacora + "'><a>" + bitacora.FechaBitacora.ToShortDateString() + "<span class='sub_icon glyphicon glyphicon-link'></span></a>";
                }

                if (bitacoras.ToList().Count > 0)
                {
                    response.IsSucess = true;
                    response.ResponseData = htmlElements;
                    response.Message = string.Empty;
                    response.CallBack = string.Empty;
                    response.ResponseAdditional = nombre;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public override string GetSingleRecord(HttpContext context)
        {
            int ID_Bitacora = Int32.Parse(context.Request.Params["ID_Bitacora"].ToString());
            
            
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var bitacora = _bitacora.GetFirst(p => p.ID_Bitacora == ID_Bitacora);
                int idParent= bitacora.ID_Bitacora;
                var bitacoraDetalle = _bitacoraDetalle.Where(bd=> bd.ID_Bitacora == idParent);

                var finalObject = new{ ObjetoBitacora= bitacora, ObjetoBitacoraDetalle= bitacoraDetalle};
                
                    response.IsSucess = true;
                    response.ResponseData = finalObject;
                    response.Message = string.Empty;
                    response.CallBack = string.Empty;
                   
               
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public override string DeleteRecord(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Bitacora = Int32.Parse(context.Request.Params["ID_Bitacora"].ToString());
            try
            {
                var bitacora = _bitacora.GetFirst(u => u.ID_Bitacora == ID_Bitacora);
                _bitacora.Delete(bitacora);
                _context.SaveChanges();
                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Registro Eliminado Satisfactoriamente";
                response.CallBack = string.Empty;

            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public override string AddRecord(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            DateTime FechaBitacora;
            try
            {
                var data = context.Request;
                var sr = new StreamReader(data.InputStream);
                var stream = sr.ReadToEnd();
                var javaScriptSerializer = new JavaScriptSerializer();
                var bitacora = javaScriptSerializer.Deserialize<BitacorasTemp>(stream);

                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                FechaBitacora =  epoch.AddMilliseconds(Convert.ToInt64(bitacora.FechaBitacora));

                Bitacora bitacoraEncabezado = new Bitacora();
                bitacoraEncabezado.ID_Persona = bitacora.ID_Persona;
                bitacoraEncabezado.FechaBitacora = FechaBitacora;
                bitacoraEncabezado.CreadoPor = SystemUsername;

                foreach (var detalle in bitacora.BitacoraDetalle)
	            {
                    BitacoraDetalle bitacoraDetalle= new BitacoraDetalle();
                    bitacoraDetalle.Actividad = detalle.Actividad;
                    bitacoraDetalle.Observaciones= detalle.Observaciones;
                    bitacoraDetalle.CreadoPor = SystemUsername;
                    bitacoraEncabezado.BitacoraDetalle.Add(bitacoraDetalle);
		 
	            }

                _bitacora.Add(bitacoraEncabezado);
                _context.SaveChanges();

                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Registro Creado Satisfactoriamente";
                response.CallBack = string.Empty;
                
            }
            catch (Exception msg)
            {

                response.Message = msg.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public override string EditRecord(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            
            try
            {
                var data = context.Request;
                var sr = new StreamReader(data.InputStream);
                var stream = sr.ReadToEnd();
                var javaScriptSerializer = new JavaScriptSerializer();
                var bitacoraEncabezado = javaScriptSerializer.Deserialize<BitacorasTemp>(stream);

                int ID_Bitacora =bitacoraEncabezado.ID_Persona; // uso el id pero no es necesariamente es de la persona sino de la bitacora
                _bitacora = new DataRepository<IAWContext,Bitacora>(_lazyContext);

                var bitacora = _bitacora.GetFirst(b=>b.ID_Bitacora==ID_Bitacora);
                bitacora.ModificadoPor = SystemUsername;

                //Forma menos complicada borrar todos los detalles e insertar los nuevos


                foreach (var detalle in bitacora.BitacoraDetalle.ToList())
                {
                    var bitacoraDetalle= _lazyContext.Set<BitacoraDetalle>().Single(c=> c.ID_BitacoraDetalle ==detalle.ID_BitacoraDetalle);
                    _lazyContext.Set<BitacoraDetalle>().Remove(bitacoraDetalle);
                    _lazyContext.SaveChanges();
                }

                
                
                //Insercion de nuevos detalles
                foreach (var detalle in bitacoraEncabezado.BitacoraDetalle)
                {
                    BitacoraDetalle bitacoraDetalle = new BitacoraDetalle();
                    bitacoraDetalle.Actividad = detalle.Actividad;
                    bitacoraDetalle.Observaciones = detalle.Observaciones;
                    bitacoraDetalle.CreadoPor = SystemUsername;
                    bitacora.BitacoraDetalle.Add(bitacoraDetalle);

                }

                _lazyContext.SaveChanges();

                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Edicion realizada Satisfactoriamente";
                response.CallBack = string.Empty;

            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                response.IsSucess = false;
            }
            return serializer.Serialize(response);
        }

        [Serializable]
        public class BitacorasTemp
        {
            public int ID_Persona { get;set;}
            public string FechaBitacora { get; set; }
            public List<BitacoraTemp> BitacoraDetalle { get; set; }
            public List<int> ToDelete { get; set; }
        }
        [Serializable]
        public class BitacoraTemp
        {
            public int ID_BitacoraDetalle { get; set; }
            public string Actividad { get; set; }
            public string Observaciones {get; set; }
        }
    }
}