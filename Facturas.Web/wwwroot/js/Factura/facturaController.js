$(document).ready(function () {
    // #region Funciones
    var App = {
        initApp: function () {
            App.CargarAccionesBotonesPrincipales();
            $('#txtValorFiltro').keydown(App.SoloNumeros);
        },
        CargarAccionesBotonesPrincipales: function () {

            // Botón Refrescar Tabla
            $("#btnAplicarBusqueda").on('click', function () {
                
                if ($('#txtValorFiltro').val().length === 0) {
                    alert("Debe ingresar el identificador de la factura.")
                }
                else {
                    var idfactura = $('#txtValorFiltro').val();
                    App.Controlador.ConsultarListadoPrincipal(idfactura);
                }
                    
            });
        },
        SoloNumeros: function (e) {
            try {
                var key = e.which || e.keyCode;
                if (!e.shiftKey && !e.altKey && !e.ctrlKey &&
                    // números
                    key >= 48 && key <= 57 ||
                    // números keypad
                    key >= 96 && key <= 105 ||
                    //// coma, periodo y minus, . en keypad
                    //key == 190 || key == 188 || key == 109 || key == 110 ||
                    // Backspace, Tab y Enter
                    key == 8 || key == 9 || key == 13 ||
                    // Home y End
                    key == 35 || key == 36 ||
                    // flechas izquierda y derecha
                    key == 37 || key == 39 ||
                    // Del y Ins
                    key == 46 || key == 45)
                    return true;

                return false;
            } catch (e) {
                console.log(e);
            }
        },
        Controlador: {
            ConsultarListadoPrincipal: function (idFactura) {
                $.ajax({
                    type: 'GET',
                    url: 'http://localhost/Facturas.Web/Factura/consultarFactura',
                    dataType: 'html',
                    contentType: "application/json; charset=utf-8",
                    data: ({ idFactura: idFactura })
                }).done(function (retrieved) {
                    $("#divTabla").empty();
                    $("#divTabla").html(retrieved);
                }).fail(function (e) {
                    $("#divTabla").empty();
                    $("#divTabla").html(e);
                    console.log(e);
                });
            }
        },
    }
    // #endregion Funciones

    App.initApp();
});