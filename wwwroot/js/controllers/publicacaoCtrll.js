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

    $scope.carregarPublicacao = function (id) {
        $http.get("/publicacoes/BuscarPublicacao/" + id).then(function (response) {
            $scope.publicacao = response.data;

            // Defina a categoria e as tags apenas após carregar as categorias e tags
            $scope.publicacao.categoriaId = response.data.categoriaId || "";
            $scope.publicacao.tagIds = response.data.publicacaoTags.map(tag => tag.id);
        });
    };

    $scope.carregarDados = function () {
        $scope.carregarCategorias();
        $scope.carregarTags();
        $scope.lerUrl();
    };

    $scope.lerUrl = function () {
        const url = window.location.href.split("/").pop();
        if (parseInt(url) > 0)
            $scope.carregarPublicacao(url);
    };

});