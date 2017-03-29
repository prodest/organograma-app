/****************************************************************************************************************************************************************************/
/*SELECT2*/
$(document).ready(function () {
    /*SELECT*/
    $("#GuidUnidadePai").select2({ width: '100%' });
    $("#IdTipoUnidade").select2({ width: '100%' });
    $("#siglaEstado").select2({ width: '100%' });
    $("#Endereco_GuidMunicipio").select2({ width: '100%' });

    /*MASCARAS*/
    $("#Cnpj").mask('00.000.000/0000-00', { reverse: true });
    $("#Endereco_Numero").mask('000000', { reverse: true });
    $("#Endereco_Cep").mask('00000-000', { reverse: true });
});

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
          elemento.attr('data-value', ++i);
      })
      .fail(function () {
          toastr["warning"]("Não foi incluir novo campo para site!");
      });
});

/****************************************************************************************************************************************************************************/
/*ADICIONA CAMPO EMAIL*/
$('body').on('click', '#addCampoEmail', function addCampoSite() {
    var elemento = $(this);
    var i = elemento.attr('data-value');

    $.ajax('/Unidade/IncluirCampoEmail?i=' + i)
      .done(function (field) {
          $('#campoemail').append(field);
          elemento.attr('data-value', ++i);
      })
      .fail(function () {
          toastr["warning"]("Não foi incluir novo campo para e-mail!");
      });
});


/****************************************************************************************************************************************************************************/
/*ADICIONA CAMPO TELEFONE*/
$('body').on('click', '#addCampoTelefone', function addCampoSite() {
    var elemento = $(this);
    var i = elemento.attr('data-value');

    $.ajax('/Unidade/IncluirCampoTelefone?i=' + i)
      .done(function (field) {
          $('#campotelefone').append(field);
          elemento.attr('data-value', ++i);
      })
      .fail(function () {
          toastr["warning"]("Não foi incluir novo campo para telefone!");
      });
});


/****************************************************************************************************************************************************************************/
/*CARREGA MUNICIPIOS*/

$('body').on('change', '#siglaEstado', function (e) {

    var elemento = this;

    if (elemento.value !== '') {
        $.ajax({ url: '/home/Municipios?uf=' + elemento.value, async: false })
          .done(function (dados) {

              $('#Endereco_GuidMunicipio option:not([value=""])').remove();

              $.each(dados, function (i) {
                  var optionhtml = '<option value="' + this.guid + '">' + this.nome + '</option>';
                  $('#Endereco_GuidMunicipio').append(optionhtml);
              });
              console.log(dados);
          })
          .fail(function () {
              toastr["warning"]("Não foi possível realizar esta operação!");
          });
    }
});

/****************************************************************************************************************************************************************************/
/*CARREGA UNIDADES DA ORGANIZACAO*/

$('body').on('change', '#idOrganizacao', function (e) {

    var elemento = this;

    if (elemento.value !== '') {
        $.ajax({ url: '/home/UnidadesPorOrganizacao?guidOrganizacao=' + elemento.value, async: false })
          .done(function (dados) {

              $('#idUnidadePai option:not([value=""])').remove();

              $.each(dados, function (i) {
                  var optionhtml = '<option value="' + this.id + '">' + this.nome + '</option>';
                  $('#idUnidadePai').append(optionhtml);
              });
              console.log(dados);
          })
          .fail(function () {
              toastr["warning"]("Não foi possível realizar esta operação!");
          });
    }
});

/****************************************************************************************************************************************************************************/
/*ATUALIZA LISTA DE UNIDADES*/
function onSuccessUnidades(dados) {
    //$.ajax('/unidade/')
      //.done(function (tela) {
          console.log(dados);
          //ExibirMensagens();
          if (dados) {
              var delay = 0;
              setTimeout(function () { window.location.href = '/Unidade?guidOrganizacao=' + $('#GuidOrganizacao').val(); }, delay);
          }
          else
              ExibirMensagens();
          //}
      //})
      //.fail(function () {
      //    toastr["warning"]("Não foi possível cadastrar a unidade!");
      //});
}

/****************************************************************************************************************************************************************************/
/*EXIBE TELA DE EDICAO, VISUALIZAO E EXCLUSAO DE DE UNIDADE*/
$('body').on('click', '.btnModalUnidade', function () {
    var evento = "";

    if ($(this).attr('title') != "") {
        evento = $(this).attr('title')
    }
    else {
        evento = $(this).attr('data-original-title')
    }

    $.ajax(this.href)
      .done(function (tela) {          
          if (tela != null) {
              $('#modalTipoUnidadeLabel').text(evento);
              $('#update').html(tela);
              $('#modalCriar').modal('show');
          }
      })
      .fail(function () {
          //toastr["warning"]("Não foi possível realizar esta operação!");
      });

    return false;
});

/****************************************************************************************************************************************************************************/
/*INSERE MASCARA CONFORME TIPO DE CONTATO SELECIONADO*/
$('body').on('change', '[id*="TipoContato"]', function () {
    try {
        var tipo = $(this).val();
        var campo = $(this).prop('id').split('.')[0];
        var telefone = '[id="' + campo + '.telefone"]';        

        switch (tipo) {
            case '1':
                $('body ' + telefone).mask('(99) 9999-9999');
                break;
            case '3':
                $('body ' + telefone).mask('(99) 9999-9999');
                break;
            case '2':
                $('body ' + telefone).mask('(99) 99999-9999');
                break;
            case '4':
                $('body ' + telefone).mask('(99) 9999-9999/9999 ');
        }
    }
    catch (error) {
        //console.log(error);
    }
});