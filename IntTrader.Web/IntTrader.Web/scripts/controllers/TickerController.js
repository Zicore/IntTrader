'use strict';

app.controller('ExchangeController', ['$scope', 'backendHubProxy','$attrs',
  function ($scope, backendHubProxy, $attrs) {

      console.log('trying to connect to service');
      var tickerHub = backendHubProxy(backendHubProxy.defaultServer, 'TickerHub');
      console.log('connected to service');

      $scope.tickerHub = tickerHub;

      tickerHub.on('update', function(data) {
          console.log($scope.exchange);
          if (data.id === $attrs.exchange){
              $scope.models = {
                  ticker: data
              };
          }
      });
  }
]);

// The $inject property of every controller (and pretty much every other type of object in Angular) needs to be a string array equal to the controllers arguments, only as strings
//LandingPageController.$inject = ['$scope'];