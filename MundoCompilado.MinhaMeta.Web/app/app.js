var app = angular.module('app', ['ngRoute']);

app.config(function ($routeProvider, $locationProvider) {
  //$locationProvider.html5Mode(true);

  $routeProvider

  .when('/', {
    templateUrl: 'app/views/home.html',
    controller: 'HomeCtrl',
  })

  //.when('/', {
  //  templateUrl: 'app/views/goal.html',
  //  controller: 'GoalCtrl',
  //})

  .when('/user', {
    templateUrl: 'app/views/user.html',
    controller: 'UserCtrl',
  })

    .when('/goal', {
      templateUrl: 'app/views/goal.html',
      controller: 'GoalCtrl',
    })

  .otherwise({ redirectTo: '/' });
});

app.run(function ($rootScope, $location, $timeout) {
  $rootScope.$on('$viewContentLoaded', function () {
    $timeout(function () {
      componentHandler.upgradeAllRegistered();
    });
  });
});