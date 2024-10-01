app.controller('publicacaoCtrll', function ($scope, $http) {
    $scope.publicacao = {};
    $scope.categorias = [];

    $scope.carregarCategorias = function () {
        $http.get('/Categorias/ListarCategorias')
            .then(function (response) {
                $scope.categorias = response.data;
                console.log("Categorias carregadas:", $scope.categorias);
            })
            .catch(function (error) {
                console.error("Erro ao carregar categorias:", error);
            });
    };

    $scope.abrirModal = function (id) {
        $http.get("/publicacoes/BuscarPublicacao/" + id).then(response => {
            $scope.publicacao = response.data;
            console.log("Dados recebidos:", $scope.publicacao);
            console.log("Tentando abrir o modal");
            $('#confirmModal').modal('show');
        }).catch(error => {
            console.error("Erro ao buscar a publicação:", error);
        });
    };

});