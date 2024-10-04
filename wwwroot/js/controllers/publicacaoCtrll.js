app.controller('publicacaoCtrll', function ($scope, $http) {
    $scope.publicacao = { tagIds: [] };
    $scope.categorias = [];
    $scope.tags = [];

    $scope.carregarCategorias = function () {
        $http.get("/Categorias/ListarCategorias")
            .then(function (response) {
                $scope.categorias = response.data;
                console.log("Categorias carregadas:", $scope.categorias);
            })
            .catch(function (error) {
                console.error("Erro ao carregar categorias:", error);
            });
    };

    $scope.carregarTags = function () {
        $http.get("/Tags/BuscarTags").then(response => {
            $scope.tags = response.data;
        }).catch(error => {
            console.error("Erro ao buscar a tag:", error);
        });
    }

    $scope.abrirModal = function (id) {
        $http.get("/publicacoes/BuscarPublicacao/" + id).then(response => {
            $scope.publicacao = response.data;
            $('#confirmModal').modal('show');
        }).catch(error => {
            console.error("Erro ao buscar a publicação:", error);
        });
    };

    $scope.carregarDados = function () {
        console.log("Método foi chamado.")
        $scope.carregarCategorias();
        $scope.carregarTags();
    };

});