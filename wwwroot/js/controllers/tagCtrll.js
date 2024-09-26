app.controller('tagCtrll', function ($scope, $http) {

    $scope.tag = { nome: "" };

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
        $http.get("/Tags/BuscarPorId/" + id).then(response => {
            $scope.tag = response.data;
            $('#confirmModal').modal('show');
        }).catch(error => {
            console.error("Erro ao buscar a tag:", error);
        });
    };
});
