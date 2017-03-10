/****************************************************************************************************************************************************************************/
/*SELECT2*/
$(document).ready(function () {
    /*SELECT*/
    $("#GuidOrganizacaoPai").select2({ width: '100%' });
    $("#IdTipoOrganizacao").select2({ width: '100%' });
    $("#siglaEstado").select2({ width: '100%' });
    $("#Endereco_GuidMunicipio").select2({ width: '100%' });

    /*MASCARAS*/
    $("#Cnpj").mask('00.000.000/0000-00', { reverse: true });
    $("#Endereco_Numero").mask('000000', { reverse: true });
    $("#Endereco_Cep").mask('00000-000', { reverse: true });    
});

/****************************************************************************************************************************************************************************/
/*ADICIONA CAMPO SITE*/
$('body').on('click', '#addCampoSite', function addCampoSite() {
    var elemento = $(this);
    var i = elemento.attr('data-value');

    $.ajax('/Organizacao/IncluirCampoSite?i=' + elemento.attr('data-value'))
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

    $.ajax('/Organizacao/IncluirCampoEmail?i=' + i)
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

    $.ajax('/Organizacao/IncluirCampoTelefone?i=' + i)
      .done(function (field) {
          $('#campotelefone').append(field);
          elemento.attr('data-value', ++i);
      })
      .fail(function () {
          toastr["warning"]("Não foi incluir novo campo para telefone!");
      });
});

/****************************************************************************************************************************************************************************/
/*CARREGA PODER E ESFERA*/

$('body').on('change', '#GuidOrganizacaoPai', function (e) {

    var elemento = this;

    if (elemento.value !== '') {
        $.ajax({ url: '/Organizacao/IncluirPoderEsfera?guid=' + elemento.value, async: false })
          .done(function (dados) {
              $('#Poder').val(dados.PoderDescricao);
              $('#Esfera').val(dados.EsferaDescricao);
          })
          .fail(function () {
              toastr["warning"]("Não foi possível realizar esta operação!");
          });
    }
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
        $.ajax({ url: '/home/OrganizacaosPorOrganizacao?guidOrganizacao=' + elemento.value, async: false })
          .done(function (dados) {

              $('#idOrganizacaoPai option:not([value=""])').remove();

              $.each(dados, function (i) {
                  var optionhtml = '<option value="' + this.id + '">' + this.nome + '</option>';
                  $('#idOrganizacaoPai').append(optionhtml);
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
function onSuccessOrganizacao(dados) {
    //$.ajax('/unidade/')
      //.done(function (tela) {
          console.log(dados);
          //ExibirMensagens();
          if (dados) {
              var delay = 0;
              setTimeout(function () { window.location.href = '/Organizacao' }, delay);
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
$('body').on('click', '.btnModalOrganizacao', function () {
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
              $('#modalTipoOrganizacaoLabel').text(evento);
              $('#update').html(tela);
              $('#modalCriar').modal('show');
          }
      })
      .fail(function () {
          //toastr["warning"]("Não foi possível realizar esta operação!");
      });

    return false;
});
