using Facturas.DataBase.Interfaces;
using Facturas.Entidades.Modelos;
using Facturas.Servicios.Interfaces;
using Facturas.Servicios.Negocio;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Facturas.Web.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class FacturaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly IFactura _Factura;
        public FacturaController(IFactura factura)
        {
            _Factura = factura;
        }

        [HttpPost("crearFactura")]
        public int create([FromBodyAttribute] Factura objFactura)
        {
            int idFactura = 0;
            try
            {
                idFactura = _Factura.crear(objFactura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return idFactura;
        }
        [HttpGet("consultarFactura")]
        public PartialViewResult ConsultarxId(int idFactura)
        {
            Factura objFactura = new Factura();
            try
            {
                var _objFactura = _Factura.ConsultarxId(idFactura);
                if (_objFactura != null && _objFactura.Result != null)
                {
                    objFactura = _objFactura.Result;
                }
            }
            catch (Exception ex)
            {
                ViewBag.MensajeError = ex.InnerException.Message;
                return PartialView("_error");
            }
            return PartialView("_verDetalleFactura", objFactura);
        }
    }
}