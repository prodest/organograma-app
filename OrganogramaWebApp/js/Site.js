/****************************************************************************************************************************************************************************/
/*VARIAVEIS GLOBAIS*/

var status;
var mensagem;

/****************************************************************************************************************************************************************************/
/*MODAL WAITING*/
$(document).ajaxStart(function () {
    $('#modalWaiting').modal('show');
});

$(document).ajaxStop(function () {
    $('#modalWaiting').modal('hide');
    ExibirMensagens();
});


/****************************************************************************************************************************************************************************/
/*MENSAGEM*/
toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-top-center",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

/****************************************************************************************************************************************************************************/
/*EXIBE TELA DE EDICAO DE TIPO DE UNIDADE*/
$('body').on('click', '.btnModalTipoUnidade', function () {
    var evento = $(this).attr('data-evento');

    $.ajax(this.href)
      .done(function (tela) {          

          switch (evento) {
              case '1':
                  $('#modalTipoUnidadeLabel').text('Cadastrar Tipo de Unidade');
                  break;
              case '2':
                  $('#modalTipoUnidadeLabel').text('Editar Tipo de Unidade');
                  break;
              case '3':
                  $('#modalTipoUnidadeLabel').text('Excluir Tipo de Unidade');                  
          }

          $('#update').html(tela);
          $('#modalCriar').modal('show');
      })
      .fail(function () {
          //toastr["warning"]("Não foi possível realizar esta operação!");
      });

    return false;
});

/****************************************************************************************************************************************************************************/
/*ATUALIZA LISTA DE TIPOS DE UNIDADE*/
function onSuccessTipoUnidade(dados) {
    $.ajax('/TipoUnidade/Listar')
      .done(function (tela) {
          $('#listaTipoUnidade').html(tela);
      })
      .fail(function () {
          toastr["warning"]("Não foi possível atualizar a lista de Tipo de Unidades!");
      });
}

/****************************************************************************************************************************************************************************/
/*EXIBE TELA DE EDICAO DE TIPO DE ORGANIZACAO*/
$('body').on('click', '.btnModalTipoOrganizacao', function () {
    var evento = $(this).attr('data-evento');

    $.ajax(this.href)
      .done(function (tela) {

          switch (evento) {
              case '1':
                  $('#modalTipoUnidadeLabel').text('Cadastrar Tipo de Organização');
                  break;
              case '2':
                  $('#modalTipoUnidadeLabel').text('Editar Tipo de Organização');
                  break;
              case '3':
                  $('#modalTipoUnidadeLabel').text('Excluir Tipo de Organização');
          }

          $('#update').html(tela);
          $('#modalCriar').modal('show');
      })
      .fail(function () {
          //toastr["warning"]("Não foi possível realizar esta operação!");
      });

    return false;
});

/****************************************************************************************************************************************************************************/
/*ATUALIZA LISTA DE TIPOS DE ORGANIZACAO*/
function onSuccessTipoOrganizacao(dados) {
    $.ajax('/TipoOrganizacao/Listar')
      .done(function (tela) {
          $('#listaTipoOrganizacao').html(tela);
      })
      .fail(function () {
          toastr["warning"]("Não foi possível atualizar a lista de Tipo de Organização!");
      });
}



