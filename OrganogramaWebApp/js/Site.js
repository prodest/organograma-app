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
/*WIZARD FORM*/
$(document).ready(function () {
    //Initialize tooltips
    $('.nav-tabs > li a[title]').tooltip();

    //Wizard
    $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
        var $target = $(e.target);
        if ($target.parent().hasClass('disabled')) {
            return false;
        }
    });

    $(".next-step").click(function (e) {
        var $active = $('.nav-tabs-custom .nav-tabs li.active');
        $active.next().removeClass('disabled');
        nextTab($active);
    });

    $(".prev-step").click(function (e) {
        var $active = $('.nav-tabs-custom .nav-tabs li.active');
        prevTab($active);
    });
});

function nextTab(elem) {
    $(elem).next().find('a[data-toggle="tab"]').click();
}
function prevTab(elem) {
    $(elem).prev().find('a[data-toggle="tab"]').click();
}

/****************************************************************************************************************************************************************************/
/*EXIBE TELA DE EDICAO DE TIPO DE UNIDADE*/
$('body').on('click', '.btnModalTipoUnidade', function () {
    var evento = "";

    if ($(this).attr('title') != "") {
        evento = $(this).attr('title')
    }
    else {
        evento = $(this).attr('data-original-title')
    }

    $.ajax(this.href)
      .done(function (tela) {

          //switch (evento) {
          //    case '1':
          //        $('#modalTipoUnidadeLabel').text('Cadastrar Tipo de Unidade');
          //        break;
          //    case '2':
          //        $('#modalTipoUnidadeLabel').text('Editar Tipo de Unidade');
          //        break;
          //    case '3':
          //        $('#modalTipoUnidadeLabel').text('Excluir Tipo de Unidade');                  
          //}

          $('#modalTipoUnidadeLabel').text(evento);

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
    var evento = $(this).attr('title');

    $.ajax(this.href)
      .done(function (tela) {

          //switch (evento) {
          //    case '1':
          //        $('#modalTipoUnidadeLabel').text('Cadastrar Tipo de Organização');
          //        break;
          //    case '2':
          //        $('#modalTipoUnidadeLabel').text('Editar Tipo de Organização');
          //        break;
          //    case '3':
          //        $('#modalTipoUnidadeLabel').text('Excluir Tipo de Organização');
          //}

          $('#modalTipoUnidadeLabel').text(evento);

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


/****************************************************************************************************************************************************************************/
/*SELECT2*/
$(document).ready(function () {
    $("#idOrganizacao").select2({ width: '100%' });
    $("#idTipoUnidade").select2({ width: '100%' });
    $("#idUnidadePai").select2({ width: '100%' });
    $("#endereco_estados").select2({ width: '100%' });
});

/****************************************************************************************************************************************************************************/
/*ADICIONA CAMPO SITE*/
$('body').on('click', '#addCampoSite', function addCampoSite() {
    var elemento = $(this);
    var i = elemento.attr('data-value');

    $.ajax('/Unidade/IncluirCampoSite?i=' + elemento.attr('data-value'))
      .done(function (field) {
          $('#camposite').append(field);
          elemento.attr('data-value', parseInt(i) + 1);
      })
      .fail(function () {
          toastr["warning"]("Não foi incluir novo campo para site!");
      });
});

/****************************************************************************************************************************************************************************/
/*ADICIONA CAMPO EMAIL*/
$('body').on('click', '#addCampoEmail', function addCampoSite() {
    $.ajax('/Unidade/IncluirCampoEmail')
      .done(function (field) {
          $('#campoemail').append(field);
      })
      .fail(function () {
          toastr["warning"]("Não foi incluir novo campo para e-mail!");
      });
});


/****************************************************************************************************************************************************************************/
/*ADICIONA CAMPO TELEFONE*/
$('body').on('click', '#addCampoTelefone', function addCampoSite() {
    $.ajax('/Unidade/IncluirCampoTelefone')
      .done(function (field) {
          $('#campotelefone').append(field);
      })
      .fail(function () {
          toastr["warning"]("Não foi incluir novo campo para telefone!");
      });
});


/****************************************************************************************************************************************************************************/
/*ADICIONA CAMPO TELEFONE*/
//$('#formCadastrarUnidade').submit(function (e) {
//    e.preventDefault();
//    $.ajax({
//        type: "POST",
//        url: "Create",
//        data: $(this).serialize()
//    }).done(function (data) {
//        toastr["success"]("Unidade cadastrada com sucesso!");
//    }).fail(function (data) {
//        toastr["warning"]("Não foi incluir novo campo para telefone!");
//    });      
//});

/****************************************************************************************************************************************************************************/
/*CARREGA MUNICIPIOS*/

$('body').on('change', '#endereco_estados', function (e) {

    var elemento = this;

    if (elemento.value !== '') {
        $.ajax({ url: '/home/Municipios?uf=' + elemento.value, async: false })
          .done(function (dados) {

              $('#endereco_guidMunicipio option:not([value=""])').remove();

              $.each(dados, function (i) {
                  var optionhtml = '<option value="' + this.guid + '">' + this.nome + '</option>';
                  $('#endereco_guidMunicipio').append(optionhtml);
              });
              console.log(dados);
          })
          .fail(function () {
              toastr["warning"]("Não foi possível realizar esta operação!");
          });
    }
});