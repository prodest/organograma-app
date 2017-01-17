/****************************************************************************************************************************************************************************/
/*FORMATA TABELA CAIXA DE ENTRADA*/
$(document).ready(function () {
    var caixaEntradaOrgao = $('#tabelaUnidades').DataTable({
        //"dom": '<"pull-left"l><"pull-right"f>rt<"pull-left"i><"pull-right"p>',
        "dom": '<"pull-right"f>rt<"pull-left"i><"pull-right"p>',
        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
        "sPaginationType": "full_numbers",
        "language": {
            "lengthMenu": " _MENU_ Unidades por página",
            "zeroRecords": "Nenhuma unidade encontrado",
            "info": "Página _PAGE_ de _PAGES_",
            "infoEmpty": "",
            "infoFiltered": "(Registros filtrados do total de _MAX_ unidades.)",
            "sSearch": "Filtrar: ",
            "paginate": {
                "previous": "‹",
                "next": "›",
                "first": "«",
                "last": "»"
            }
        }        
    });

    caixaEntradaOrgao.draw();
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
    $.ajax('/unidade/')
      .done(function (tela) {
          console.log(dados);
          ExibirMensagens();
          if (dados.IsSuccessStatusCode) {
              var delay = 3000;
              setTimeout(function () { window.location.href = '/Unidade'; }, delay);
          }
      })
      .fail(function () {
          toastr["warning"]("Não foi possível cadastrar a unidade!");
      });
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

          $('#modalTipoUnidadeLabel').text(evento);
          $('#update').html(tela);
          $('#modalCriar').modal('show');
      })
      .fail(function () {
          //toastr["warning"]("Não foi possível realizar esta operação!");
      });

    return false;
});
