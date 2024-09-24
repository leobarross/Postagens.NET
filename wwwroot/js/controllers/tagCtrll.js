app.controller('tagCtrll', function ($scope, $http, $window) {

    $scope.tag = { nome: "" };
    $scope.idPessoaExcluir = null;

    $scope.abrirModalCadastro = function () {
        $('#modalCadastroTag').modal('show');
    };
    $scope.abrirModalEdicao = function (id) {
        $http.get("/Tags/BuscarPorId/" + id).then(response => {
            $scope.tag = response.data;
            $('#modalCadastroTag').modal('show');
        }).catch(error => {
            console.error("Erro ao buscar a tag:", error);
        });
    };


    $scope.abrirModalExclusao = function (id) {
        $scope.idPessoaExcluir = id;
        console.log("Passei por aqui:" + $scope.idPessoaExcluir);

        // Abre o modal com Bootstrap 5
        var modal = new bootstrap.Modal(document.getElementById('confirmModal'));
        modal.show();
    };
    $scope.confirmarExclusao = function () {
        $('#confirmModal').modal('hide');
        $scope.excluirPessoa($scope.idPessoaExcluir);
    };

  
    //$scope.salvar = function () {
    //    if ($scope.tag.id) {
    //        // Editar tag existente
    //        $http.put("/Tags/Editar/" + $scope.tag.id, $scope.tag).then(response => {
    //            $window.location.href = "/Tags/Index";
    //        }).catch(error => {
    //            console.error("Erro ao editar a tag:", error);
    //        });
    //    } else {
    //        // Criar nova tag
    //        $http.post("/Tags/Cadastro", $scope.tag).then(response => {
    //            $window.location.href = "/Tags/Index";
    //        }).catch(error => {
    //            console.error("Erro ao criar a tag:", error);
    //        });
    //    }
    //};

    $scope.excluirPessoa = function (id) {
        $http.delete("/Tags/Excluir/" + id).then(response => {
            location.reload();
        });
    };

    $scope.buscarFormulario = function (id) {
        $http.get("/Tags/BuscarPorId/" + id).then(response => {
            $scope.tag = response.data;
        }).catch(error => {
            console.error("Erro ao buscar a tag:", error);
        });
    }

    $scope.lerUrl = function () {
        const url = window.location.href.split("/").pop();
        if (parseInt(url) > 0)
            $scope.buscarFormulario(url);
    };


});
