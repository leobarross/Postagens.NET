app.controller('categoriaCtrll', function ($scope, $http) {
    $scope.categoria = {};
    $scope.abrirModalCadastro = function () {
        $('#modalCadastro').modal('show');
    };
    $scope.abrirModalEdicao = function (id) {
        $http.get("/Categorias/BuscarUmaCategoria/" + id).then(response => {
            $scope.categoria = response.data;
            $('#modalCadastro').modal('show');
        }).catch(error => {
            console.error("Erro ao buscar a tag:", error);
        });
    };

    $scope.abrirModalExclusao = function (id) {
        $http.get("/Categorias/BuscarUmaCategoria/" + id).then(response => {
            $scope.categoria = response.data;
            $('#confirmModal').modal('show');
        }).catch(error => {
            console.error("Erro ao buscar a tag:", error);
        });
    };
});