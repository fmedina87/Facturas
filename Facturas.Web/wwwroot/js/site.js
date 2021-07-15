$(document).ready(function () {
    // #region Funciones
    var App = {
        CargarAccionesBotonesPrincipales: function () {

            // Botón Refrescar Tabla
            $("#btnAplicarBusqueda").on('click', function () {
                App.Controlador.ConsultarListadoPrincipal();
            });
        },
        Controlador: {
            ConsultarListadoPrincipal: function () {
                ActivarDesactivarLoading("iboxTabla");
                ajaxPartialView('consultarFactura', 'Factura', null,
                    function (respHtml) {
                        $("#divTabla").empty();
                        $("#divTabla").html(respHtml);
                    }, undefined,
                    function () {
                        ActivarDesactivarLoading("iboxTabla");
                    });
            }
        },

    }
    function ActivarDesactivarLoading(idDiv) {
        var div = $('#' + idDiv).children('.ibox-content');
        div.toggleClass('sk-loading');
    }
    // #endregion Funciones   
});