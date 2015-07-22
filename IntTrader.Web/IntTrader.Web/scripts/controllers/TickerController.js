'use strict';

app.controller('TickerController', ['$scope', 'backendHubProxy','$attrs',
  function ($scope, backendHubProxy, $attrs) {

      console.log('trying to connect to service');
      var tickerHub = backendHubProxy(backendHubProxy.defaultServer, 'TickerHub');
      console.log('connected to service');

      $scope.tickerHub = tickerHub;

      tickerHub.on('updateTicker', function (data) {
          if (data.exchange === $attrs.exchange && data.Pair.Key === $attrs.pair) {
              $scope.models = {
                  ticker: data
              };
          }
      });

      tickerHub.start().done(function () {
          var args = [$attrs.exchange, $attrs.pair];
          tickerHub.invoke('RequestTicker', args, function (data) { });
      });
  }
]);