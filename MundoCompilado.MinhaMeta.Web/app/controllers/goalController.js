app.controller('GoalCtrl', function ($scope, $rootScope, $location) {

    $scope.models = [
        {
            Name: 'Comprar carro',
            Description: 'Quero comprar um carro muito louco!',
            PurposeName: 'reais',
            Purpose: 50.000
        },
        {
            Name: 'Comprar carro',
            Description: 'Quero comprar um carro muito louco!',
            PurposeName: 'reais',
            Purpose: 50.000
        },
        {
            Name: 'Comprar carro',
            Description: 'Quero comprar um carro muito louco!',
            PurposeName: 'reais',
            Purpose: 50.000
        },
        {
            Name: 'Comprar carro',
            Description: 'Quero comprar um carro muito louco!',
            PurposeName: 'reais',
            Purpose: 50.000
        }
    ];

    $scope.save = function (model) {
        $scope.models.push(model);

        $scope.showForm = false;

    }

    $scope.update = function (model) {

    }

    $scope.remove = function (index) {
        $scope.models.splice(index, 1);
    }

    $scope.edit = function (index) {
        $scope.model = $scope.models[index];
        $scope.showForm = true;
    }
});