'use strict';

app.controller('ExchangeController', ['$scope', 'backendHubProxy','$attrs',
  function ($scope, backendHubProxy, $attrs) {

      console.log('trying to connect to service');
      var tickerHub = backendHubProxy(backendHubProxy.defaultServer, 'ExchangeHub');
      console.log('connected to service');

      $scope.tickerHub = tickerHub;

      //------------------

      tickerHub.on('updateTicker', function (data) {
          if (data.exchange === $attrs.exchange && data.Pair.Key === $attrs.pair) {
              $scope.ticker = data;
          }
      });

      tickerHub.start().done(function () {
          var args = [$attrs.exchange, $attrs.pair];
          tickerHub.invoke('RequestTicker', args, function (data) { });
      });

      //------------------

      tickerHub.on('updateTrade', function (data) {
          if (data.exchange === $attrs.exchange) {
              $scope.trades = data.trades;
          }
      });

      tickerHub.start().done(function () {
          var args = [$attrs.exchange, $attrs.pair];
          tickerHub.invoke('RequestTrade', args, function (data) { });
      });

      //------------------

      tickerHub.on('updateBalance', function (data) {
          if (data.exchange === $attrs.exchange) {
              $scope.balances = data.balances.Items;
          }
      });

      tickerHub.start().done(function () {
          var args = [$attrs.exchange, $attrs.pair];
          tickerHub.invoke('RequestBalance', args, function (data) { });
      });
  }
]);