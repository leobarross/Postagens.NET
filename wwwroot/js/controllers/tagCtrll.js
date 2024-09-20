app.controller('tagCtrll', function ($scope, $http) {

    $scope.tag = {nome: ""}; // Modelo para a nova tag

    $scope.salvar = function () {
        if ($scope.tag.id) {
            // Editar tag existente
            $http.put("/Tag/" + $scope.tag.id, $scope.tag).then(response => {
                $scope.tag = response.data; // Atualiza a tag editada
            });
            return;
        }

        // Criar nova tag
        $http.post("/Tag/AdicionarTag", $scope.tag).then(response => {
            $scope.tags.push(response.data); // Adiciona a nova tag à lista
            $scope.tag = {}; // Limpa o modelo
            $('#cadastroTagModal').modal('hide'); // Fecha o modal
        });
    };

});