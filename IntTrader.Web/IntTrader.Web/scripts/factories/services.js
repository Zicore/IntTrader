'use strict';

app.factory('backendHubProxy', ['$rootScope', 'backendServerUrl',
  function ($rootScope, backendServerUrl) {

      function backendFactory(serverUrl, hubName) {
          var connection = $.hubConnection(backendServerUrl);
          var proxy = connection.createHubProxy(hubName);

          var hubStart = null;

          var hub = {
              on: function (eventName, callback) {
                  proxy.on(eventName, function (result) {
                      $rootScope.$evalAsync(function () {
                          if (callback) {
                              callback(result);
                          }
                      });
                  });
              },
              invoke: function (methodName, args, callback) {
                  proxy.invoke.apply(proxy, $.merge([methodName], args))
                      .done(function (result) {
                          $rootScope.$evalAsync(function () {
                              if (callback) {
                                  callback(result);
                              }
                          });
                      });
              },
              start: function () {
                  if (hubStart === null) {
                      hubStart = connection.start();
                  }
                  return hubStart;
              }
          };
          return hub;
      };

      return backendFactory;
  }]);